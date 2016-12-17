IF OBJECT_ID('dbo.LeagueLevels', 'U') IS NOT NULL 
  DROP TABLE dbo.LeagueLevels; 

CREATE TABLE [dbo].[LeagueLevels]
(
	[LeagueLevelId] INT NOT NULL IDENTITY , 
    [LeagueLevel] NCHAR(10) NOT NULL, 
    PRIMARY KEY ([LeagueLevel])
)


