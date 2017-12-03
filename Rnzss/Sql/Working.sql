use rnzss


  select 
    rfq.*     
    ,coalesce(s.SolicitaionStatus,rfq.SolicitaionStatus,'Synch') as SolicitaionStatus
    ,e.RfqEvent
from [rnz].[RequestForQuote] rfq 
left join ( 
			select  
				re.RFQNo,EventDescription as  RfqEvent
			from [rnz].RequestForQuoteEvents re
			inner join (select RFQNo, max(RequestForQuoteEventId) as RequestForQuoteEventId from [rnz].RequestForQuoteEvents group by RFQNo ) me 
			on re.RequestForQuoteEventId = me.RequestForQuoteEventId
		  ) e on rfq.RFQNo = e.RFQNo
left join (select distinct solicitationno as SolicitationNumber,SolicitaionStatus from [rnz].Solicitations t) s on rfq.SolicitationNumber = s.SolicitationNumber

order by rfq.[UpdateDate]


select  
	re.RFQNo,EventDescription as  RfqEvent
from [rnz].RequestForQuoteEvents re
inner join (select RFQNo, max(RequestForQuoteEventId) as RequestForQuoteEventId from [rnz].RequestForQuoteEvents group by RFQNo ) me 
on re.RequestForQuoteEventId = me.RequestForQuoteEventId


select * 
from rnz.[InputHtml] t

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
