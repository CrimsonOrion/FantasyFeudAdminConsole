CREATE TABLE [dbo].[Answers]
(
    [Id] INT NOT NULL , 
    [QuestionId] INT NOT NULL, 
    [Rank] INT NOT NULL, 
    [Answer] NVARCHAR(50) NOT NULL, 
    [Value] INT NOT NULL, 
    [Visible] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Answers_Questions] FOREIGN KEY (QuestionId) REFERENCES [Questions]([Id])
)
