SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[compounds](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[public_id] [varchar](50),
	[food_id] [int] NOT NULL,
	[created_date] [varchar](50) NOT NULL,
	[updated_date] [varchar](50) NOT NULL,
	[data_source] [int] NOT NULL,
	CONSTRAINT [FK_food_id] FOREIGN KEY (food_id) REFERENCES [dbo].[foods](public_id_int),
	CONSTRAINT [FK_compound_data_source] FOREIGN KEY (data_source) REFERENCES [dbo].[data_sources](id),
 CONSTRAINT [PK_compounds] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
