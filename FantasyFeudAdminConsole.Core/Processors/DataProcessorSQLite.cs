using FantasyFeudAdminConsole.Core.Configuration;
using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessorSQLite : IDataProcessor
    {
        private readonly IDataAccess _sqliteDataAccess;

        public DataProcessorSQLite(IDataAccess sqliteDataAccess)
        {
            _sqliteDataAccess = sqliteDataAccess;
        }

        public async Task<int> AddStrikeAsync(QuestionsDataModel model)
        {
            var query = $"" +
                $"UPDATE Questions " +
                $"SET Strikes = {model.Strikes} " +
                $"WHERE Id = {model.Id}";

            var result = await _sqliteDataAccess.PutDataAsync(query);
            return result;
        }

        public async Task<int> AddTeamMemberAsync(TeamMembersDataModel model)
        {
            var query = $"" +
                $"INSERT INTO TeamMembers VALUES " +
                $"({model.Id}, {model.TeamId}, '{model.Name}', {model.Active});";

            var result = await _sqliteDataAccess.PostDataAsync(query);
            return result;
        }

        public async Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId)
        {
            var query = $"" +
                $"UPDATE TeamMembers " +
                $"SET Active = 0 " +
                $"WHERE Id = {inactiveMemberId}; " +
                $"" +
                $"UPDATE TeamMembers " +
                $"SET Active = 1 " +
                $"WHERE Id = {activeMemberId};";

            var result = await _sqliteDataAccess.PutDataAsync(query);
            return result;
        }

        public async Task<int> ChangeTeamNamesAsync(TeamsDataModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ChangeTeamScoreAsync(GamesDataModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnswersDataModel>> GetAnswersDataAsync(int questionId)
        {
            //TODO: Fix this!!!
            var query = "" +
                "SELECT * " +
                "FROM Answers " +
                $"WHERE QuestionId = {questionId}";

            IEnumerable<AnswersDataModel> data = await _sqliteDataAccess.GetDataAsync<AnswersDataModel>(query);
            return data;
        }

        public async Task<GamesDataModel> GetGameDataAsync(int gameId)
        {
            await CreateDatabase();

            var query = "" +
                "SELECT * " +
                "FROM Games " +
                $"WHERE Id = {gameId}";

            IEnumerable<GamesDataModel> data = await _sqliteDataAccess.GetDataAsync<GamesDataModel>(query);
            return data.FirstOrDefault();
        }

        public async Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId)
        {
            var query = "" +
                "SELECT * " +
                "FROM Questions " +
                $"WHERE GameId = {gameId}";

            IEnumerable<QuestionsDataModel> data = await _sqliteDataAccess.GetDataAsync<QuestionsDataModel>(query);
            return data;
        }

        public async Task<TeamsDataModel> GetTeamDataAsync(int teamId)
        {
            var query = "" +
                "SELECT * " +
                "FROM Teams " +
                $"WHERE Id = {teamId}";

            IEnumerable<TeamsDataModel> data = await _sqliteDataAccess.GetDataAsync<TeamsDataModel>(query);
            return data.FirstOrDefault();
        }

        public async Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId)
        {
            var query = "" +
                "SELECT * " +
                "FROM TeamMembers " +
                $"WHERE Id = {teamMemberId}";

            IEnumerable<TeamMembersDataModel> data = await _sqliteDataAccess.GetDataAsync<TeamMembersDataModel>(query);
            return data.FirstOrDefault();
        }

        public async Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId)
        {
            var query = "" +
                "SELECT * " +
                "FROM TeamMembers " +
                $"WHERE TeamId = {teamId} AND Name NOT LIKE ''";

            IEnumerable<TeamMembersDataModel> data = await _sqliteDataAccess.GetDataAsync<TeamMembersDataModel>(query);
            return data;
        }

        public async Task<int> RemoveTeamMemberAsync(int teamMemberId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ShowAnswerAsync(AnswersDataModel answer)
        {
            var query = $"" +
                $"UPDATE Answers " +
                $"SET Visible = {answer.Visible} " +
                $"WHERE Id = {answer.Id}";

            var result = await _sqliteDataAccess.PutDataAsync(query);
            return result;
        }

        private async Task<int> CreateDatabase()
        {
            if (!File.Exists(GlobalConfig.DatabaseSettings.DataSource))
            {
                try
                {
                    var query = "" +
                        "CREATE TABLE Teams ( " +
                        "   Id INTEGER NOT NULL PRIMARY KEY," +
                        "   TeamName TEXT NOT NULL); " +
                        "" +
                        "CREATE TABLE Games( " +
                        "   Id INTEGER NOT NULL PRIMARY KEY, " +
                        "   Team1Id INTEGER NOT NULL REFERENCES Teams, " +
                        "   Team1Score INTEGER NOT NULL DEFAULT 0, " +
                        "   Team2Id INTEGER NOT NULL REFERENCES Teams, " +
                        "   Team2Score INTEGER NOT NULL DEFAULT 0); " +
                        "" +
                        "CREATE TABLE TeamMembers( " +
                        "   Id INTEGER NOT NULL PRIMARY KEY, " +
                        "   TeamId INTEGER NOT NULL REFERENCES Teams, " +
                        "   Name TEXT NOT NULL, " +
                        "   Active INTEGER NOT NULL DEFAULT 0); " +
                        "" +
                        "CREATE TABLE Questions( " +
                        "   Id INTEGER NOT NULL PRIMARY KEY, " +
                        "   GameId INTEGER NOT NULL REFERENCES Games, " +
                        "   Question TEXT NOT NULL, " +
                        "   Responses INTEGER NOT NULL DEFAULT 0, " +
                        "   Strikes INTEGER NOT NULL DEFAULT 0); " +
                        "" +
                        "CREATE TABLE Answers( " +
                        "   Id INTEGER NOT NULL PRIMARY KEY, " +
                        "   QuestionId INTEGER NOT NULL REFERENCES Questions, " +
                        "   Rank INTEGER NOT NULL, " +
                        "   Answer TEXT NOT NULL, " +
                        "   Value INTEGER NOT NULL DEFAULT 0, " +
                        "   Visible INTEGER NOT NULL DEFAULT 0); " +
                        "" +
                        "INSERT INTO Teams VALUES " +
                        "   (0, 'Scions'), " +
                        "   (1, 'Garleans'); " +
                        "" +
                        "INSERT INTO TeamMembers VALUES " +
                        "   (0, 0, 'Thancred', 1), " +
                        "   (1, 0, 'Y''shtola', 0), " +
                        "   (2, 0, 'Gra''ha', 0), " +
                        "   (3, 1, 'Zenos', 1), " +
                        "   (4, 1, 'Fandaniel', 0), " +
                        "   (5, 1, 'Voidsent', 0); " +
                        "" +
                        "INSERT INTO Games VALUES " +
                        "   (0, 0, 0, 1, 0); " +
                        "" +
                        "INSERT INTO Questions VALUES " +
                        "   (0, 0, 'Who''s Ready For Fantasy Feud?', 6, 0); " +
                        "" +
                        "INSERT INTO Answers VALUES " +
                        "   (0, 0, 1, 'Me', 25, 0), " +
                        "   (1, 0, 2, 'You', 15, 0), " +
                        "   (2, 0, 3, 'Them', 10, 0), " +
                        "   (3, 0, 4, 'Us', 8, 0), " +
                        "   (4, 0, 5, 'Lalas', 4, 0), " +
                        "   (5, 0, 6, 'No One', 0, 0);";

                    var output = await _sqliteDataAccess.PostDataAsync(query);
                    Debug.WriteLine(output);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Error Creating DB");
                }
            }

            return 0;
        }
    }
}

/*
Creation of the database (with example data):
  
CREATE TABLE Teams (
	Id INTEGER NOT NULL PRIMARY KEY,
	TeamName TEXT NOT NULL
);

CREATE TABLE Games (
	Id INTEGER NOT NULL PRIMARY KEY,
	Team1Id INTEGER NOT NULL REFERENCES Teams,
	Team1Score INTEGER NOT NULL DEFAULT 0,
	Team2Id INTEGER NOT NULL REFERENCES Teams,
	Team2Score INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE TeamMembers (
	Id INTEGER NOT NULL PRIMARY KEY,
	TeamId INTEGER NOT NULL REFERENCES Teams,
	Name TEXT NOT NULL,
	Active INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE Questions (
	Id INTEGER NOT NULL PRIMARY KEY,
	GameId INTEGER NOT NULL REFERENCES Games,
	Question TEXT NOT NULL,
	Responses INTEGER NOT NULL DEFAULT 0,
	Strikes INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE Answers (
	Id INTEGER NOT NULL PRIMARY KEY,
	QuestionId INTEGER NOT NULL REFERENCES Questions,
	Rank INTEGER NOT NULL,
	Answer TEXT NOT NULL,
	Value INTEGER NOT NULL DEFAULT 0,
	Visible INTEGER NOT NULL DEFAULT 0
);


INSERT INTO Teams VALUES 
(0, 'Scions'),
(1, 'Garleans');

INSERT INTO TeamMembers VALUES 
(0, 0, 'Thancred', 1),
(1, 0, 'Y''shtola', 0),
(2, 0, 'Gra''ha', 0),
(3, 1, 'Zenos', 1),
(4, 1, 'Fandaniel', 0),
(5, 1, 'Voidsent', 0);

INSERT INTO Games VALUES
(0, 0, 0, 1, 0);

INSERT INTO Questions VALUES
(0, 0, 'Who''s Ready For Fantasy Feud?', 6, 0);

INSERT INTO Answers VALUES
(0, 0, 1, 'Me', 25, 0),
(1, 0, 2, 'You', 15, 0),
(2, 0, 3, 'Them', 10, 0),
(3, 0, 4, 'Us', 8, 0),
(4, 0, 5, 'Lalas', 4, 0),
(5, 0, 6, 'No One', 0, 0);
*/