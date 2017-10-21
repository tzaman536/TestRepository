IF OBJECT_ID('SportsNetwork.dbo.AppSettings', 'U') IS NOT NULL 
  DROP TABLE SportsNetwork.dbo.AppSettings;
GO


CREATE TABLE SportsNetwork.[dbo].[AppSettings]
(
	[Id] INT NOT  NULL  IDENTITY, 
	AppKey NVARCHAR(255) NOT NULL, 
	AppValue NVARCHAR(255) NOT NULL
)

insert into SportsNetwork.[dbo].[AppSettings](AppKey,AppValue)
select 'SMTP_SERVER','smtp.gmail.com'
union 
select 'SMTP_PORT','587'
union 
select 'MAIL_FROM','536phenix@gmail.com'
union 
select 'MAIL_FROM_CRED','Phenix536'

