using FantasyFeudAdminConsole.Core.Configuration;
using FantasyFeudAdminConsole.Core.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class WebProcessor : IWebProcessor
    {
        private static readonly Uri _eventServerUri = GlobalConfig.WebServerSettings.EventServer;

        public WebProcessor() { }

        public QuestionModel CreateEventMessage(GamesDataModel games, IEnumerable<TeamsDataModel> teams, IEnumerable<TeamMembersDataModel> teamMembers, QuestionsDataModel questions, IEnumerable<AnswersDataModel> answers)
        {
            QuestionModel message = new()
            {
                IsValid = true,
                Team1Name = teams.FirstOrDefault(_ => _.Id == games.Team1Id).TeamName,
                Team2Name = teams.FirstOrDefault(_ => _.Id == games.Team2Id).TeamName,
                Team1Score = games.Team1Score,
                Team2Score = games.Team2Score,
                Team1Members = teamMembers.Where(_ => _.TeamId == games.Team1Id).Select(_ => new TeamMembersModel() { Name = _.Name, Active = _.Active }),
                Team2Members = teamMembers.Where(_ => _.TeamId == games.Team2Id).Select(_ => new TeamMembersModel() { Name = _.Name, Active = _.Active }),
                Question = questions.Question,
                Responses = questions.Responses,
                Strikes = questions.Strikes,
                Answers = answers.Select(_ => new AnswerModel()
                {
                    Answer = _.Visible == 1 ? _.Answer : "",
                    Value = _.Visible == 1 ? _.Value : 0,
                    Visible = _.Visible
                })
            };

            return message;
        }

        public async Task<string> PostEventAsync(QuestionModel questionModel)
        {
            var res = "";
            try
            {
                HttpResponseMessage httpResMessage = null;
                using HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonSerializer.Serialize(questionModel);
                Debug.WriteLine(json);

                StringContent httpContent = new(json, Encoding.UTF8, "application/json");

                httpResMessage = await httpClient.PostAsync(_eventServerUri, httpContent);

                res = httpResMessage.StatusCode == HttpStatusCode.OK
                    ? httpResMessage.Content.ReadAsStringAsync().Result
                    : "Some error occured. " + httpResMessage.StatusCode;

                return res;
            }
            catch (Exception ex)
            {
                return $"{res}\r\n{ex.Message}";
            }
        }
    }
}