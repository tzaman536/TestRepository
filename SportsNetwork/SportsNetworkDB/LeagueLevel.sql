IF OBJECT_ID('SportsNetwork.dbo.LeagueLevels', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.LeagueLevels; 

CREATE TABLE SportsNetwork.[dbo].[LeagueLevels]
(
	[LeagueLevelId] INT NOT NULL IDENTITY , 
    [LeagueLevel] NCHAR(10) NOT NULL, 
    PRIMARY KEY ([LeagueLevel])
)


