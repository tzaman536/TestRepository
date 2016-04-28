/*
   Saturday, November 21, 20156:27:17 AM
   User: 
   Server: TZAMAN-HP\RZ
   Database: Phenix
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE invoice.Company
	(
	CompanyId int NOT NULL IDENTITY (1, 1),
	CompanyType nvarchar(25) NOT NULL,
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
	Message text NULL,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NOT NULL,
	ModifiedAt datetime NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE invoice.Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'CONTROL') as Contr_Per 

CREATE CLUSTERED INDEX IX_Company_CompanyName
    ON invoice.Company(CompanyName); 
GO