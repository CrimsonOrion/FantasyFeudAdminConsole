CREATE PROCEDURE [dbo].[spQuestions_GetByGameId]
    @GameId int
AS
BEGIN
    SELECT Id, GameId, Question, Responses, Strikes
    FROM dbo.Questions
    WHERE GameId = @GameId;
END