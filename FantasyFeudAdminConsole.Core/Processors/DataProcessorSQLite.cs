using FantasyFeudAdminConsole.Core.Configuration;
using FantasyFeudAdminConsole.Core.DataAccess;
using FantasyFeudAdminConsole.Core.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class DataProcessorSQLite : IDataProcessor
    {
        private IDataAccess _sqliteDataAccess;

        public DataProcessorSQLite(IDataAccess sqliteDataAccess)
        {
            _sqliteDataAccess = sqliteDataAccess;
            CreateDatabase();
        }

        public Task<int> AddStrikeAsync(QuestionsDataModel model)
        {
            throw new NotImplementedException();
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

        async Task<int> CreateDatabase()
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