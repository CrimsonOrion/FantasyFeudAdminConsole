using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors;

public interface IDataProcessor
{
    Task<int> AddStrikeAsync(QuestionsDataModel model);
    Task<int> AddTeamMemberAsync(TeamMembersDataModel model);
    Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore);
    Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId);
    Task<int> ChangeTeamNamesAsync(TeamsDataModel model);
    Task<int> ChangeTeamScoreAsync(GamesDataModel model);
    Task<IEnumerable<AnswersDataModel>> GetAnswersDataAsync(int questionId);
    Task<GamesDataModel> GetGameDataAsync(int gameId);
    Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId);
    Task<TeamsDataModel> GetTeamDataAsync(int teamId);
    Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId);
    Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId);
    Task<int> RemoveTeamMemberAsync(int teamMemberId);
    Task<int> ShowAnswerAsync(AnswersDataModel answer);
    Task<bool> UpdateAllDataAsync(TeamsDataModel team1, TeamsDataModel team2, GamesDataModel games, IEnumerable<TeamMembersDataModel> team1Members, IEnumerable<TeamMembersDataModel> team2Members, QuestionsDataModel questions, IEnumerable<AnswersDataModel> answers);
}