CREATE TABLE [dbo].[Users] (
    [Id]                INT IDENTITY(1, 1)  NOT NULL,
    [IdentityUserId]    INT                 NOT NULL,
    [UserNumber]        UNIQUEIDENTIFIER    NOT NULL,
    [DisplayName]       NVARCHAR(50)        NOT NULL,
    [CreatedAt]         DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [UpdatedAt]         DATETIMEOFFSET      NULL,

    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
    WITH (
        FILLFACTOR = 90
    )
);

GO;

CREATE UNIQUE NONCLUSTERED INDEX [UX_Users_UserNumber]
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
