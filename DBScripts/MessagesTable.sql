USE [PojectBetterTeamsDb]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 9/11/2018 7:31:37 πμ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Messages](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[UsernameSender] [varchar](20) NOT NULL,
	[Receiver] [varchar](20) NOT NULL,
	[Message] [varchar](250) NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.Users_UsernameSender] FOREIGN KEY([UsernameSender])
REFERENCES [dbo].[Users] ([Username])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.Users_UsernameSender]
GO

