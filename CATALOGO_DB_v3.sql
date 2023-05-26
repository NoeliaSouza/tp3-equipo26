use master
go
create database CATALOGO_P3_DB
go
use CATALOGO_P3_DB
go
USE CATALOGO_P3_DB
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MARCAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_MARCAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CATEGORIAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ARTICULOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](150) NULL,
	[IdMarca] [int] NULL,
	[IdCategoria] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_ARTICULOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

create table IMAGENES(
	Id int IDENTITY(1,1) not null,
	IdArticulo int not null,
	ImagenUrl varchar(1000) not null
)
go

insert into MARCAS values ('Samsung'), ('Apple'), ('Sony'), ('Huawei'), ('Motorola')
insert into CATEGORIAS values ('Celulares'),('Televisores'), ('Media'), ('Audio')
insert into ARTICULOS values ('S01', 'Galaxy S10', 'Una canoa cara', 1, 1, 69.999),
('M03', 'Moto G Play 7ma Gen', 'Ya siete de estos?', 1, 5, 15699),
('S99', 'Play 4', 'Ya no se cuantas versiones hay', 3, 3, 35000),
('S56', 'Bravia 55', 'Alta tele', 3, 2, 49500),
('A23', 'Apple TV', 'lindo loro', 2, 3, 7850)

insert into imagenes values
(1,'https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542'),
(2, 'https://www.motorola.cl/arquivos/moto-g7-play-img-product.png?v=636862863804700000'),
(2, 'https://i.blogs.es/9da288/moto-g7-/1366_2000.jpg'),
(3, 'https://www.euronics.cz/image/product/800x800/532620.jpg'),
(4, 'https://intercompras.com/product_thumb_keepratio_2.php?img=images/product/SONY_KDL-55W950A.jpg&w=650&h=450'),
(5, 'https://cnnespanol2.files.wordpress.com/2015/12/gadgets-mc3a1s-populares-apple-tv-2015-18.jpg?quality=100&strip=info&w=460&h=260&crop=1')

select * from ARTICULOS


Select Codigo, Nombre, a.Descripcion as DescripcionArticulo, Precio , m.Descripcion as NombreMarca, c.Descripcion as NombreCategoria, i.ImagenUrl as imagen from ARTICULOS as a
inner join MARCAS as m on a.IdMarca = m.Id
inner join CATEGORIAS as c on a.IdCategoria = c.Id
inner join IMAGENES as i on i.IdArticulo = a.Id


select Codigo,Nombre, Descripcion,Precio from ARTICULOS


alter procedure storedListar as
SELECT a.Id, Codigo, Nombre, a.Descripcion 
as DescripcionArticulo, 
Precio,m.Id as IdMarca, 
m.Descripcion as NombreMarca,
c.Id as IdCategoria, 
c.Descripcion as NombreCategoria, 
i.ImagenUrl as imagen,i.Id as IdImagen ,i.IdArticulo as idArticuloImagen from ARTICULOS a 
left join MARCAS m on a.IdMarca=m.Id 
left join CATEGORIAS c on a.IdCategoria=c.Id 
left join IMAGENES i on a.Id=i.IdArticulo




storedListar



select * from IMAGENES