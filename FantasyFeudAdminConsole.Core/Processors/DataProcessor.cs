using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessor : IDataProcessor
    {
        private readonly ISqlDataAccess _sqlDataAccess;

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
            IEnumerable<GamesDataModel> output = await _sqlDataAccess.GetDataAsync<GamesDataModel, Dictionary<string, object>>("spGames_GetById", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<TeamsDataModel> GetTeamDataAsync(int teamId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamId }
            };
            IEnumerable<TeamsDataModel> output = await _sqlDataAccess.GetDataAsync<TeamsDataModel, Dictionary<string, object>>("spTeams_GetById", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId)
        {
            Dictionary<string, object> param = new()
            {
                { "@TeamId", teamId }
            };
            IEnumerable<TeamMembersDataModel> output = await _sqlDataAccess.GetDataAsync<TeamMembersDataModel, Dictionary<string, object>>("spTeamMembers_GetByTeamId", MsSqlConnectionString.ConnectionString, param, true);
            return output;
        }

        public async Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamMemberId }
            };
            IEnumerable<TeamMembersDataModel> output = await _sqlDataAccess.GetDataAsync<TeamMembersDataModel, Dictionary<string, object>>("spTeamMembers_GetById", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId)
        {
            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId }
            };
            IEnumerable<QuestionsDataModel> output = await _sqlDataAccess.GetDataAsync<QuestionsDataModel, Dictionary<string, object>>("spQuestion_GetByGameId", MsSqlConnectionString.ConnectionString, param, true);
            return output;
        }

        public async Task<AnswersDataModel> GetAnswersDataAsync(int questionId)
        {
            Dictionary<string, object> param = new()
            {
                { "@QuestionId", questionId }
            };
            IEnumerable<AnswersDataModel> output = await _sqlDataAccess.GetDataAsync<AnswersDataModel, Dictionary<string, object>>("spAnswers_GetByQuestionId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<int> ShowAnswerAsync(AnswersDataModel answer)
        {
            answer.Visible = answer.Visible == 1 ? 0 : 1;
            Dictionary<string, object> param = new()
            {
                { "@Id", answer.Id },
                { "@Visible", answer.Visible }
            };
            var result = await _sqlDataAccess.PutDataAsync("spAnswers_UpdateVisibleById", MsSqlConnectionString.ConnectionString, param, true);
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

            var result = await _sqlDataAccess.PutDataAsync("spGames_UpdateTeamScoreById", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }

        public async Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@ActiveId", activeMemberId },
                { "@InactiveId", inactiveMemberId }
            };

            var result = await _sqlDataAccess.PutDataAsync("spTeamMembers_UpdateActiveById", MsSqlConnectionString.ConnectionString, param, true);
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

            var result = await _sqlDataAccess.PostDataAsync("spTeamMembers_Add", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }

        public async Task<int> RemoveTeamMemberAsync(int teamMemberId)
        {
            Dictionary<string, object> param = new()
            {
                { "@Id", teamMemberId }
            };

            var result = await _sqlDataAccess.DeleteDataAsync("spTeamMembers_RemoveById", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }
    }
}