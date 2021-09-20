CREATE PROCEDURE [dbo].[spTeams_UpdateTeamNameById]
    @Id int,
    @TeamName nvarchar(50)
AS
BEGIN
    UPDATE dbo.Teams
    SET TeamName = @TeamName
    WHERE Id = @Id;
END