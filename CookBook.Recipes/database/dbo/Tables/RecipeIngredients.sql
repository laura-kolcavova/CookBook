CREATE TABLE [dbo].[RecipeIngredients]
(
    [Id]                BIGINT IDENTITY(1, 1) NOT NULL,
    [RecipeId]          BIGINT NOT NULL,
    [Note]              NVARCHAR(256) NOT NULL,
    [OrderIndex]        SMALLINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_dbo_RecipeIngredients] PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_dbo_RecipeIngredients_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE
)
