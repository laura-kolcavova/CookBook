CREATE TABLE [dbo].[Users]
(
    [Id]                INT IDENTITY(1, 1)  NOT NULL,
    [IdentityUserId]    INT                 NOT NULL,
    [UserNumber]        INT                 NOT NULL,
    [DisplayName]       NVARCHAR(50)        NOT NULL,
    [DateCreatedAt]     DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [DateUpdatedAt]     DATETIMEOFFSET      NULL,

    CONSTRAINT [PK_dbo_Users] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
    WITH (
        FILLFACTOR = 90
    )
);

GO;

CREATE UNIQUE NONCLUSTERED INDEX [UX_dbo_Users_UserNumber]
ON [dbo].[Users] (
    [UserNumber] ASC
)
-- INCLUDE(
--     [DisplayName]
-- )
WITH (
    FILLFACTOR = 90
);

GO;
