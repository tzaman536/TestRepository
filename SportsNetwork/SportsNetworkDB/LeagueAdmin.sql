IF OBJECT_ID('dbo.LeagueAdmin', 'U') IS NOT NULL 
  DROP TABLE dbo.LeagueAdmin; 
CREATE TABLE [dbo].[LeagueAdmin]
(
	[LeagueAdminId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [LeagueId] INT NOT NULL
)
