using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public interface IDataProcessor
    {
        Task<int> AddTeamMemberAsync(TeamMembersDataModel model);
        Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore);
        Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId);
        Task<AnswersDataModel> GetAnswersDataAsync(int questionId);
        Task<GamesDataModel> GetGameDataAsync(int gameId);
        Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId);
        Task<TeamsDataModel> GetTeamDataAsync(int gameId);
        Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId);
        Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int gameId);
        Task<int> RemoveTeamMemberAsync(int teamMemberId);
        Task<int> ShowAnswerAsync(AnswersDataModel answer);
    }
}