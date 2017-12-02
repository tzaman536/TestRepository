use rnzss
select * from rnz.[InputHtml]

declare @RfqNo nvarchar(200) ='RZRFQ10009'


alter table [rnz].[RequestForQuote] add SolicitaionStatus nvarchar(50) null

select * 
from [rnz].[RequestForQuote] t
WHERE PONo is not null

select * from [rnz].[ProductInformation] WHERE RFQNo=@RfqNo
select * from [rnz].[RequestForQuote] WHERE RFQNo=@RfqNo
select * from rnz.RequestForQuoteEvents  WHERE RFQNo=@RfqNo
select * from rnz.DocumentStore WHERE LinkId=@RfqNo

alter table [rnz].[RequestForQuote] add SolicitaionStatus nvarchar(50) null


select *
from [rnz].[Vendors]
where companyname like 'Tal%'

select * from [rnz].[ProductInformation] where quantity = '11'

select * from [rnz].[ProductInformation] where Partname like '%pin%'
select * from [rnz].[ProductInformation] where PartDescription like '%inge%'
select * 
from [rnz].[RequestForQuote] 
where RfqStatus = 'Closed'
order by 1 desc


select distinct solicitationno,SolicitaionStatus from [rnz].Solicitations t
select * from rnz.DocumentStore


select * from rnz.Vendors



select rfq.RfqNo,p.*
from [rnz].[ProductInformation] p
left join [rnz].[RequestForQuote] rfq on p.RfqNo = rfq.RfqNo
where rfq.RfqNo is null
