CREATE TABLE [dbo].[Categories]
(
    [Id]                INT IDENTITY(1, 1) NOT NULL,
    [Name]              NVARCHAR(256) NOT NULL,
    [ParentCategoryId]  INT NOT NULL DEFAULT 0,
    [DateCreatedAt]     DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET,

     CONSTRAINT [PK_dbo_Categories] PRIMARY KEY CLUSTERED ([Id] ASC),
)
