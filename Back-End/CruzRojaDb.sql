USE [CruzRojaDB]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 13/6/2020 21:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[IdPermission] [int] IDENTITY(1,1) NOT NULL,
	[PermissionType] [nvarchar](100) NULL,
	[PermissionValue] [nvarchar](50) NULL,
	[IdRole] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[IdPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13/6/2020 21:07:54 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 13/6/2020 21:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserFirstName] [nvarchar](100) NULL,
	[UserLastName] [nvarchar](100) NULL,
	[UserDni] [nvarchar](8) NULL,
	[UserPassword] [nvarchar](16) NULL,
	[UserPhone] [nvarchar](12) NULL,
	[UserEmail] [nvarchar](50) NULL,
	[UserGender] [nvarchar](1) NULL,
	[UserAddress] [nvarchar](max) NULL,
	[UserCreatedate] [datetimeoffset](7) NULL,
	[UserBirthdate] [datetimeoffset](7) NULL,
	[UserAvatar] [nvarchar](max) NULL,
	[IdRole] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdRole]) VALUES (1, N'ListUsers', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdRole]) VALUES (2, N'ListUserId', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdRole]) VALUES (3, N'AddNewUser', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdRole]) VALUES (4, N'UpdateUser', N'true', 1)
INSERT [dbo].[Permissions] ([IdPermission], [PermissionType], [PermissionValue], [IdRole]) VALUES (5, N'DeleteUser', N'true', 1)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (2, N'C.General')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserFirstName], [UserLastName], [UserDni], [UserPassword], [UserPhone], [UserEmail], [UserGender], [UserAddress], [UserCreatedate], [UserBirthdate], [UserAvatar], [IdRole]) VALUES (7, N'Yoel', N'Carreras', N'40519791', N'AñadirUsuarios', N'4856567', N'Y.Carreras@cruzroja.org.ar', N'M', N'Activo', CAST(N'2018-12-20T00:00:00.0000000-03:00' AS DateTimeOffset), CAST(N'1993-11-04T00:00:00.0000000-03:00' AS DateTimeOffset), N'hola', 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

