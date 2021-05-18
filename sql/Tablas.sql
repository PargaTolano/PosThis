-- Equipo: 1

-- Integrantes:
-- José Antonio Parga Tolano - 1808868
-- Esteban Barbosa Martínez - 1735087
-- Yareli Guevara Villalpando - 1805427
-- Valdemar Botello Jasso - 1542845

-- Query para creacion de base de datos [PosThis]
-- Insert de registros en tabla X
-- Query Creado el 2021-02-27 por José Antonio Parga Tolano


--Query Modificado 27/03/2021
--Se ha agragado un Drop al script para mejorar el funcionamiento

--Query modificado 21/04/21
--Se ha modificado los nombres de tablas a idioma inglés

--Query modificado 23/04/21
--Se ha eliminado un atributo ("Content") de la tabla Repost para 
--para su mejor funcionamiento

--Query modificado 12/05/21
-- Se ha agregado ProfilePicID y CoverPicID	 al la tabla AspNetUsers y
-- se ha eliminado las tablas MediaPosts y MediaReplies

USE [master];

DROP DATABASE IF EXISTS PosThis;

CREATE DATABASE PosThis;
GO

USE PosThis;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 27/03/2021 11:05:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 27/03/2021 11:05:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 27/03/2021 11:05:23 a. m. ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 27/03/2021 11:05:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 27/03/2021 11:05:23 a. m. ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	Tag					VARCHAR(15) UNIQUE			,
	BirthDate			DATETIME					,
	ProfilePicID		INT							,
	CoverPicID			INT							,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 27/03/2021 11:05:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO

CREATE TABLE Posts(
	PostID				INTEGER PRIMARY KEY IDENTITY,
	Content				VARCHAR(256)				,
	PostDate			DATETIME	DEFAULT(CURRENT_TIMESTAMP),
	UserID				[nvarchar](450)
);

CREATE TABLE Reposts(
	RepostID			INTEGER PRIMARY KEY IDENTITY,
	RepostDate			DATETIME					,
	PostID				INTEGER NOT NULL,
	UserID			[nvarchar](450) NOT NULL,
);

CREATE TABLE Likes(
	LikeID				INTEGER PRIMARY KEY IDENTITY,
	PostID				INTEGER			NOT NULL,
	UserID				[nvarchar](450) NOT NULL,
);

CREATE TABLE Replies(
	ReplyID				INTEGER PRIMARY KEY IDENTITY,
	ContentReplies		[nvarchar](max) NULL,
	ReplyDate			DATETIME	DEFAULT(CURRENT_TIMESTAMP),
	PostID				INTEGER			NOT NULL,
	UserID				[nvarchar](450) NOT NULL,
);

CREATE TABLE Hashtags(
	HashtagID			INTEGER PRIMARY KEY IDENTITY,
	ContentHashtag		[nvarchar](max)
);

CREATE TABLE Follows(
	FollowID			INTEGER PRIMARY KEY IDENTITY,
	UserFollowerID	[nvarchar](450),
	UserFollowID	[nvarchar](450),
);

CREATE TABLE UserMedias(
	MediaID				INTEGER PRIMARY KEY IDENTITY,
	MIME				VARCHAR(20)					,
	Name				VARCHAR(MAX)				,
);

CREATE TABLE PostMedias(
	MediaID				INTEGER PRIMARY KEY IDENTITY,
	MIME				VARCHAR(20)					,
	Name				VARCHAR(MAX)				,
	PostID				INT							,
);

CREATE TABLE ReplyMedias(
	MediaID				INTEGER PRIMARY KEY IDENTITY,
	MIME				VARCHAR(20)					,
	Name				VARCHAR(MAX)				,
	ReplyID				INT			
);

-- TABLAS RELACIONALES

CREATE TABLE HashtagPosts(

	HashtagPostID		INTEGER PRIMARY KEY IDENTITY,
	HashtagID			INTEGER,
	PostID				INTEGER,
);

/*
CREATE TABLE MediaPosts(
	MediaPostID			INTEGER PRIMARY KEY IDENTITY,
	MediaID				INTEGER,
	PostID				INTEGER,

	CONSTRAINT fk_MediaPost_Media
	FOREIGN KEY(MediaID)
	REFERENCES Medias( MediaID ),

	CONSTRAINT fk_MediaPost_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID )
);

CREATE TABLE MediaReplies(
	MediaReplyID		INTEGER PRIMARY KEY IDENTITY,
	MediaID				INTEGER,
	ReplyID				INTEGER,

	CONSTRAINT fk_MediaReply_Media
	FOREIGN KEY( MediaID )
	REFERENCES Medias( MediaID ),

	CONSTRAINT fk_MediaReply_Reply
	FOREIGN KEY( ReplyID )
	REFERENCES Replies( ReplyID )
);*/