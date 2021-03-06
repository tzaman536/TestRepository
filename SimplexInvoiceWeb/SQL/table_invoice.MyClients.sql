IF OBJECT_ID('invoice.MyClients', 'U') IS NOT NULL 
  DROP TABLE invoice.MyClients; 

CREATE TABLE invoice.MyClients
	(
	ClientCompanyId int NOT NULL IDENTITY (1, 1),
	CompanyId int not null,
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
	BillToName nvarchar(200) NOT NULL,
	BillToAddressLine1 nvarchar(255)  NOT NULL,
	BillToAddressLine2 nvarchar(255) NULL,
	BillToCity nvarchar(200) NOT NULL,
	BillToState nvarchar(25) NOT NULL,
	BillToZip nvarchar(20) NOT NULL,
	ComplimentaryWeight int not null,
	WeightRate int not null,
	BasePickupCharge decimal(25,2) not null,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NULL,
	ModifiedAt datetime NULL,
	CONSTRAINT PK_Company_ClientCompanyName PRIMARY KEY  (CompanyName,CompanyId)
	)  ON [PRIMARY]
	 
GO
ALTER TABLE invoice.MyClients SET (LOCK_ESCALATION = TABLE)
GO

