IF OBJECT_ID('SportsNetwork.dbo.LeaguePlayers', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.LeaguePlayers

CREATE TABLE SportsNetwork.[dbo].[LeaguePlayers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LeagueId] INT NOT NULL, 
    [PlayerId] INT NOT NULL
)
