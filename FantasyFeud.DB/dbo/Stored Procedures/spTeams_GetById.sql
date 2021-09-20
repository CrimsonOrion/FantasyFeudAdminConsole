CREATE PROCEDURE [dbo].[spTeams_GetById]
    @Id int
AS
BEGIN
    SELECT Id, TeamName
    FROM dbo.Teams
    WHERE Id = @Id;
END