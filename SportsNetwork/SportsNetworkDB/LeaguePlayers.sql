IF OBJECT_ID('dbo.LeaguePlayers', 'U') IS NOT NULL 
  DROP TABLE dbo.LeaguePlayers

CREATE TABLE [dbo].[LeaguePlayers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LeagueId] INT NOT NULL, 
    [PlayerId] INT NOT NULL
)
