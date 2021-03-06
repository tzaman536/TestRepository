IF OBJECT_ID('invoice.Company', 'U') IS NOT NULL 
  DROP TABLE invoice.Company; 

CREATE TABLE invoice.Company
	(
	CompanyId int NOT NULL IDENTITY (1, 1),
	CompanyName nvarchar(200) NOT NULL,
	ContactPerson nvarchar(200) NULL,
	AddressLine1 nvarchar(255) NOT NULL,
	AddressLine2 nvarchar(255) NULL,
	City nvarchar(200) NOT NULL,
	State nvarchar(25) NULL,
	Zip nvarchar(20) NULL,
	Email nvarchar(200) NULL,
	MobileNumber nvarchar(100) NULL,
	OfficeNumber nvarchar(100) NULL,
	FaxNumber nvarchar(100) NULL,
	ComplimentaryWeight int not null,
	WeightRate int not null,
	BasePickupCharge decimal(25,2) not null,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NULL,
	ModifiedAt datetime NULL,
	CONSTRAINT PK_Company_CompanyName PRIMARY KEY  (CompanyName)
	)  ON [PRIMARY]
	 
GO
ALTER TABLE invoice.Company SET (LOCK_ESCALATION = TABLE)
GO
select Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'CONTROL') as Contr_Per 
GO

