use rnzss
/* Search Product
select * from [rnz].[ProductInformation] where quantity = '11'
select * from [rnz].[ProductInformation] where Partname like '%5960%'
select * from [rnz].[ProductInformation] where PartDescription like '%MG%'
*/

select top 100 * from [rnz].[ProductInformation] WHERE PartNumber like '%X1024-503%'

select SolicitationNumber,*
from [rnz].[RequestForQuote] t
where SolicitationNumber like '%SPE4A618T6086%' 
select *
from [rnz].[RequestForQuote] t
where SolicitationNumber like ' %' 


select LinkId, count(*)
from rnz.DocumentStore 
group by LinkId
having count(*) > 1

WHERE LinkId=@RfqNo


select *
from rnz.DocumentStore 
where LinkId like 'RZRFQ10321'
order by LinkId

select *
from rnz.DocumentStore 
where FileExtension like '%C4%'


select * from rnz.[InputHtml] t

declare @RfqNo nvarchar(200) ='RZRFQ10009'


alter table [rnz].[RequestForQuote] add SolicitaionStatus nvarchar(50) null

select * 
from [rnz].[RequestForQuote] t
WHERE PONo is not null

select * from [rnz].[ProductInformation] WHERE RFQNo=@RfqNo
select * from [rnz].[RequestForQuote] WHERE RFQNo=@RfqNo
select * from rnz.RequestForQuoteEvents  WHERE RFQNo=@RfqNo

alter table [rnz].[RequestForQuote] add SolicitaionStatus nvarchar(50) null


select *
from [rnz].[Vendors]
where companyname like 'Tal%'

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
