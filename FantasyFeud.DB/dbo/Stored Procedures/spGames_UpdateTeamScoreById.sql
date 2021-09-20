CREATE PROCEDURE [dbo].[spGames_UpdateTeamScoreById]
    @Id int,
    @TeamNumber int,
    @Score int
AS
IF @TeamNumber = 1
BEGIN
    UPDATE dbo.Games
    SET Team1Score = @Score
    WHERE Id = @Id;
END
ELSE
BEGIN
    UPDATE dbo.Games
    SET Team2Score = @Score
    WHERE Id = @Id;
END