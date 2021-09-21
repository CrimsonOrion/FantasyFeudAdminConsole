using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessorSQLite : IDataProcessor
    {
        private IDataAccess _sqliteDataAccess;

        public DataProcessorSQLite(IDataAccess sqliteDataAccess)
        {
            _sqliteDataAccess = sqliteDataAccess;
        }

        public Task<int> AddStrikeAsync(QuestionsDataModel model)
        {
            
        }

        public Task<int> AddTeamMemberAsync(TeamMembersDataModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore)
        {
            throw new NotImplementedException();
        }

        public Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId)
        {
            throw new NotImplementedException();
        }

        public Task<int> ChangeTeamNamesAsync(TeamsDataModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> ChangeTeamScoreAsync(GamesDataModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AnswersDataModel>> GetAnswersDataAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<GamesDataModel> GetGameDataAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamsDataModel> GetTeamDataAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveTeamMemberAsync(int teamMemberId)
        {
            throw new NotImplementedException();
        }

        public Task<int> ShowAnswerAsync(AnswersDataModel answer)
        {
            throw new NotImplementedException();
        }
    }
}