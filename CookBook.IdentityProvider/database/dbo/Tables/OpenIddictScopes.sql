USE [CookBookUsers]
GO

/****** Object:  Table [dbo].[OpenIddictScopes]    Script Date: 2/21/2026 12:35:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OpenIddictScopes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConcurrencyToken] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Descriptions] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[DisplayNames] [nvarchar](max) NULL,
	[Name] [nvarchar](200) NULL,
	[Properties] [nvarchar](max) NULL,
	[Resources] [nvarchar](max) NULL,
 CONSTRAINT [PK_OpenIddictScopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


