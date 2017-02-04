IF OBJECT_ID('dbo.Leagues', 'U') IS NOT NULL 
  DROP TABLE dbo.Leagues;

CREATE TABLE [dbo].[Leagues]
(
	[LeagueId] INT NOT NULL IDENTITY , 
    [LeagueName] NCHAR(100) NOT NULL, 
    [LeagueDescription] NCHAR(255) NOT NULL, 
	[LeagueTypeId] INT NOT NULL, 
    [LeagueLevelId] INT NOT NULL, 
    [AddUserName] NCHAR(100) NOT NULL, 
    [AddUpdateDt] datetime not null

    PRIMARY KEY ([LeagueName])
)
