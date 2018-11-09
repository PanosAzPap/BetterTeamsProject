USE [PojectBetterTeamsDb]
GO

/****** Object:  Table [dbo].[Posts]    Script Date: 9/11/2018 7:31:55 πμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[UsernameSender] [varchar](20) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Post] [varchar](250) NOT NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Posts_dbo.Users_UsernameSender] FOREIGN KEY([UsernameSender])
REFERENCES [dbo].[Users] ([Username])
GO

ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_dbo.Posts_dbo.Users_UsernameSender]
GO

