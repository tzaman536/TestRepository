IF OBJECT_ID('dbo.LeagueTypes', 'U') IS NOT NULL 
  DROP TABLE dbo.LeagueTypes;
CREATE TABLE [dbo].[LeagueTypes]
(
	[LeagueTypeId] INT NOT NULL IDENTITY , 
    [LeagueType] NVARCHAR(100) NOT NULL, 
    PRIMARY KEY ([LeagueType])
)
