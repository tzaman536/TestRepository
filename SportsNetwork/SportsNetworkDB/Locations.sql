IF OBJECT_ID('SportsNetwork.dbo.Locations', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.Locations;

CREATE TABLE [SportsNetwork].[dbo].[Locations]
(
	[LocationId] INT NOT  NULL  IDENTITY, 
    [LocationName] NVARCHAR(255) NOT NULL, 
    [LoctionDetail] NVARCHAR(255) NULL, 
    [AddressLink] text NULL, 
	[AddUserName] nvarchar(100) NOT NULL,
	[AddUpdateDt] datetime not null
)
GO

ALTER TABLE [SportsNetwork].[dbo].[Locations]
ADD CONSTRAINT PK_Locations PRIMARY KEY (LocationName);
GO
