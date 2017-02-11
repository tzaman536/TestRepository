IF OBJECT_ID('SportsNetwork.dbo.LeagueTypes', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.LeagueTypes;
go

CREATE TABLE [SportsNetwork].[dbo].[LeagueTypes]
(
	[LeagueTypeId] INT NOT NULL IDENTITY , 
    [LeagueType] NVARCHAR(100) NOT NULL, 
    PRIMARY KEY ([LeagueType])
)
