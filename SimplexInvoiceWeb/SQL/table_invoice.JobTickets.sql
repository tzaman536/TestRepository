IF OBJECT_ID('invoice.JobTickets', 'U') IS NOT NULL 
  DROP TABLE invoice.JobTickets; 

CREATE TABLE invoice.JobTickets
(
	JobTicketId int NOT NULL IDENTITY (1000, 1),
	CompanyId int not null,
	ClientCompanyId int not null,
	JobDate date not null,
	DeliveryDate date not null,
    Quantity int not null,
    [Weight] decimal(25,2) null,
    Milage decimal (25,2) null,
    Toll decimal(25,2) null,
    FuelSurcharge decimal(25,2) null,
    MiscFee decimal(25,2) null,
    TotalCharge decimal(25,2) null,
    WaitTime nvarchar(25) null,
    PickupFrom text null,
    PickupFromContact nvarchar(100) null,
    PickupFromPhone nvarchar(100) null,
    DeliverTo text null,
    DeliverToContact nvarchar(100) null,
    DeliverToPhone nvarchar(100) null,
    Instruction nvarchar(255) null,
    ServiceType nvarchar(100) null,
    DeliveryAgent nvarchar(200) null,
    POD nvarchar(200) null,
    Comments nvarchar(255) null,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NULL,
	ModifiedAt datetime NULL,
	)  ON [PRIMARY]
	 
GO
ALTER TABLE invoice.JobTickets SET (LOCK_ESCALATION = TABLE)
GO

