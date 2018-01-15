IF OBJECT_ID ('amz.Products', 'U') IS NOT NULL 
   drop table amz.Products

create table amz.Products
(
	ProductID int identity not null,
	ProductName nvarchar(150) not null,
	ProductDescription nvarchar(255) not null,
	ProductLongDescription text null,
	UnitPrice decimal(25,2) not null,
	UnitsInStock int not null,
	UnitsOnOrder int not null,
	Discontinued bit not null,
	LastSupply datetime null,
	ImageUploadSuccessful bit not null,
	SmallImageId nvarchar(150) not null,
	MediumImageId nvarchar(150) not null,
	LargeImageId nvarchar(150) not null,
	OriginalImageId nvarchar(150) not null,
	ImageIdOne nvarchar(150) null,
	ImageIdTwo nvarchar(150) null,
	ImageIdThree nvarchar(150) null,
	ImageIdFour nvarchar(150) null,
	ImageIdFive nvarchar(150) null,
	SectionID int null,
	AddDate datetime not null,
	AddedBy nvarchar(50) not null,
	ModifiedDate datetime null,
	ModifiedBy nvarchar(50) null
)

ALTER TABLE amz.Products
ADD CONSTRAINT PK_Products_ProductName PRIMARY KEY (ProductName)
GO
