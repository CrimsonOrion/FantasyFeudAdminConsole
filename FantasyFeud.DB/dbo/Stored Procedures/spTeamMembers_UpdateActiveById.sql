CREATE PROCEDURE [dbo].[spTeamMembers_UpdateActiveById]
    @ActiveId int,
    @InactiveId int
AS
BEGIN
    UPDATE dbo.TeamMembers
    SET Active = 1
    WHERE Id = @ActiveId;

    UPDATE dbo.TeamMembers
    SET Active = 0
    WHERE Id = @InactiveId;
END