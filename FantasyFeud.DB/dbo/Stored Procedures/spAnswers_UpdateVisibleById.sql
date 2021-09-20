CREATE PROCEDURE [dbo].[spAnswers_UpdateVisibleById]
    @Id int,
    @Visible bit
AS
BEGIN
    UPDATE dbo.Answers
    SET Visible = @Visible
    WHERE Id = @Id;
END