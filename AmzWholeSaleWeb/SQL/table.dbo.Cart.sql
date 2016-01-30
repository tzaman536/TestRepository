IF OBJECT_ID('dbo.Cart', 'U') IS NOT NULL
  DROP TABLE dbo.Cart; 

create table dbo.Cart
(
	CartID int identity not null,
	UserName nvarchar(200) not null,
	DateCreated datetime not null,
	CheckedOut bit not null
)
GO



ALTER TABLE dbo.Cart
ADD CONSTRAINT PK_CartID PRIMARY KEY (CartID)
GO
