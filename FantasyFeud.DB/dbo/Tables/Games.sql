CREATE TABLE [dbo].[Games]
(
    [Id] INT NOT NULL, 
    [Team1Id] INT NOT NULL, 
    [Team1Score] INT NOT NULL DEFAULT 0, 
    [Team2Id] INT NOT NULL, 
    [Team2Score] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Games] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Games_Teams1] FOREIGN KEY([Team1Id]) REFERENCES [Teams]([Id]),
    CONSTRAINT [FK_Games_Teams2] FOREIGN KEY([Team2Id]) REFERENCES [Teams]([Id])
)
