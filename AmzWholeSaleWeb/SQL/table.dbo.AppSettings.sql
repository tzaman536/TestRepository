IF OBJECT_ID('dbo.AppSettings', 'U') IS NOT NULL
  DROP TABLE dbo.AppSettings; 

create table dbo.AppSettings
(
	AppSettingsID int identity not null,
	AppKey nvarchar(255) not null,
	AppValue nvarchar(255) not null,
)
GO



ALTER TABLE dbo.AppSettings
ADD CONSTRAINT PK_AppKey PRIMARY KEY (AppKey)
GO


insert into AppSettings(AppKey,AppValue) values ('PayPalBusiness','tamimzaman10@yahoo.com')
insert into AppSettings(AppKey,AppValue) values ('PayPalReturnUrl','http://www.amzwholesale.com')
insert into AppSettings(AppKey,AppValue) values ('TechSupportEmail','tzaman536@gmail.com')
insert into AppSettings(AppKey,AppValue) values ('HomeView','Index')
insert into AppSettings(AppKey,AppValue) values ('HomeController','AmzHome')
