IF OBJECT_ID('SportsNetwork.dbo.LeagueAdmin', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.LeagueAdmin; 
GO

CREATE TABLE SportsNetwork.[dbo].[LeagueAdmin]
(
	[LeagueAdminId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [LeagueId] INT NOT NULL
)
