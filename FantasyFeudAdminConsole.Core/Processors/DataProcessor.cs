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
            IEnumerable<QuestionsDataModel> output = await _sqlDataAccess.GetDataAsync<QuestionsDataModel, Dictionary<string, object>>("spQuestion_GetByGameIdAndQuestionId", MsSqlConnectionString.ConnectionString, param, true);
            return output.FirstOrDefault();
        }

        public async Task<AnswerDataModel> GetAnswerDataAsync(int questionId)
        {
            Dictionary<string, object> param = new()
            {
                { "@QuestionId", questionId }
            };
            IEnumerable<AnswerDataModel> output = await _sqlDataAccess.GetDataAsync<AnswerDataModel, Dictionary<string, object>>("spAnswers_GetByQuestionId", MsSqlConnectionString.ConnectionString, param, true);
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
            var result = await _sqlDataAccess.PutDataAsync("spAnswers_UpdateById", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }

        public async Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore)
        {
            string storedProcedure;
            if (teamNumber == 1)
            {
                storedProcedure = "spGames_UpdateTeam1ScoreByGameId";
            }
            else
            {
                storedProcedure = "spGames_UpdateTeam2ScoreByGameId";
            }

            Dictionary<string, object> param = new()
            {
                { "@GameId", gameId },
                { "@Score", newScore }
            };

            var result = await _sqlDataAccess.PutDataAsync(storedProcedure, MsSqlConnectionString.ConnectionString, param, true);
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

            var result = await _sqlDataAccess.PutDataAsync("spTeamMembers_RemoveById", MsSqlConnectionString.ConnectionString, param, true);
            return result;
        }
    }
}