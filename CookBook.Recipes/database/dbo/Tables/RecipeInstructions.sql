CREATE TABLE [dbo].[RecipeInstructions]
(
    [Id]                TINYINT IDENTITY(1, 1) NOT NULL,
    [RecipeId]          BIGINT NOT NULL,
    [Note]              NVARCHAR(1024) NOT NULL,
    [OrderIndex]        TINYINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_dbo_RecipeInstructions] PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_RecipeInstructions_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE
)
