CREATE PROCEDURE [dbo].[spTeamMembers_RemoveById]
    @Id int
AS
BEGIN
    DELETE
    FROM dbo.TeamMembers
    WHERE Id = @Id;
END