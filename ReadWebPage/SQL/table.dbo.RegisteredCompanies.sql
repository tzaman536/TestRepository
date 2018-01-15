IF OBJECT_ID ('dbo.RegisteredCompanies', 'U') IS NOT NULL 
   drop table dbo.RegisteredCompanies

create table dbo.RegisteredCompanies
(
	RegisteredCompanyId int identity not null,
	CompanyName nvarchar(200) not null,
	CompanyAddress text not null,
	FilingDate datetime not null,
	DOSID nvarchar(50) not null,
	MailSent bit not null,
	AddDate datetime not null,
	AddedBy nvarchar(50) not null,
	ModifiedDate datetime null,
	ModifiedBy nvarchar(50) null
)

ALTER TABLE amz.Products
ADD CONSTRAINT PK_Products_ProductName PRIMARY KEY (ProductName)
GO
