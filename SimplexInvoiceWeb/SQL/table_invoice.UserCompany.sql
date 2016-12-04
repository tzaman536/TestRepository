IF OBJECT_ID('invoice.UserCompany', 'U') IS NOT NULL 
  DROP TABLE invoice.UserCompany; 

CREATE TABLE invoice.UserCompany
	(
	UserCompanyId int NOT NULL IDENTITY (1, 1),
	UserId nvarchar(200) NOT NULL,
	CompanyId nvarchar(200) NOT NULL,
	CreatedBy nvarchar(50) NOT NULL,
	CreatedAt datetime NOT NULL,
	ModifiedBy nvarchar(50) NULL,
	ModifiedAt datetime NULL

	)  ON [PRIMARY]
	 
GO

