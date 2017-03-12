IF OBJECT_ID('SportsNetwork.dbo.Players', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.Players;

CREATE TABLE [SportsNetwork].[dbo].[Players]
(
	[PlayerId] INT NOT  NULL  IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [Phone] NVARCHAR(50) NULL,
	[AddUserName] nvarchar(100) NOT NULL,
	[AddUpdateDt] datetime not null
)
GO

ALTER TABLE [SportsNetwork].[dbo].[Players]
ADD CONSTRAINT PK_Players PRIMARY KEY (Email,AddUserName);
GO
