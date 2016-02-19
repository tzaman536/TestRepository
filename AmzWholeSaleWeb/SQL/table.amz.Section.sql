IF OBJECT_ID ('amz.Section', 'U') IS NOT NULL 
   drop table amz.Section

create table amz.Section
(
	SectionID int identity not null,
	SectionName nvarchar(50) not null,
	SectionDescription nvarchar(255) not null,
	AddDate datetime not null,
	AddedBy nvarchar(50) not null,
	ModifiedDate datetime null,
	ModifiedBy nvarchar(50) null
)
GO

ALTER TABLE amz.Section
ADD CONSTRAINT PK_Section_SectionName PRIMARY KEY (SectionName)
GO



