CREATE PROCEDURE [dbo].[spAnswers_GetByQuestionId]
    @QuestionId int
AS
BEGIN
    SELECT Id, QuestionId, Rank, Answer, Value, Visible
    FROM dbo.Answers
    WHERE QuestionId = @QuestionId;
END