SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[foods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[diet_type] [int] NOT NULL,
	[description] [varchar](5000),
	[public_id] [varchar](50) NOT NULL,
	[public_id_int] [int] NOT NULL,
	[group] [varchar](50),
	[sub_group] [varchar](50),
	[created_date] [varchar](50) NOT NULL,
	[updated_date] [varchar](50) NOT NULL,
	[data_source] [int] NOT NULL,
	CONSTRAINT [UC_public_id_int] UNIQUE (public_id_int),
	CONSTRAINT [FK_diet_type] FOREIGN KEY (diet_type) REFERENCES [dbo].[diet_types](id),
	CONSTRAINT [FK_food_data_source] FOREIGN KEY (data_source) REFERENCES [dbo].[data_sources](id),
 CONSTRAINT [PK_foods] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
