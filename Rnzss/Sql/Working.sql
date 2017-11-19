declare @RfqNo nvarchar(200) ='RZRFQ10113'


select * from [rnz].[ProductInformation] WHERE RFQNo=@RfqNo
select * from rnz.RequestForQuoteEvents  WHERE RFQNo=@RfqNo
select * from [rnz].[RequestForQuote] WHERE RFQNo=@RfqNo

select * from [rnz].[RequestForQuote] order by 1 desc
select * from [rnz].Solicitations
select * from rnz.DocumentStore


select * from rnz.Vendors



select rfq.RfqNo,p.*
from [rnz].[ProductInformation] p
left join [rnz].[RequestForQuote] rfq on p.RfqNo = rfq.RfqNo
where rfq.RfqNo is null
