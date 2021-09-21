using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessorMsSql : IDataProcessor
    {
        private readonly IDataAccess _sqlDataAccess;

        public DataProcessorMsSql(IDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<GamesDataModel> GetGameDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", gameId }
            };
            IEnumerable<GamesDataModel> output = await _sqlDataAccess.GetDataAsync<GamesDataModel, Dictionary<string, object>>("spGames_GetById", param, true);
            return output.FirstOrDefault();
        }

        public async Task<TeamsDataModel> GetTeamDataAsync(int teamId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamId }
            };
            IEnumerable<TeamsDataModel> output = await _sqlDataAccess.GetDataAsync<TeamsDataModel, Dictionary<string, object>>("spTeams_GetById", param, true);
            return output.FirstOrDefault();
        }

        public async Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId)
        {
            Dictionary<string, object> param = new()
            {
                { "@TeamId", teamId }
            };
            IEnumerable<TeamMembersDataModel> output = await _sqlDataAccess.GetDataAsync<TeamMembersDataModel, Dictionary<string, object>>("spTeamMembers_GetByTeamId", param, true);
            return output;
        }

        public async Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamMemberId }
            };
            IEnumerable<TeamMembersDataModel> output = await _sqlDataAccess.GetDataAsync<TeamMembersDataModel, Dictionary<string, object>>("spTeamMembers_GetById", param, true);
            return output.FirstOrDefault();
        }

        public async Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId }
            };
            IEnumerable<QuestionsDataModel> output = await _sqlDataAccess.GetDataAsync<QuestionsDataModel, Dictionary<string, object>>("spQuestion_GetByGameId", param, true);
            return output;
        }

        public async Task<IEnumerable<AnswersDataModel>> GetAnswersDataAsync(int questionId)
        {
            Dictionary<string, object> param = new()
            {
                { "@QuestionId", questionId }
            };
            IEnumerable<AnswersDataModel> output = await _sqlDataAccess.GetDataAsync<AnswersDataModel, Dictionary<string, object>>("spAnswers_GetByQuestionId", param, true);
            return output;
        }

        public async Task<int> ShowAnswerAsync(AnswersDataModel answer)
        {
            answer.Visible = answer.Visible == 1 ? 0 : 1;
            Dictionary<string, object> param = new()
            {
                { "@Id", answer.Id },
                { "@Visible", answer.Visible }
            };
            var result = await _sqlDataAccess.PutDataAsync("spAnswers_UpdateVisibleById", param, true);
            return result;
        }

        public async Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", gameId },
                { "@TeamNumber", teamNumber },
                { "@Score", newScore }
            };

            var result = await _sqlDataAccess.PutDataAsync("spGames_UpdateTeamScoreById", param, true);
            return result;
        }

        public async Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@ActiveId", activeMemberId },
                { "@InactiveId", inactiveMemberId }
            };

            var result = await _sqlDataAccess.PutDataAsync("spTeamMembers_UpdateActiveById", param, true);
            return result;
        }

        public async Task<int> AddTeamMemberAsync(TeamMembersDataModel model)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", model.Id },
                { "@TeamId", model.TeamId },
                { "@Name", model.Name },
                { "@Active", model.Active }
            };

            var result = await _sqlDataAccess.PostDataAsync("spTeamMembers_Add", param, true);
            return result;
        }

        public async Task<int> RemoveTeamMemberAsync(int teamMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamMemberId }
            };

            var result = await _sqlDataAccess.DeleteDataAsync("spTeamMembers_RemoveById", param, true);
            return result;
        }

        public async Task<int> AddStrikeAsync(QuestionsDataModel model)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", model.Id },
                { "@Strikes", model.Strikes }
            };

            var result = await _sqlDataAccess.PutDataAsync("spQuestions_UpdateStrikesById", param, true);
            return result;
        }

        public async Task<int> ChangeTeamNamesAsync(TeamsDataModel model)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", model.Id },
                { "@TeamName", model.TeamName }
            };

            var result = await _sqlDataAccess.PutDataAsync("spTeams_UpdateTeamNameById", param, true);
            return result;
        }

        public async Task<int> ChangeTeamScoreAsync(GamesDataModel model)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", model.Id },
                { "@Team1Score", model.Team1Score },
                { "@Team2Score", model.Team2Score }
            };

            var result = await _sqlDataAccess.PutDataAsync("spGames_UpdateScoresById", param, true);
            return result;
        }
    }
}