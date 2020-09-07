USE [CruzRojaDB]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 13/6/2020 19:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionType] [nvarchar](100) NULL,
	[PermissionValue] [nvarchar](50) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13/6/2020 19:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13/6/2020 19:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserFirstname] [nvarchar](100) NOT NULL,
	[UserLastname] [nvarchar](100) NOT NULL,
	[UserDni] [nvarchar](8) NOT NULL,
	[UserPassword] [nvarchar](16) NOT NULL,
	[UserPhone] [nvarchar](12) NOT NULL,
	[UserEmail] [nvarchar](50) NOT NULL,
	[UserGender] [nvarchar](1) NOT NULL,
	[UserAddress] [nvarchar](max)NOT NULL,
	[UserCreatedate] [datetimeoffset](7) NULL,
	[UserBirthdate] [datetimeoffset](7) NULL,
	[UserAvatar] [nvarchar](max)  NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([PermissionID], [PermissionType], [PermissionValue], [RoleID]) VALUES (3, N'ListUsers', N'true', 1)
INSERT [dbo].[Permissions] ([PermissionID], [PermissionType], [PermissionValue], [RoleID]) VALUES (4, N'AddNewUser', N'true', 1)
INSERT [dbo].[Permissions] ([PermissionID], [PermissionType], [PermissionValue], [RoleID]) VALUES (5, N'ListUserId', N'true', 1)
INSERT [dbo].[Permissions] ([PermissionID], [PermissionType], [PermissionValue], [RoleID]) VALUES (6, N'UpdateUser', N'true', 1)
INSERT [dbo].[Permissions] ([PermissionID], [PermissionType], [PermissionValue], [RoleID]) VALUES (7, N'DeleteUser', N'true', 1)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Coordinadora General')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserFirstname], [UserLastname], [UserDni], [UserPassword], [UserPhone], [UserEmail], [UserGender], [UserAddress], [UserCreatedate], [UserBirthdate], [UserAvatar], [RoleID]) VALUES (2, N'Matias', N'Roldan', N'38413770', N'admin123', N'3875954878', N'matias_roldan89@hotmail.com', N'M', N'Corrientes 334', CAST(N'2020-06-16T00:00:00.0000000-03:00' AS DateTimeOffset), CAST(N'1994-10-04T00:00:00.0000000-03:00' AS DateTimeOffset), N'cacota', 1)
INSERT [dbo].[Users] ([UserID], [UserFirstname], [UserLastname], [UserDni], [UserPassword], [UserPhone], [UserEmail], [UserGender], [UserAddress], [UserCreatedate], [UserBirthdate], [UserAvatar], [RoleID]) VALUES (9, N'Matias', N'Roldan', N'38413771', N'admin123', N'3875954878', N'matias-roldan98@hotmail.com', N'O', N'Corrientes 334', CAST(N'2020-06-16T00:00:00.0000000-03:00' AS DateTimeOffset), CAST(N'1999-06-16T00:00:00.0000000-03:00' AS DateTimeOffset), N'cacaca', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
