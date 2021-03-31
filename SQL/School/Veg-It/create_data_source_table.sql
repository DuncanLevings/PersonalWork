SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[data_sources](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[source_ID] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[url] [varchar](500) NOT NULL,
 CONSTRAINT [PK_data_sources] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
