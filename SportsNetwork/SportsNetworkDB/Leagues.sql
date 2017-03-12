IF OBJECT_ID('SportsNetwork.dbo.Leagues', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.Leagues;

CREATE TABLE [SportsNetwork].[dbo].[Leagues]
(
	[LeagueId] INT NOT NULL IDENTITY , 
    [LeagueName] NCHAR(100) NOT NULL, 
    [LeagueDescription] NCHAR(255) NOT NULL, 
	[LeagueTypeId] INT NOT NULL, 
    [LeagueLevelId] INT NOT NULL, 
    [AddUserName] NCHAR(100) NOT NULL, 
    [AddUpdateDt] datetime not null
)
go

ALTER TABLE [SportsNetwork].[dbo].[Leagues]
ADD CONSTRAINT PK_Leagues PRIMARY KEY (LeagueName,AddUserName);
