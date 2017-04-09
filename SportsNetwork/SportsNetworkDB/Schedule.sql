IF OBJECT_ID('SportsNetwork.dbo.Schedules', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.Schedules;

CREATE TABLE [SportsNetwork].[dbo].[Schedules]
(
	[ScheduleId] INT NOT  NULL  IDENTITY, 
    [TeamOneId] INT NOT NULL, 
    [TeamTwoId] INT NOT NULL, 
    [GameTime] datetime not null, 
	[LocationId] int NOT NULL,
	[LeagueId] int not null,
	[AddUserName] nvarchar(255) NOT NULL,
	[AddUpdateDt] datetime not null
)
GO

