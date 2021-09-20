CREATE PROCEDURE [dbo].[spTeamMembers_Add]
    @Id int,
    @TeamId int,
    @Name nvarchar(50),
    @Active bit
AS
BEGIN
    INSERT INTO dbo.TeamMembers
    VALUES (@Id, @TeamId, @Name, @Active);
END