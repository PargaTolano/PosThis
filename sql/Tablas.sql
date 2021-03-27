-- Equipo: 1

-- Integrantes:
-- José Antonio Parga Tolano - 1808868
-- Esteban Barbosa Martínez - 1735087
-- Yareli Guevara Villalpando - 1805427
-- Valdemar Botello Jasso - 1542845

-- Query para creacion de base de datos [PosThis]
-- Insert de registros en tabla X
-- Query Creado el 2021-02-27 por José Antonio Parga Tolano

-- TABLAS PRIMAS

--Query Modificado 27/03/2021
--Se ha agragado un Drop al script para mejorar el funcionamiento

USE [master];

DROP DATABASE IF EXISTS PosThis;

CREATE DATABASE PosThis;
GO

USE PosThis;

CREATE TABLE Usuarios(
	UsuarioID			INTEGER PRIMARY KEY IDENTITY,
	Nombre				VARCHAR(20)					,
	Tag					VARCHAR(15) UNIQUE			,
	Correo				VARCHAR(35) UNIQUE			,
	Contrasena			VARCHAR(20)					,
	FechaNacimiento		DATETIME					,
);

CREATE TABLE Posts(
	PostID				INTEGER PRIMARY KEY IDENTITY,
	Texto				VARCHAR(256)				,
	FechaPublicacion	DATETIME					,
	UsuarioID			INTEGER						,

	CONSTRAINT fk_Post_Usuario 
	FOREIGN KEY(UsuarioID)
	REFERENCES Usuarios( UsuarioID )
);

CREATE TABLE Reposts(
	RepostID			INTEGER PRIMARY KEY IDENTITY,
	Texto				VARCHAR(256)				,
	FechaPublicacion	DATETIME					,
	PostID				INTEGER NOT NULL,
	UsuarioID			INTEGER NOT NULL,

	CONSTRAINT fk_Repost_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID ),

	CONSTRAINT fk_Repost_Usuario
	FOREIGN KEY(UsuarioID)
	REFERENCES Usuarios( UsuarioID )
);

CREATE TABLE Likes(
	LikeID				INTEGER PRIMARY KEY IDENTITY,
	PostID				INTEGER NOT NULL,
	UsuarioID			INTEGER NOT NULL,

	CONSTRAINT fk_Likes_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID ),

	CONSTRAINT fk_Likes_Usuario
	FOREIGN KEY(UsuarioID)
	REFERENCES Usuarios( UsuarioID )
);

CREATE TABLE Replies(
	ReplyID			INTEGER PRIMARY KEY IDENTITY,
	PostID				INTEGER NOT NULL,
	UsuarioID			INTEGER NOT NULL,

	CONSTRAINT fk_Replies_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID ),

	CONSTRAINT fk_Replies_Usuario
	FOREIGN KEY(UsuarioID)
	REFERENCES Usuarios( UsuarioID )
);

CREATE TABLE Hashtags(
	HastagID			INTEGER PRIMARY KEY IDENTITY,
	Texto				TEXT
);

CREATE TABLE Follows(
	FollowID			INTEGER PRIMARY KEY IDENTITY,
	UsuarioSeguidorID	INTEGER,
	UsuarioSeguidoID	INTEGER,

	CONSTRAINT fk_Follows_Usuarios_Seguidor
	FOREIGN KEY(UsuarioSeguidorID)
	REFERENCES Usuarios( UsuarioID ),

	CONSTRAINT fk_Follows_Usuarios_Seguido
	FOREIGN KEY(UsuarioSeguidoID)
	REFERENCES Usuarios( UsuarioID ),
);

CREATE TABLE Media(
	MediaID				INTEGER PRIMARY KEY IDENTITY,
	MIME				VARCHAR(20)					,
	Contenido			VARBINARY(MAX)
);

ALTER TABLE Usuarios 
ADD 
	FotoPerfil INTEGER NULL
	CONSTRAINT fk_Usuario_Media FOREIGN KEY( FotoPerfil) REFERENCES Media(MediaID);

-- TABLAS RELACIONALES

CREATE TABLE HastagPost(
	HastagID			INTEGER,
	PostID				INTEGER,

	CONSTRAINT fk_HashtagPost_Hashtag
	FOREIGN KEY(HastagID)
	REFERENCES Hashtags( HastagID ),

	CONSTRAINT fk_HashtagPost_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID ),
);

CREATE TABLE MediaPost(
	MediaID				INTEGER,
	PostID				INTEGER,

	CONSTRAINT fk_MediaPost_Media
	FOREIGN KEY(MediaID)
	REFERENCES Media( MediaID ),

	CONSTRAINT fk_MediaPost_Post
	FOREIGN KEY(PostID)
	REFERENCES Posts( PostID )
);