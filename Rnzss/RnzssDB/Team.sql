IF OBJECT_ID('SportsNetwork.dbo.Teams', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.Teams;

CREATE TABLE [SportsNetwork].[dbo].[Teams]
(
	[TeamId] INT NOT  NULL  IDENTITY, 
    [TeamName] NVARCHAR(255) NOT NULL, 
    [TeamDetail] NVARCHAR(255)  NULL, 
    [TeamType] int not null, 
	[AddUserName] nvarchar(255) NOT NULL,
	[AddUpdateDt] datetime not null
)
GO

ALTER TABLE [SportsNetwork].[dbo].[Teams]
ADD CONSTRAINT PK_Teams PRIMARY KEY (TeamName);
GO
