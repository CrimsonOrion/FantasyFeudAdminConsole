CREATE PROCEDURE [dbo].[spTeamMembers_GetById]
    @Id int
AS
BEGIN
    SELECT Id, TeamId, Name, Active
    FROM dbo.TeamMembers
    WHERE Id = @Id;
END