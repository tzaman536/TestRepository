IF OBJECT_ID ('amz.Sections', 'U') IS NOT NULL 
   drop table amz.Sections

create table amz.Sections
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

ALTER TABLE amz.Sections
ADD CONSTRAINT PK_Sections_SectionName PRIMARY KEY (SectionName)
GO



