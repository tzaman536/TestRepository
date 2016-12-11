CREATE TABLE [dbo].[LeagueTypes]
(
	[LeagueTypeId] INT NOT NULL IDENTITY , 
    [Type] NVARCHAR(100) NOT NULL, 
    PRIMARY KEY ([Type])
)
