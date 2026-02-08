CREATE TABLE [dbo].[RecipeIngredients]
(
    [RecipeId]          BIGINT          NOT NULL,
    [LocalId]           INT             NOT NULL,
    [Note]              NVARCHAR(256)   NOT NULL,
    [OrderIndex]        SMALLINT        NOT NULL DEFAULT 0

    CONSTRAINT [PK_dbo_RecipeIngredients] PRIMARY KEY CLUSTERED (
        [RecipeId] ASC,
        [LocalId] ASC
    )
    WITH (
        FILLFACTOR = 90
    ),

    CONSTRAINT [FK_dbo_RecipeIngredients_Recipes] FOREIGN KEY (
        [RecipeId]
    )
    REFERENCES [dbo].[Recipes] (
        [Id]
    )
    ON DELETE CASCADE
)
