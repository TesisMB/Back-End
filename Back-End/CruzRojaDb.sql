USE [CruzRojaDB]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 13/6/2020 12:57:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[IdPermission] [int] IDENTITY(1,1) NOT NULL,
	[PermissionType] [nvarchar](50) NULL,
	[PermissionValue] [nvarchar](50) NULL,
	[IdUser] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[IdPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13/6/2020 12:57:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13/6/2020 12:57:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Dni] [nvarchar](8) NULL,
	[Password] [nvarchar](16) NULL,
	[Phone] [nvarchar](12) NULL,
	[Email] [nvarchar](75) NULL,
	[Gender] [nvarchar](1) NULL,
	[Address] [nvarchar](50) NULL,
	[DateOfCreate] [datetimeoffset](7) NULL,
	[DateOfBirth] [datetimeoffset](7) NULL,
	[IdRole] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdUser]) VALUES (1, N'ListUsers', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdUser]) VALUES (2, N'ListUserId', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdUser]) VALUES (3, N'AddNewUser', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdUser]) VALUES (4, N'UpdateUser', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdUser]) VALUES (5, N'DeleteUser ', N'true', 1)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (3, N'Admin')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (4, N'C.General')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (5, N'C.EmergenciaDesastres')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (8, N'C.Comunicación')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (9, N'Logística')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([IdUser], [Name], [LastName], [Dni], [Password], [Phone], [Email], [Gender], [Address], [DateOfCreate], [DateOfBirth], [IdRole]) VALUES (1, N'Noelia', N'Funes', N'29245591', N'1234', N'4875521', N'N.Funes@cruzroja.org.ar', N'F', N'hola', CAST(N'2013-03-12T12:41:06.0000000-03:00' AS DateTimeOffset), CAST(N'1987-05-21T00:00:00.0000000-03:00' AS DateTimeOffset), 4)
INSERT [dbo].[Users] ([IdUser], [Name], [LastName], [Dni], [Password], [Phone], [Email], [Gender], [Address], [DateOfCreate], [DateOfBirth], [IdRole]) VALUES (2, N'Carlos ', N'Diaz', N'32081127', N'478', N'4875623', N'C.Diaz@cruzroja.org.ar', N'M', N'Soltero', CAST(N'2015-08-03T10:31:58.0000000-03:00' AS DateTimeOffset), CAST(N'1991-02-01T00:00:00.0000000-03:00' AS DateTimeOffset), 5)
INSERT [dbo].[Users] ([IdUser], [Name], [LastName], [Dni], [Password], [Phone], [Email], [Gender], [Address], [DateOfCreate], [DateOfBirth], [IdRole]) VALUES (3, N'Diego ', N'Rodriguez', N'34251961', N'r04Dz%D&12', N'4879902', N'D.Rodriguez@cruzroja.org.ar', N'M', N'Soltero', CAST(N'2017-04-20T12:30:48.0000000-03:00' AS DateTimeOffset), CAST(N'1995-05-10T00:00:00.0000000-03:00' AS DateTimeOffset), 8)
INSERT [dbo].[Users] ([IdUser], [Name], [LastName], [Dni], [Password], [Phone], [Email], [Gender], [Address], [DateOfCreate], [DateOfBirth], [IdRole]) VALUES (4, N'Fernando', N'Paredes', N'32457896', N'123', N'4851245', N'F.Paredes@cuzroja.com.ar', N'M', N'Soltero', CAST(N'2015-10-10T11:56:13.0000000-03:00' AS DateTimeOffset), CAST(N'1992-04-22T00:00:00.0000000-03:00' AS DateTimeOffset), 9)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Roles]
GO
