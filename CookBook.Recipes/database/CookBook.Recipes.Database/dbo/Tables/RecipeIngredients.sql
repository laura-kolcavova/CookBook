CREATE TABLE [dbo].[RecipeIngredients]
(
    [Id]                TINYINT IDENTITY(1, 1) NOT NULL,
    [RecipeId]          BIGINT NOT NULL,
    [Note]              NVARCHAR(500) NOT NULL,
    [OrderIndex]        TINYINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_RecipeIngredients_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE
)
