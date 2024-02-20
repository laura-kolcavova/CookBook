CREATE TABLE [dbo].[Recipes]
(
    [Id]                BIGINT IDENTITY(1, 1) NOT NULL,
    [UserId]            INT NOT NULL,
    [Title]             NVARCHAR(256) NOT NULL,
    [Description]       NVARCHAR(1024),
    [Notes]             NVARCHAR(1024),
    [Servings]          SMALLINT NOT NULL DEFAULT 0,
    [PreparationTime]   SMALLINT NOT NULL DEFAULT 0,
    [CookTime]          SMALLINT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

    CONSTRAINT [PK_dbo_Recipes] PRIMARY KEY CLUSTERED ([Id] ASC)
)
