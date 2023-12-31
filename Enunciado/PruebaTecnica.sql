USE [PruebaTecnica]
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 9/3/2020 21:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[IdArticulo] [int] NULL,
	[Descripcion] [varchar](50) NULL,
	[IdMarca] [int] NULL,
	[Sku] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 9/3/2020 21:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[IdMarca] [int] NULL,
	[Descripcion] [varchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Articulo] ([IdArticulo], [Descripcion], [IdMarca], [Sku]) VALUES (1, N'Refri', 1, 12345)
INSERT [dbo].[Articulo] ([IdArticulo], [Descripcion], [IdMarca], [Sku]) VALUES (2, N'Celular ', 4, 23456)
INSERT [dbo].[Articulo] ([IdArticulo], [Descripcion], [IdMarca], [Sku]) VALUES (3, N'Cocina', 3, 34567)
INSERT [dbo].[Articulo] ([IdArticulo], [Descripcion], [IdMarca], [Sku]) VALUES (4, N'Microondas', 2, 45678)
INSERT [dbo].[Marca] ([IdMarca], [Descripcion]) VALUES (1, N'Telstar')
INSERT [dbo].[Marca] ([IdMarca], [Descripcion]) VALUES (2, N'LG')
INSERT [dbo].[Marca] ([IdMarca], [Descripcion]) VALUES (3, N'MABE')
INSERT [dbo].[Marca] ([IdMarca], [Descripcion]) VALUES (4, N'Huawei')
/****** Object:  StoredProcedure [dbo].[usp_ConsultaArticulos]    Script Date: 9/3/2020 21:51:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_ConsultaArticulos]
AS
BEGIN

	SET NOCOUNT ON;

    
	SELECT a.IdArticulo, 
		   a.Descripcion,
		   a.sku,
		   b.IdMarca, 
		   b.Descripcion
	from Articulo a, Marca b
END
GO
