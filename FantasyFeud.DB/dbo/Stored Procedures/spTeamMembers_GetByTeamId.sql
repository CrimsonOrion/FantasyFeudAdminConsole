CREATE PROCEDURE [dbo].[spTeamMembers_GetByTeamId]
    @TeamId int
AS
BEGIN
    SELECT Id, TeamId, Name, Active
    FROM dbo.TeamMembers
    WHERE TeamId = @TeamId;
END