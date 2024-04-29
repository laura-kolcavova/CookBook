CREATE TABLE [dbo].[RecipeCategories]
(
    [RecipeID]          BIGINT NOT NULL,
    [CategoryId]        INT NOT NULL,

    CONSTRAINT [PK_dbo_RecipeCategories] PRIMARY KEY CLUSTERED ([RecipeId] ASC, [CategoryId] ASC),

    CONSTRAINT [FK_RecipeCategories_Recipes] FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[Recipes] ([Id]) ON DELETE CASCADE,

    CONSTRAINT [FK_RecipeCategories_Categories] FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE
)
