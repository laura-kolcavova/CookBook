USE [CookBookUsers]
GO

/****** Object:  Table [dbo].[OpenIddictAuthorizations]    Script Date: 2/21/2026 12:35:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OpenIddictAuthorizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [int] NULL,
	[ConcurrencyToken] [nvarchar](50) NULL,
	[CreationDate] [datetime2](7) NULL,
	[Properties] [nvarchar](max) NULL,
	[Scopes] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[Subject] [nvarchar](400) NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_OpenIddictAuthorizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OpenIddictAuthorizations]  WITH CHECK ADD  CONSTRAINT [FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[OpenIddictApplications] ([Id])
GO

ALTER TABLE [dbo].[OpenIddictAuthorizations] CHECK CONSTRAINT [FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId]
GO


