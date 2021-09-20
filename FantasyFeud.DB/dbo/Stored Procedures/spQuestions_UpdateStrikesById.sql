CREATE PROCEDURE [dbo].[spQuestions_UpdateStrikesById]
    @Id int,
    @Strikes int
AS
BEGIN
    UPDATE dbo.Questions
    SET Strikes = @Strikes
    WHERE Id = @Id;
END