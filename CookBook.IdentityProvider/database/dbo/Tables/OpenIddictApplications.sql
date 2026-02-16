USE [CookBookUsers]
GO

/****** Object:  Table [dbo].[OpenIddictApplications]    Script Date: 2/21/2026 12:32:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OpenIddictApplications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationType] [nvarchar](50) NULL,
	[ClientId] [nvarchar](100) NULL,
	[ClientSecret] [nvarchar](max) NULL,
	[ClientType] [nvarchar](50) NULL,
	[ConcurrencyToken] [nvarchar](50) NULL,
	[ConsentType] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[DisplayNames] [nvarchar](max) NULL,
	[JsonWebKeySet] [nvarchar](max) NULL,
	[Permissions] [nvarchar](max) NULL,
	[PostLogoutRedirectUris] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
	[RedirectUris] [nvarchar](max) NULL,
	[Requirements] [nvarchar](max) NULL,
	[Settings] [nvarchar](max) NULL,
 CONSTRAINT [PK_OpenIddictApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


