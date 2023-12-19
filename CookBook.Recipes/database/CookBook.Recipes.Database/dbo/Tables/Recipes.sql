CREATE TABLE [dbo].[Recipes]
(
    [Id]                BIGINT IDENTITY(1, 1) NOT NULL,
    [UserId]            BIGINT NOT NULL,
    [Title]             NVARCHAR(256) NOT NULL,
    [Description]       NVARCHAR(1024),
    [Notes]             NVARCHAR(1024),
    [Servings]          TINYINT NOT NULL DEFAULT 0,
    [PreparationTime]   TINYINT NOT NULL DEFAULT 0,
    [CookTime]          TINYINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
