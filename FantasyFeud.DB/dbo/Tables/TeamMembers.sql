CREATE TABLE [dbo].[TeamMembers]
(
    [Id] INT NOT NULL , 
    [TeamId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_TeamMembers] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_TeamMembers_Teams] FOREIGN KEY ([TeamId]) REFERENCES [Teams]([Id])
)
