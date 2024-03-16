CREATE TABLE [dbo].[RecipeIngredients]
(
    [RecipeId]          BIGINT NOT NULL,
    [LocalId]           INT NOT NULL,
    [Note]              NVARCHAR(256) NOT NULL,
    [OrderIndex]        SMALLINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_dbo_RecipeIngredients] PRIMARY KEY CLUSTERED ([RecipeId] ASC, [LocalId] ASC),

    CONSTRAINT [FK_dbo_RecipeIngredients_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE
)
