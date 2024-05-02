CREATE TABLE [dbo].[RecipeTags]
(
    [RecipeId]          BIGINT NOT NULL,
    [Name]              NVARCHAR(256) NOT NULL,

    CONSTRAINT [PK_dbo_RecipeTags] PRIMARY KEY CLUSTERED ([RecipeId] ASC, [Name] ASC),

    CONSTRAINT [FK_dbo_RecipeTags_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE
)
