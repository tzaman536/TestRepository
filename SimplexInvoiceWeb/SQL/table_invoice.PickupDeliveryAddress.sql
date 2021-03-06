IF OBJECT_ID('invoice.PickupDeliveryAddress', 'U') IS NOT NULL 
  DROP TABLE invoice.PickupDeliveryAddress; 

CREATE TABLE invoice.PickupDeliveryAddress
	(
	LocationID int NOT NULL IDENTITY (1, 1),
	Location nvarchar(255) NOT NULL,
	Name nvarchar(200) NULL,
	Line1 nvarchar(255) NOT NULL,
	City nvarchar(200) NOT NULL,
	State nvarchar(25) NULL,
	Zip nvarchar(20) NULL,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NULL,
	ModifiedAt datetime NULL,
	CONSTRAINT PK_PickupDeliveryAddress_Location PRIMARY KEY  (Location)
	)  ON [PRIMARY]
	 
GO
ALTER TABLE invoice.PickupDeliveryAddress SET (LOCK_ESCALATION = TABLE)
GO
