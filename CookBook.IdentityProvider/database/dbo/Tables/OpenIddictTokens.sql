CREATE TABLE [dbo].[OpenIddictTokens](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [ApplicationId] [int] NULL,
    [AuthorizationId] [int] NULL,
    [ConcurrencyToken] [nvarchar](50) NULL,
    [CreationDate] [datetime2](7) NULL,
    [ExpirationDate] [datetime2](7) NULL,
    [Payload] [nvarchar](max) NULL,
    [Properties] [nvarchar](max) NULL,
    [RedemptionDate] [datetime2](7) NULL,
    [ReferenceId] [nvarchar](100) NULL,
    [Status] [nvarchar](50) NULL,
    [Subject] [nvarchar](400) NULL,
    [Type] [nvarchar](150) NULL,

    CONSTRAINT [PK_OpenIddictTokens] PRIMARY KEY CLUSTERED (
        [Id] ASC
    )
    WITH (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON,
        OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
    ON [PRIMARY]
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

GO;

ALTER TABLE [dbo].[OpenIddictTokens]
WITH CHECK ADD CONSTRAINT [FK_OpenIddictTokens_OpenIddictApplications_ApplicationId]
FOREIGN KEY(
    [ApplicationId]
)
REFERENCES [dbo].[OpenIddictApplications] (
    [Id]
);

GO;

ALTER TABLE [dbo].[OpenIddictTokens]
CHECK CONSTRAINT [FK_OpenIddictTokens_OpenIddictApplications_ApplicationId];

GO;

ALTER TABLE [dbo].[OpenIddictTokens]
WITH CHECK ADD CONSTRAINT [FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId]
FOREIGN KEY(
    [AuthorizationId]
)
REFERENCES [dbo].[OpenIddictAuthorizations] (
    [Id]
);

GO;

ALTER TABLE [dbo].[OpenIddictTokens]
CHECK CONSTRAINT [FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId];

GO;
