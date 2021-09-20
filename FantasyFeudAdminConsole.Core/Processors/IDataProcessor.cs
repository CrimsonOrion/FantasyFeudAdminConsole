using FantasyFeudAdminConsole.Core.Models;

using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public interface IDataProcessor
    {
        Task<int> AddTeamMemberAsync(TeamMembersDataModel model);
        Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore);
        Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId);
        Task<AnswerDataModel> GetAnswerDataAsync(int questionId);
        Task<GamesDataModel> GetGameDataAsync(int gameId);
        Task<QuestionsDataModel> GetQuestionDataAsync(int gameId, int questionId);
        Task<TeamsDataModel> GetTeamDataAsync(int gameId);
        Task<TeamMembersDataModel> GetTeamMembersDataAsync(int gameId);
        Task<int> RemoveTeamMemberAsync(int teamMemberId);
        Task<int> ShowAnswerAsync(AnswerDataModel answer);
    }
}