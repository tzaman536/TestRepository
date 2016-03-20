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

/* Local Settings
delete AppSettings
insert into AppSettings(AppKey,AppValue) values ('SMTP_SERVER','smtp.gmail.com')
insert into AppSettings(AppKey,AppValue) values ('SMTP_PORT','587')
insert into AppSettings(AppKey,AppValue) values ('MAIL_FROM','536phenix@gmail.com')
insert into AppSettings(AppKey,AppValue) values ('MAIL_FROM_CRED','Phenix536')
insert into AppSettings(AppKey,AppValue) values ('MAIL_SALES_TEAM','tazman536@yahoo.com')
select * from AppSettings
*/

/* Server Settings
delete AppSettings
insert into AppSettings(AppKey,AppValue) values ('SMTP_SERVER','localhost')
insert into AppSettings(AppKey,AppValue) values ('SMTP_PORT','25')
insert into AppSettings(AppKey,AppValue) values ('MAIL_FROM','send@simplexsys.co')
insert into AppSettings(AppKey,AppValue) values ('MAIL_FROM_CRED','send')
insert into AppSettings(AppKey,AppValue) values ('MAIL_SALES_TEAM','tazman536@yahoo.com')
select * from AppSettings
*/