IF OBJECT_ID('dbo.CartItems', 'U') IS NOT NULL
  DROP TABLE dbo.CartItems; 

create table dbo.CartItems
(
	CartItemID int identity not null,
	CartID int not null,
	ProductID int not null,
	Quantity int not null,
	Price decimal(25,2) not null
)
GO


ALTER TABLE dbo.CartItems
ADD CONSTRAINT PK_CartItems_CartItemID PRIMARY KEY (CartItemID)
GO


ALTER TABLE dbo.CartItems
ADD CONSTRAINT FK_Cart_CartID FOREIGN KEY (CartID)
REFERENCES dbo.Cart(CartID)
GO

ALTER TABLE dbo.CartItems
ADD CONSTRAINT uc_CartIdProductId UNIQUE (CartID,ProductID)
