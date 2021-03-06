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
CREATE TABLE dbo.ContactDetail
	(
	ContactDetailId int NOT NULL IDENTITY (1, 1),
	CompanyName nvarchar(255) NOT NULL,
	FirstName nvarchar(255) NOT NULL,
	LastName nvarchar(255) NULL,
	Email nvarchar(255) NULL,
	MobileNumber nvarchar(100) NULL,
	OfficeNumber nvarchar(255) NULL,
	Message text NULL,
	CreatedAt datetime NOT NULL,
	CreatedBy nvarchar(50) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.ContactDetail SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContactDetail', 'Object', 'CONTROL') as Contr_Per 