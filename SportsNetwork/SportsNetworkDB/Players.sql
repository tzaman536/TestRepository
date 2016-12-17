IF OBJECT_ID('dbo.Players', 'U') IS NOT NULL 
  DROP TABLE dbo.Players;

CREATE TABLE [dbo].[Players]
(
	[PlayerId] INT NOT  NULL  IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Email] NVARCHAR(256) NOT NULL, 
    [Phone] NVARCHAR(50) NULL
)
