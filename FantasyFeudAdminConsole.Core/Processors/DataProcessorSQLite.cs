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

namespace FantasyFeudAdminConsole.Core.Processors;

public class DataProcessorSQLite(IDataAccess sqliteDataAccess) : IDataProcessor
{
    public async Task<int> AddStrikeAsync(QuestionsDataModel model)
    {
        var query = $"""
            UPDATE Questions
            SET Strikes = {model.Strikes}
            WHERE Id = {model.Id};
            """;

        var result = await sqliteDataAccess.PutDataAsync(query);
        return result;
    }

    public async Task<int> AddTeamMemberAsync(TeamMembersDataModel model)
    {
        var query = $"""
            INSERT INTO TeamMembers (TeamId, Name, Active) VALUES
            ({model.TeamId}, '{model.Name?.EscapeName()}', {model.Active});
            """;

        var result = await sqliteDataAccess.PostDataAsync(query);
        return result;
    }

    public async Task<int> AwardPointsAsync(int gameId, int teamNumber, int newScore)
    {
        var query = $"""
            UPDATE Games
            SET Team{teamNumber}Score = {newScore}
            WHERE Id = {gameId};
            """;

        var results = await sqliteDataAccess.PutDataAsync(query);
        return results;
    }

    public async Task<int> ChangeActiveMemberAsync(int inactiveMemberId, int activeMemberId)
    {
        var query = $"""
            UPDATE TeamMembers
            SET Active = 0
            WHERE Id = {inactiveMemberId};

            UPDATE TeamMembers
            SET Active = 1
            WHERE Id = {activeMemberId};
            """;

        var result = await sqliteDataAccess.PutDataAsync(query);
        return result;
    }

    public async Task<int> ChangeTeamNamesAsync(TeamsDataModel model)
    {
        var query = $"""
            UPDATE Teams
            SET TeamName = {model.TeamName?.EscapeName()}
            WHERE Id = {model.Id};
            """;

        var result = await sqliteDataAccess.PutDataAsync(query);
        return result;
    }

    public async Task<int> ChangeTeamScoreAsync(GamesDataModel model)
    {
        var query = $"""
            UPDATE Games
            SET Team1Score = {model.Team1Score}, Team2Score = {model.Team2Score}
            WHERE Id = {model.Id};
            """;

        var result = await sqliteDataAccess.PutDataAsync(query);
        return result;
    }

    public async Task<IEnumerable<AnswersDataModel>> GetAnswersDataAsync(int questionId)
    {
        var query = $"""
            SELECT Id, QuestionId, Rank, Answer, Value, Visible
            FROM Answers
            WHERE QuestionId = {questionId};
            """;

        IEnumerable<AnswersDataModel> data = await sqliteDataAccess.GetDataAsync<AnswersDataModel>(query);
        return data;
    }

    public async Task<GamesDataModel> GetGameDataAsync(int gameId)
    {
        //await CreateDatabaseAsync();

        var query = $"""
            SELECT Id, Team1Id, Team1Score, Team2Id, Team2Score
            FROM Games
            WHERE Id = {gameId};
            """;

        IEnumerable<GamesDataModel> data = await sqliteDataAccess.GetDataAsync<GamesDataModel>(query);
        return data.FirstOrDefault()
            ?? new();
    }

    public async Task<IEnumerable<QuestionsDataModel>> GetQuestionsDataAsync(int gameId)
    {
        var query = $"""
            SELECT Id, GameId, Question, Responses, Strikes
            FROM Questions
            WHERE GameId = {gameId};
            """;

        IEnumerable<QuestionsDataModel> data = await sqliteDataAccess.GetDataAsync<QuestionsDataModel>(query);
        return data;
    }

    public async Task<TeamsDataModel> GetTeamDataAsync(int teamId)
    {
        var query = $"""
            SELECT Id, TeamName
            FROM Teams
            WHERE Id = {teamId};
            """;

        IEnumerable<TeamsDataModel> data = await sqliteDataAccess.GetDataAsync<TeamsDataModel>(query);
        return data.FirstOrDefault()
            ?? new();
    }

    public async Task<TeamMembersDataModel> GetTeamMemberDataAsync(int teamMemberId)
    {
        var query = $"""
            SELECT Id, TeamId, Name, Active
            FROM TeamMembers
            WHERE Id = {teamMemberId};
            """;

        IEnumerable<TeamMembersDataModel> data = await sqliteDataAccess.GetDataAsync<TeamMembersDataModel>(query);
        return data.FirstOrDefault()
            ?? new();
    }

    public async Task<IEnumerable<TeamMembersDataModel>> GetTeamMembersDataAsync(int teamId)
    {
        var query = $"""
            SELECT Id, TeamId, Name, Active
            FROM TeamMembers
            WHERE TeamId = {teamId} AND Name NOT LIKE '';
            """;

        IEnumerable<TeamMembersDataModel> data = await sqliteDataAccess.GetDataAsync<TeamMembersDataModel>(query);
        return data;
    }

    public async Task<int> RemoveTeamMemberAsync(int teamMemberId)
    {
        var query = $"""
            DELETE FROM TeamMembers
            WHERE Id = {teamMemberId};
            """;

        var result = await sqliteDataAccess.DeleteDataAsync(query);
        return result;
    }

    public async Task<int> ShowAnswerAsync(AnswersDataModel answer)
    {
        var query = $"""
            UPDATE Answers
            SET Visible = {answer.Visible}
            WHERE Id = {answer.Id};
            """;

        var result = await sqliteDataAccess.PutDataAsync(query);
        return result;
    }

    public async Task<bool> UpdateAllDataAsync(TeamsDataModel team1, TeamsDataModel team2, GamesDataModel games, IEnumerable<TeamMembersDataModel> team1Members, IEnumerable<TeamMembersDataModel> team2Members, QuestionsDataModel questions, IEnumerable<AnswersDataModel> answers)
    {
        StringBuilder queryBuilder = new();
        _ = queryBuilder.AppendLine($"""
            UPDATE Teams
            SET TeamName = '{team1.TeamName?.EscapeName()}'
            WHERE Id = {team1.Id};

            UPDATE Teams
            SET TeamName = '{team2.TeamName?.EscapeName()}'
            WHERE Id = {team2.Id};

            UPDATE Games
            SET Team1Id = {games.Team1Id}, Team1Score = {games.Team1Score}, Team2Id = {games.Team2Id}, Team2Score = {games.Team2Score}
            WHERE Id = {games.Id};

            DELETE FROM TeamMembers;
                
            INSERT INTO TeamMembers (TeamId, Name, Active) VALUES
            """);

        foreach (TeamMembersDataModel member in team1Members)
        {
            _ = queryBuilder.Append($"({member.TeamId},'{member.Name?.EscapeName()}',{member.Active}),");
        }

        foreach (TeamMembersDataModel member in team2Members)
        {
            _ = queryBuilder.Append($"({member.TeamId},'{member.Name?.EscapeName()}',{member.Active}),");
        }
        _ = queryBuilder.Remove(queryBuilder.Length - 1, 1).Append(';')

            .AppendLine($"""
                UPDATE Questions
                SET Strikes = {questions.Strikes}
                WHERE Id = {questions.Id};
                """);

        foreach (AnswersDataModel answer in answers)
        {
            _ = queryBuilder.AppendLine($"""
                UPDATE Answers
                SET Visible = {answer.Visible}
                WHERE Id = {answer.Id};
                """);
        }

        try
        {
            _ = await sqliteDataAccess.PutDataAsync(queryBuilder.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<int> CreateDatabaseAsync()
    {
        if (!File.Exists(GlobalConfig.DatabaseSettings?.DataSource))
        {
            try
            {
                var query = """
                    CREATE TABLE Teams (
                        Id INTEGER NOT NULL PRIMARY KEY,
                        TeamName TEXT NOT NULL);
                    CREATE TABLE Games (
                        Id INTEGER NOT NULL PRIMARY KEY,
                        Team1Id INTEGER NOT NULL REFERENCES Teams,
                        Team1Score INTEGER NOT NULL DEFAULT 0,
                        Team2Id INTEGER NOT NULL REFERENCES Teams,
                        Team2Score INTEGER NOT NULL DEFAULT 0);
                    CREATE TABLE TeamMembers (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        TeamId INTEGER NOT NULL REFERENCES Teams,
                        Name TEXT NOT NULL,
                        Active INTEGER NOT NULL DEFAULT 0);
                    CREATE TABLE Questions (
                        Id INTEGER NOT NULL PRIMARY KEY,
                        GameId INTEGER NOT NULL REFERENCES Games,
                        Question TEXT NOT NULL,
                        Responses INTEGER NOT NULL DEFAULT 0,
                        Strikes INTEGER NOT NULL DEFAULT 0);
                    CREATE TABLE Answers (
                        Id INTEGER NOT NULL PRIMARY KEY,
                        QuestionId INTEGER NOT NULL REFERENCES Questions,
                        Rank INTEGER NOT NULL,
                        Answer TEXT NOT NULL,
                        Value INTEGER NOT NULL DEFAULT 0,
                        Visible INTEGER NOT NULL DEFAULT 0);
                    INSERT INTO Teams VALUES
                        (0, 'Scions'),
                        (1, 'Garleans');
                    INSERT INTO TeamMembers (TeamId, Name, Active) VALUES
                        (0, 'Thancred', 1),
                        (0, 'Y''shtola', 0),
                        (0, 'Gra''ha', 0),
                        (1, 'Zenos', 1),
                        (1, 'Fandaniel', 0),
                        (1, 'Voidsent', 0);
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
                    """;
                var output = await sqliteDataAccess.PostDataAsync(query);
                Debug.WriteLine($"DB Creation successful with Code: {output}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error Creating DB: {ex.Message}");
            }
        }

        return 0;
    }
}