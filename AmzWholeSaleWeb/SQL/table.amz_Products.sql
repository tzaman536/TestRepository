IF OBJECT_ID (N'amz_Products', N'U') IS NOT NULL 
   drop table phenix.amz_Products

create table phenix.amz_Products
(
	ProductID int identity not null,
	ProductName nvarchar(100) not null,
	ProductDescription nvarchar(200) not null,
	Price decimal(25,2) not null,
	UnitsInStock int not null,
	IsActive  bit not null,
	AddDate datetime not null,
	AddedBy nvarchar(50) not null,
	ModifiedDate datetime null,
	ModifiedBy nvarchar(50) null
)
