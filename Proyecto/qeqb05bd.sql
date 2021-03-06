USE [master]
GO
/****** Object:  Database [QEQB05]    Script Date: 10/10/2018 10:42:23 ******/
CREATE DATABASE [QEQB05]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QEQB05', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\QEQB05.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QEQB05_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\QEQB05_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [QEQB05] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QEQB05].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QEQB05] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QEQB05] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QEQB05] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QEQB05] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QEQB05] SET ARITHABORT OFF 
GO
ALTER DATABASE [QEQB05] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QEQB05] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QEQB05] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QEQB05] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QEQB05] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QEQB05] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QEQB05] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QEQB05] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QEQB05] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QEQB05] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QEQB05] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QEQB05] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QEQB05] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QEQB05] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QEQB05] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QEQB05] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QEQB05] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QEQB05] SET RECOVERY FULL 
GO
ALTER DATABASE [QEQB05] SET  MULTI_USER 
GO
ALTER DATABASE [QEQB05] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QEQB05] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QEQB05] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QEQB05] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QEQB05] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QEQB05', N'ON'
GO
ALTER DATABASE [QEQB05] SET QUERY_STORE = OFF
GO
USE [QEQB05]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [QEQB05]
GO
/****** Object:  User [QEQB05]    Script Date: 10/10/2018 10:42:23 ******/
CREATE USER [QEQB05] FOR LOGIN [QEQB05] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [QEQB05]
GO
/****** Object:  Table [dbo].[CategoríaDePersonajes]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoríaDePersonajes](
	[IdCategoriaP] [int] NOT NULL,
	[Categoría] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CategoríaDePersonajes] PRIMARY KEY CLUSTERED 
(
	[IdCategoriaP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriasDePreguntas]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasDePreguntas](
	[IDCategoría] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [nchar](10) NULL,
 CONSTRAINT [PK_CategoriasDePreguntas] PRIMARY KEY CLUSTERED 
(
	[IDCategoría] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partidas1Jugador]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partidas1Jugador](
	[IDPartida1] [int] IDENTITY(1,1) NOT NULL,
	[IDJugador] [int] NOT NULL,
	[Puntaje] [int] NOT NULL,
 CONSTRAINT [PK_Partidas1Jugador] PRIMARY KEY CLUSTERED 
(
	[IDPartida1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partidas2Jugadores]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partidas2Jugadores](
	[IDPartida2] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[IDUsuario1] [int] NOT NULL,
	[IDUsuario2] [int] NOT NULL,
	[TotalApostado] [int] NOT NULL,
	[Ganador] [bit] NOT NULL,
 CONSTRAINT [PK_Partidas] PRIMARY KEY CLUSTERED 
(
	[IDPartida2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personajes]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personajes](
	[IdPersonaje] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Foto] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Personajes] PRIMARY KEY CLUSTERED 
(
	[IdPersonaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonajesXCategoría]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonajesXCategoría](
	[IdPersonajesXCategoría] [int] NOT NULL,
	[IdPersonaje] [int] NOT NULL,
	[IdCategoría] [int] NOT NULL,
 CONSTRAINT [PK_PersonajesXCategoría] PRIMARY KEY CLUSTERED 
(
	[IdPersonajesXCategoría] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonajeXPregunta]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonajeXPregunta](
	[IdPersonajeXPregunta] [int] NOT NULL,
	[IdPersonaje] [int] NOT NULL,
	[IdPregunta] [int] NOT NULL,
 CONSTRAINT [PK_PersonajeXPregunta] PRIMARY KEY CLUSTERED 
(
	[IdPersonajeXPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preguntas]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preguntas](
	[IDPregunta] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [varchar](75) NOT NULL,
	[IDCategoríaFK] [int] NOT NULL,
 CONSTRAINT [PK_Preguntas] PRIMARY KEY CLUSTERED 
(
	[IDPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 10/10/2018 10:42:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[ID_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Contraseña] [varchar](max) NOT NULL,
	[mail] [varchar](50) NOT NULL,
	[Administrador] [bit] NOT NULL,
	[PuntosAcumulados] [bigint] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PersonajesXCategoría]  WITH CHECK ADD  CONSTRAINT [FK_PersonajesXCategoría_CategoríaDePersonajes] FOREIGN KEY([IdCategoría])
REFERENCES [dbo].[CategoríaDePersonajes] ([IdCategoriaP])
GO
ALTER TABLE [dbo].[PersonajesXCategoría] CHECK CONSTRAINT [FK_PersonajesXCategoría_CategoríaDePersonajes]
GO
ALTER TABLE [dbo].[PersonajesXCategoría]  WITH CHECK ADD  CONSTRAINT [FK_PersonajesXCategoría_Personajes] FOREIGN KEY([IdPersonaje])
REFERENCES [dbo].[Personajes] ([IdPersonaje])
GO
ALTER TABLE [dbo].[PersonajesXCategoría] CHECK CONSTRAINT [FK_PersonajesXCategoría_Personajes]
GO
ALTER TABLE [dbo].[PersonajeXPregunta]  WITH CHECK ADD  CONSTRAINT [FK_PersonajeXPregunta_Personajes] FOREIGN KEY([IdPersonaje])
REFERENCES [dbo].[Personajes] ([IdPersonaje])
GO
ALTER TABLE [dbo].[PersonajeXPregunta] CHECK CONSTRAINT [FK_PersonajeXPregunta_Personajes]
GO
ALTER TABLE [dbo].[PersonajeXPregunta]  WITH CHECK ADD  CONSTRAINT [FK_PersonajeXPregunta_Preguntas] FOREIGN KEY([IdPregunta])
REFERENCES [dbo].[Preguntas] ([IDPregunta])
GO
ALTER TABLE [dbo].[PersonajeXPregunta] CHECK CONSTRAINT [FK_PersonajeXPregunta_Preguntas]
GO
ALTER TABLE [dbo].[Preguntas]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_CategoriasDePreguntas] FOREIGN KEY([IDCategoríaFK])
REFERENCES [dbo].[CategoriasDePreguntas] ([IDCategoría])
GO
ALTER TABLE [dbo].[Preguntas] CHECK CONSTRAINT [FK_Preguntas_CategoriasDePreguntas]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCategoríaDePersonaje]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteCategoríaDePersonaje]
@pIdP int, @pIdC int AS
DELETE FROM PersonajesXCategoría 
WHERE PersonajesXCategoría.IdPersonaje = @pIdP AND PersonajesXCategoría.IdCategoría = @pIdC 
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarCategoríaDePersonaje]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarCategoríaDePersonaje]
@pIdP int, @pIdC int AS
INSERT INTO PersonajesXCategoría (IdPersonaje, IdCategoría) VALUES (@pIdP, @pIdC)
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarTodasLasPreguntas]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ListarTodasLasPreguntas]
	
AS
BEGIN
	select * from Preguntas
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarTodosLosPersonajes]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarTodosLosPersonajes]
	
AS
BEGIN
	select * from Personajes
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Login]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_Login] 
	@pMail varchar(50),
	@pContraseña varchar(50)
AS
Begin
IF exists (SELECT Usuarios.Mail FROM Usuarios WHERE  Usuarios.mail = @pMail) 
BEGIN
		IF EXISTS (SELECT * FROM Usuarios WHERE Usuarios.mail = @pMail AND Usuarios.Contraseña = HASHBYTES('SHA2_256', @pContraseña))
		BEGIN
			SELECT * FROM Usuarios WHERE Usuarios.mail = @pMail
		END
END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_OlvideContraseña]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in

CREATE PROCEDURE [dbo].[sp_OlvideContraseña]
	@Mail varchar(50),
	@Contraseña varchar(50)
AS
BEGIN
	if(exists(Select Usuarios.mail from Usuarios Where Usuarios.mail = @Mail))
	begin
	update  Usuarios
	set Contraseña=HASHBYTES('MD5',@Contraseña)
	where Usuarios.mail = @Mail
	End
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PersonajeAlta]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PersonajeAlta]
	@pNombre varchar(50), @pFoto varchar(max)
AS
BEGIN
	insert into Personajes(Nombre,Foto)
	values (@pNombre,@pFoto)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PersonajeBaja]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PersonajeBaja]
	@pId int
AS
BEGIN
	delete from Personajes
	where IdPersonaje=@pId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PersonajeModif]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PersonajeModif]
	@pId int, @pNombre varchar(50), @pFoto varchar(max)
AS
BEGIN
	update Personajes set Nombre=@pNombre,Foto=@pFoto
	where IdPersonaje=@pId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PreguntasAlta]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PreguntasAlta]
	@pTexto varchar(75), @pIdCat int
AS
BEGIN
	insert into Preguntas(Texto,IDCategoríaFK)
	values (@pTexto,@pIdCat)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PreguntasBaja]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PreguntasBaja]
	@pIdPreg int
AS
BEGIN
	delete from Preguntas
	where IDPregunta=@pIdPreg
END
GO
/****** Object:  StoredProcedure [dbo].[sp_PreguntasModif]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PreguntasModif]
	@pIdPreg int, @pTexto varchar(75), @pIdCat int
AS
BEGIN
	update Preguntas set Texto=@pTexto,IDCategoríaFK=@pIdCat
	where IDPregunta=@pIdPreg
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarNuevoUsuario]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegistrarNuevoUsuario]
	@pNombre varchar(50), @pContraseña varchar(max), @pMail varchar(50), @pAdmin bit, @pPuntosAcum bigint
AS
BEGIN
	declare @EncriptPass varchar(max)
	set @EncriptPass=HASHBYTES('SHA2_256',@pContraseña)
	insert into Usuarios(Nombre,Contraseña,mail,Administrador,PuntosAcumulados)
	values (@pNombre,@EncriptPass,@pMail,@pAdmin,@pPuntosAcum)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SumarPuntuacionFinal]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SumarPuntuacionFinal]
	@pIdUsu varchar(50), @pPuntosObtenidos int
AS
BEGIN
	update Usuarios set PuntosAcumulados=PuntosAcumulados+@pPuntosObtenidos
	where ID_Usuario=@pIdUsu
	insert into Partidas1Jugador(IDJugador,Puntaje)
	values (@pIdUsu,@pPuntosObtenidos)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Traer1Personaje]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Traer1Personaje]
	@pIdPers int
AS
BEGIN
	select * from Personajes where IdPersonaje=@pIdPers
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TraerCategoríasDePersonaje]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TraerCategoríasDePersonaje]
@pId int AS
SELECT PersonajesXCategoría.IdCategoría, CategoríaDePersonajes.Categoría FROM PersonajesXCategoría
INNER JOIN CategoríaDePersonajes ON PersonajesXCategoría.IdCategoría = CategoríaDePersonajes.IdCategoriaP
WHERE PersonajesXCategoría.IdPersonaje = @pId
GO
/****** Object:  StoredProcedure [dbo].[sp_TraerTodasCategoríasP]    Script Date: 10/10/2018 10:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TraerTodasCategoríasP] AS
SELECT CategoríaDePersonajes.IdCategoriaP, CategoríaDePersonajes.Categoría FROM CategoríaDePersonajes
GO
USE [master]
GO
ALTER DATABASE [QEQB05] SET  READ_WRITE 
GO
