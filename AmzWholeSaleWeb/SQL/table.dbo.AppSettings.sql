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

/* AMZ Wholesale
delete AMZ.dbo.AppSettings
insert into AMZ.dbo.AppSettings(AppKey,AppValue) values ('PayPalBusiness','tamimzaman10@yahoo.com')
insert into AMZ.dbo.AppSettings(AppKey,AppValue) values ('PayPalReturnUrl','http://www.amzwholesale.com')
insert into AMZ.dbo.AppSettings(AppKey,AppValue) values ('TechSupportEmail','tzaman536@gmail.com')
insert into AMZ.dbo.AppSettings(AppKey,AppValue) values ('HomeView','http://www.amzwholesale.com')
insert into AMZ.dbo.AppSettings(AppKey,AppValue) values ('HomeController','AmzHome')
select * from AMZ.dbo.AppSettings
*/


/* MASSDATAUSA
insert into MASSDATAUSA.dbo.AppSettings(AppKey,AppValue) values ('PayPalBusiness','sales@massdataus.com')
insert into MASSDATAUSA.dbo.AppSettings(AppKey,AppValue) values ('PayPalReturnUrl','http://www.massdatausa.com')
insert into MASSDATAUSA.dbo.AppSettings(AppKey,AppValue) values ('TechSupportEmail','tzaman536@gmail.com')
insert into MASSDATAUSA.dbo.AppSettings(AppKey,AppValue) values ('HomeView','http://www.massdatausa.com')
insert into MASSDATAUSA.dbo.AppSettings(AppKey,AppValue) values ('HomeController','AmzHome')
update t
set AppValue = 'sales@massdataus.com'
where AppKey = 'PayPalBusiness'
select * from MASSDATAUSA.dbo.AppSettings
*/
