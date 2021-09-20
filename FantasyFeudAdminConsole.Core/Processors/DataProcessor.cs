using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessor : IDataProcessor
    {
        private ISqlDataAccess _sqlDataAccess;

        public DataProcessor(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<GamesDataModel> GetGameDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", gameId }
            };
            IEnumerable<GamesDataModel> output = await _sqlDataAccess.GetDataAsync<GamesDataModel, Dictionary<string, object>>("spGameData_GetById", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<TeamsDataModel> GetTeamDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId }
            };
            IEnumerable<TeamsDataModel> output = await _sqlDataAccess.GetDataAsync<TeamsDataModel, Dictionary<string, object>>("spTeamData_GetByGameId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<TeamMembersDataModel> GetTeamMembersDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId }
            };
            IEnumerable<TeamMembersDataModel> output = await _sqlDataAccess.GetDataAsync<TeamMembersDataModel, Dictionary<string, object>>("spTeamMembers_GetByGameId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<QuestionsDataModel> GetQuestionDataAsync(int gameId, int questionId)
        {
            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId },
                { "@QuestionId", questionId }
            };
            var output = await _sqlDataAccess.GetDataAsync<QuestionsDataModel, Dictionary<string, object>>("spQuestion_GetByGameIdAndQuestionId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<AnswerDataModel> GetAnswerDataAsync(int questionId)
        {
            Dictionary<string, object> param = new()
            {
                { "@QuestionId", questionId }
            };
            var output = await _sqlDataAccess.GetDataAsync<AnswerDataModel, Dictionary<string, object>>("spAnswers_GetByQuestionId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<int> ShowAnswerAsync(AnswerDataModel answer)
        {
            answer.Visible = answer.Visible == 1 ? 0 : 1;
            Dictionary<string, object> param = new()
            {
                { "@Id", answer.Id },
                { "@Visible", answer.Visible }
            };
            var result = await _sqlDataAccess.PostDataAsync("spAnswers_UpdateById", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }

        public async Task<int> AwardPointsAsync()
    }
}