USE [Friss]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 24.03.2019 15:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FilePath] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[LastDownloadDate] [datetime2](7) NOT NULL,
	[FileSize] [varchar](32) NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 24.03.2019 15:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[Id] [int] NOT NULL,
	[TypeName] [nvarchar](64) NOT NULL,
	[Extentions] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 24.03.2019 15:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[IsUploadDocument] [bit] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24.03.2019 15:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[refUserRole] [int] NOT NULL,
	[FirstName] [nvarchar](64) NOT NULL,
	[LastName] [nvarchar](64) NOT NULL,
	[UserName] [nvarchar](64) NOT NULL,
	[Email] [varchar](64) NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Documents] ON 

INSERT [dbo].[Documents] ([Id], [Name], [FilePath], [CreatedDate], [CreatedUserId], [LastDownloadDate], [FileSize]) VALUES (1, N'My First File', N'http://localhost:52490/LocalStorage/d57ca1054b8d45f5916880cab6db59fe.docx', CAST(N'2019-03-24T11:59:46.6773711' AS DateTime2), 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'12692')
INSERT [dbo].[Documents] ([Id], [Name], [FilePath], [CreatedDate], [CreatedUserId], [LastDownloadDate], [FileSize]) VALUES (2, N'0380c9095ea24986a230bbcb67305fac', N'http://localhost:52490/LocalStorage/cb860de51bf740ea8ce95c02803753b9.docx', CAST(N'2019-03-24T12:01:03.1134488' AS DateTime2), 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'12692')
SET IDENTITY_INSERT [dbo].[Documents] OFF
INSERT [dbo].[DocumentType] ([Id], [TypeName], [Extentions]) VALUES (1, N'PDF', N'.pdf')
INSERT [dbo].[DocumentType] ([Id], [TypeName], [Extentions]) VALUES (2, N'WORD', N'.doc;.docx')
INSERT [dbo].[DocumentType] ([Id], [TypeName], [Extentions]) VALUES (3, N'EXCEL', N'.xls;.xlsx')
INSERT [dbo].[UserRole] ([Id], [Name], [IsUploadDocument]) VALUES (1, N'Admin', 1)
INSERT [dbo].[UserRole] ([Id], [Name], [IsUploadDocument]) VALUES (2, N'Operator', 0)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [refUserRole], [FirstName], [LastName], [UserName], [Email], [Password], [CreatedDate], [CreatedUserId], [IsActive], [IsDeleted], [Token]) VALUES (1, 1, N'Burak', N'Kepti', N'bkepti', N'bkepti@gmail.com', N'5c9a4a1337ab9da0b4e7380b873ffc01', CAST(N'2019-03-22T13:06:04.1866667' AS DateTime2), 1, 1, 0, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJia2VwdGkiLCJqdGkiOiJmOWE3ZWIzMC1mZTA2LTQ2M2UtOTg0OC0wMjNhYmIxNjFhZGQiLCJleHAiOjE1NTM1NDg4NTgsImlzcyI6ImZyaXNzLmNvbSIsImF1ZCI6ImZyaXNzLmNvbSJ9.5bRM1IcNEVUEUquGZGuvjBbrhliavNFxpaifruJLYpA')
INSERT [dbo].[Users] ([Id], [refUserRole], [FirstName], [LastName], [UserName], [Email], [Password], [CreatedDate], [CreatedUserId], [IsActive], [IsDeleted], [Token]) VALUES (2, 2, N'Guest', N'Guest', N'guest', N'guest@gmail.com', N'b7dfe9134c7a717a3b6eaf37bdc1ef7b', CAST(N'2019-03-22T13:07:05.1633333' AS DateTime2), 1, 1, 0, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJndWVzdCIsImp0aSI6ImJiYjMwODZlLWUwNTItNDBkNC1iZTA0LTM1NDgyOTA1MzY3NyIsImV4cCI6MTU1MzU0ODg2NSwiaXNzIjoiZnJpc3MuY29tIiwiYXVkIjoiZnJpc3MuY29tIn0.iYsm2ATmWyw9aT41HH6zHok3N4inNMt3bzaimOCcc1g')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 24.03.2019 15:13:32 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 24.03.2019 15:13:32 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [DF_UserRole_IsUploadDocument]  DEFAULT ((0)) FOR [IsUploadDocument]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedUserId]  DEFAULT ((1)) FOR [CreatedUserId]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Documents] FOREIGN KEY([CreatedUserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Documents]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRole] FOREIGN KEY([refUserRole])
REFERENCES [dbo].[UserRole] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRole]
GO
