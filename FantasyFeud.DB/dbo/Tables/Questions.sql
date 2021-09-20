CREATE TABLE [dbo].[Questions]
(
    [Id] INT NOT NULL , 
    [GameId] INT NOT NULL, 
    [Question] NVARCHAR(50) NOT NULL, 
    [Responses] INT NOT NULL, 
    [Strikes] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Questions_Games] FOREIGN KEY (GameId) REFERENCES [Games]([Id]), 
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
)
