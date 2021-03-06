SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[diet_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[level] [int] NOT NULL DEFAULT 0
	CONSTRAINT CHK_LEVEL
	CHECK (level > -1 AND level < 3),
 CONSTRAINT [PK_diet_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
