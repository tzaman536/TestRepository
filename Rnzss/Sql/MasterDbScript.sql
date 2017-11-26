USE [rnzss]
GO


/****** Object:  Table [rnz].[ProductInformation]    Script Date: 11/9/2017 10:26:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA rnz
GO


CREATE TABLE [rnz].[ProductInformation](
	[ProductInformationId] [int] IDENTITY(1,1) NOT NULL,
	[RFQNo] [nvarchar](50) NULL,
	[PartName] [nvarchar](200) NULL,
	[PartNumber] [nvarchar](200) NULL,
	[PartDescription] VARCHAR(MAX),
	[Quantity] [nvarchar](100) NULL,
	[CN] [nvarchar](100) NULL,
	VendorPrice decimal(18,2) null,
	PkgCost decimal(18,2) null,
	DeliverIn int null,
	DeliverInUnit nvarchar(10) null,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[ProductInformation] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO


CREATE TABLE [rnz].[RequestForQuote](
	[RequestForQuoteId] [int] IDENTITY(1,1) NOT NULL,
	[RFQNo] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](200) NULL,
	[Attention] [nvarchar](200) NULL,
	[CompanyAddress] [nvarchar](255) NULL,
	[PhoneNo] [nvarchar](100) NULL,
	[FaxNo] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Comment] [nvarchar](255) NULL,
	DueDate date null,
	SolicitationNumber nvarchar(200) null,
	RfqStatus nvarchar(50) NOT NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[RequestForQuote] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO

ALTER TABLE rnz.RequestForQuote ADD CONSTRAINT Pk_RequestForQuote PRIMARY KEY (RFQNo)  
GO


/****** Object:  Table [rnz].[RequestForQuoteIdTable]    Script Date: 11/9/2017 10:50:44 AM ******/

CREATE TABLE [rnz].[RequestForQuoteIdTable](
	[NextId] [int] IDENTITY(10000,1) NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [rnz].[RequestForQuoteEvents](
	[RequestForQuoteEventId] [int] IDENTITY(1,1) NOT NULL,
	[RFQNo] [nvarchar](50) NOT NULL,
	[EventDescription] [nvarchar](255) NOT NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[RequestForQuoteEvents] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO

CREATE TABLE [rnz].[DocumentStore](
	[DocumentStoreId] [int] IDENTITY(1,1) NOT NULL,
	[LinkId] [nvarchar](50) NOT NULL,
	[FileBaseName] [nvarchar](100) NOT NULL,
	[FileExtension] [nvarchar](20) NOT NULL,
	[ContentType] [nvarchar](100) NULL,
	[BinaryData] [varbinary](max) NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [rnz].[DocumentStore] ADD CONSTRAINT Pk_DocumentStgore PRIMARY KEY (LinkId,FileBaseName,FileExtension)  

ALTER TABLE [rnz].[DocumentStore] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO


CREATE TABLE [rnz].[FileUploadLog](
	[FileUploadLogId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[Message] [text] NULL,
	[FileUploaded] [bit] NOT NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [rnz].[FileUploadLog] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO
CREATE TABLE [rnz].[Solicitations](
	[SolicitationId] [int] IDENTITY(1,1) NOT NULL,
	[SolicitationNo] [nvarchar](50) NOT NULL,
	[SolicitationDescription] [nvarchar](255) NULL,
	[AwardQuantity] [int] NULL,
	[AwardAmount] [decimal](18, 0) NULL,
	[DueDate] [date] NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[Solicitations] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO

ALTER TABLE rnz.Solicitations ADD CONSTRAINT Pk_Solicitations PRIMARY KEY (SolicitationNo)  
GO


CREATE TABLE [rnz].[Vendors](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](200) NOT NULL,
	[Attention] [nvarchar](200) NULL,
	[CompanyAddress] [nvarchar](255) NULL,
	[PhoneNo] [nvarchar](100) NULL,
	[FaxNo] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[Vendors] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO

ALTER TABLE rnz.Vendors ADD CONSTRAINT Pk_Vendors PRIMARY KEY (CompanyName)  
GO


