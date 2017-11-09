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
	[RFQNo] [nvarchar](200) NULL,
	[PartName] [nvarchar](200) NULL,
	[PartNumber] [nvarchar](200) NULL,
	[PartDescription] [nvarchar](255) NULL,
	[Quantity] [nvarchar](100) NULL,
	[CN] [nvarchar](100) NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[ProductInformation] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO


CREATE TABLE [rnz].[RequestForQuote](
	[RequestForQuoteId] [int] IDENTITY(1,1) NOT NULL,
	[RFQNo] [nvarchar](200) NULL,
	[CompanyName] [nvarchar](200) NULL,
	[Attention] [nvarchar](200) NULL,
	[CompanyAddress] [nvarchar](255) NULL,
	[PhoneNo] [nvarchar](100) NULL,
	[FaxNo] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Comment] [nvarchar](255) NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [rnz].[RequestForQuote] ADD  DEFAULT (getutcdate()) FOR [UpdateDate]
GO



/****** Object:  Table [rnz].[RequestForQuoteIdTable]    Script Date: 11/9/2017 10:50:44 AM ******/

CREATE TABLE [rnz].[RequestForQuoteIdTable](
	[NextId] [int] IDENTITY(10000,1) NOT NULL
) ON [PRIMARY]

GO


