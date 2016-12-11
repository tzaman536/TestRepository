CREATE TABLE [dbo].[LeagueAdmin]
(
	[LeagueAdminId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [LeagueId] INT NOT NULL
)
