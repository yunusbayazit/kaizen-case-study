USE [BlogDatabase]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [Title], [Description], [Content], [Author], [CreatedDate]) VALUES (3, N'string', N'string', N'string', NULL, CAST(N'2021-02-14T11:29:44.4100000' AS DateTime2))
INSERT [dbo].[Posts] ([Id], [Title], [Description], [Content], [Author], [CreatedDate]) VALUES (4, N'string', N'string', N'string', NULL, CAST(N'2021-02-14T11:29:48.9133333' AS DateTime2))
INSERT [dbo].[Posts] ([Id], [Title], [Description], [Content], [Author], [CreatedDate]) VALUES (5, N'string123123', N's123123tring', N'str123213123ing', NULL, CAST(N'2021-02-14T11:29:56.5133333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt]) VALUES (1, N'YUNUS EMRE', N'BAYAZIT', N'yunusbayazit', 0x44845EEB9CBFAC2A7A873BF1803C781CB573D23242CC88DA0FD9F449B29F5DA01BE4977264775637424D6774D7A0B42021A5D4521AB7555C60E99773BC6EC2E0, 0x2AA0AD5FA9FD9A4C45B2803407597028157B435AED4E3EF6770155C61E30948DDC67CDB365CC1BCED340EE257F78D109711C27DFF6048B1200F2D701BCE939C9FB2B1EAF1834A6903E3B9361A01CAEF52383EE0409287EE01BD0998D599EB4F523C9EE6B39389F11810E12BAD95CFD6C51E798EF615D5A0C0137844D2B89A2E2)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt]) VALUES (2, N'yunus emre', N'bayazıtttt', N'yunusbayazit1', 0xC582E63E8BFAAE1458B19DEB445CABCC0CC1002FA5B0E8E4B14FBD0DD315249568DFE9AF27DB1294FDCADB6CC13ED8EF1540AD646F42074575C423E3CDB95A3C, 0x151DC0C544C8B0B4E5A3E1596BC1C6A571104123F1E2BDB64D0DBB6D1712F40CEC47177B9989E7DB3FBD5D6D9E878966D2D536D9CADE11ECB5809DD43CDD389DD9438B0E501CF26FCDC143FB6E94DC435D1C69759EDF55735E51441FE23BCB6301492DF92D05559064DC75D2D19AA3D3D308D63970B2A3D0982C8112073EE26C)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt]) VALUES (3, N'admin', N'admin', N'admin', 0x848BF5A3A1D6C20B33F9EAF31566C2A63ACCD39BCBC354F109BF9547BF0BB2077CD16F41255428AAD2CACCB45807A50AFAD3B8A6DA3EDC46171B2CA3B827FB9B, 0x739F3266B8879FE1BF256D7257248DE7567248061EE9D58084ED8D8B6759CB96BD9FE71C392EB77FAB0CA7C15C08DD6A3CD691F7C084385597F43ED5901998879D06DEEF07A98DAC6E958B2525412059ECF96BF2A2EB16FD8194E37E80427AE2C92FCC1C617ECEECA911E6A6A1AD710126FB4D458572B15534AA91E0F88B99F9)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_PostsDelete]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_PostsDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Posts]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_PostsInsert]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_PostsInsert] 
    @Title nvarchar(MAX) = NULL,
    @Description nvarchar(MAX) = NULL,
    @Content nvarchar(MAX) = NULL,
    @Author nvarchar(MAX) = NULL,
    @CreatedDate datetime2(7)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Posts] ([Title], [Description], [Content], [Author], [CreatedDate])
	SELECT @Title, @Description, @Content, @Author, @CreatedDate
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Title], [Description], [Content], [Author], [CreatedDate]
	FROM   [dbo].[Posts]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_PostsSelectFindById]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[sp_PostsSelectFindById] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [Title], [Description], [Content], [Author], [CreatedDate] 
	FROM   [dbo].[Posts] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_PostsSelectGetAll]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_PostsSelectGetAll] 
	@sortorder varchar(10),
	@sortcolumn varchar(100),
	@filter varchar(max)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [Title], [Description], [Content], [Author], [CreatedDate] 
	FROM   [dbo].[Posts] where Title like '%' + @filter +'%' and Description like '%' + @filter +'%' and [Content] like '%' + @filter +'%'
	ORDER by CASE WHEN @sortorder='ASC' THEN @sortcolumn END,
         CASE WHEN @sortorder='DESC' THEN @sortcolumn END DESC

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_PostsUpdate]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_PostsUpdate] 
    @Id int,
    @Title nvarchar(MAX) = NULL,
    @Description nvarchar(MAX) = NULL,
    @Content nvarchar(MAX) = NULL,
    @Author nvarchar(MAX) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Posts]
	SET    [Title] = @Title, [Description] = @Description, [Content] = @Content, [Author] = @Author
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Title], [Description], [Content], [Author], [CreatedDate]
	FROM   [dbo].[Posts]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersDelete]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_UsersDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Users]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersInsert]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_UsersInsert] 
    @FirstName nvarchar(MAX) = NULL,
    @LastName nvarchar(MAX) = NULL,
    @Username nvarchar(MAX) = NULL,
    @PasswordHash varbinary(MAX) = NULL,
    @PasswordSalt varbinary(MAX) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Users] ([FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt])
	SELECT @FirstName, @LastName, @Username, @PasswordHash, @PasswordSalt
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt]
	FROM   [dbo].[Users]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersSelect]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_UsersSelect] 
    @Username nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt] 
	FROM   [dbo].[Users] 
	WHERE  ([Username] = @Username OR @Username IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersSelectFindById]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[sp_UsersSelectFindById] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt] 
	FROM   [dbo].[Users] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersSelectGetAll]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[sp_UsersSelectGetAll] 
    @Username nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt] 
	FROM   [dbo].[Users]

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersUpdate]    Script Date: 2/14/2021 5:17:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_UsersUpdate] 
    @Id int,
    @FirstName nvarchar(MAX) = NULL,
    @LastName nvarchar(MAX) = NULL,
    @Username nvarchar(MAX) = NULL,
    @PasswordHash varbinary(MAX) = NULL,
    @PasswordSalt varbinary(MAX) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Users]
	SET    [FirstName] = @FirstName, [LastName] = @LastName, [Username] = @Username, [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [FirstName], [LastName], [Username], [PasswordHash], [PasswordSalt]
	FROM   [dbo].[Users]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT
GO
