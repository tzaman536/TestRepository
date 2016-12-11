CREATE TABLE [dbo].[Leagues]
(
	[LeagueId] INT NOT NULL IDENTITY , 
    [LeagueName] NCHAR(100) NOT NULL, 
    [LeagueDescription] NCHAR(255) NOT NULL, 
    [UserName] NVARCHAR(150) NULL, 
    [AddUpdateDt] DATETIME NULL, 
    PRIMARY KEY ([LeagueName])
)
