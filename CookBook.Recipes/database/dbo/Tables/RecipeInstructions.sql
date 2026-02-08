CREATE TABLE [dbo].[RecipeInstructions]
(
    [RecipeId]          BIGINT          NOT NULL,
    [LocalId]           INT             NOT NULL,
    [Note]              NVARCHAR(1024)  NOT NULL,
    [OrderIndex]        SMALLINT        NOT NULL DEFAULT 0

    CONSTRAINT [PK_dbo_RecipeInstructions] PRIMARY KEY CLUSTERED (
        [RecipeId] ASC,
        [LocalId] ASC
    )
    WITH (
        FILLFACTOR = 90
    ),

    CONSTRAINT [FK_RecipeInstructions_Recipes] FOREIGN KEY (
        [RecipeId]
    )
    REFERENCES [dbo].[Recipes] (
        [Id]
    )
    ON DELETE CASCADE
);

GO;
