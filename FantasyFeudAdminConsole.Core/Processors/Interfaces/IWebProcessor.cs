using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors;

public interface IWebProcessor
{
    QuestionModel CreateEventMessage(GamesDataModel games, IEnumerable<TeamsDataModel> teams, IEnumerable<TeamMembersDataModel> teamMembers, QuestionsDataModel questions, IEnumerable<AnswersDataModel> answers);
    Task<string> PostEventAsync(QuestionModel questionModel);
}