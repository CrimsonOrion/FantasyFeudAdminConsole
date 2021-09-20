CREATE PROCEDURE [dbo].[spGames_UpdateScoresById]
    @Id int,
    @Team1Score int,
    @Team2Score int
AS
BEGIN
    UPDATE dbo.Games
    SET Team1Score = @Team1Score, Team2Score = @Team2Score
    WHERE Id = @Id;
END