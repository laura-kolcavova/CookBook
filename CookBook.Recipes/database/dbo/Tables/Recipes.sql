CREATE TABLE [dbo].[Recipes]
(
    [Id]                BIGINT IDENTITY(1, 1)   NOT NULL,
    [UserName]          NVARCHAR(256)           NOT NULL,
    [Title]             NVARCHAR(256)           NOT NULL,
    [Description]       NVARCHAR(1024)          NULL,
    [Notes]             NVARCHAR(1024)          NULL,
    [Servings]          SMALLINT                NOT NULL DEFAULT 0,
    [CookTime]          SMALLINT                NOT NULL DEFAULT 0,
    [CreatedAt]         DATETIMEOFFSET          NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [UpdatedAt]         DATETIMEOFFSET          NULL,

    CONSTRAINT [PK_dbo_Recipes] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
    WITH (
        FILLFACTOR = 90
    )
);

GO;
