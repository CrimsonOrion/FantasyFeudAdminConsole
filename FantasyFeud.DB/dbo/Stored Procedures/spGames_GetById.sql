CREATE PROCEDURE [dbo].[spGames_GetById]
    @Id int
AS
BEGIN
    SELECT Id, Team1Id, Team1Score, Team2Id, Team2Score
    FROM dbo.Games
    WHERE Id = @Id;
END