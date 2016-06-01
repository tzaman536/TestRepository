using Dapper;
using log4net;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceBL
{
    public class JobTicketHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(JobTicketHandler));

        public int Add(JobTicket jt, string currentUser)
        {
            try {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    var result = conn.Query<int>(@"
                    insert into invoice.JobTickets(CompanyId,ClientCompanyId,JobDate,DeliveryDate,Quantity,[Weight],Milage,Toll,FuelSurcharge,
                                                    MiscFee,TotalCharge,WaitTime,PickupFrom,DeliverTo,Instruction,ServiceType,
                                                    DeliveryAgent,POD,Comments,CreatedBy,CreatedAt)
                                           values(@CompanyId,@ClientCompanyId,@JobDate,@DeliveryDate,@Quantity,@Weight,@Milage,@Toll,@FuelSurcharge,
                                                    @MiscFee,@TotalCharge,@WaitTime,@PickupFrom,@DeliverTo,@Instruction,@ServiceType,
                                                    @DeliveryAgent,@POD,@Comments,@CreatedBy,getutcdate());
                                           SELECT SCOPE_IDENTITY()

                   
                                            ", jt);

                    return result.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                logger.Fatal(ex);
                return 0;
            }
        }

        public int Update(JobTicket jt)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    var result = conn.Query<int>(@"
                                        update invoice.JobTickets
                                        set ClientCompanyId = @ClientCompanyId
                                            ,JobDate = @JobDate
                                            ,DeliveryDate = @DeliveryDate
                                            ,Quantity = @Quantity
                                            ,[Weight] = @Weight
                                            ,Milage = @Milage
                                            ,Toll = @Toll
                                            ,FuelSurcharge = @FuelSurcharge
                                            ,MiscFee = @MiscFee
                                            ,TotalCharge = @TotalCharge
                                            ,WaitTime = @WaitTime
                                            ,PickupFrom = @PickupFrom
                                            ,DeliverTo = @DeliverTo
                                            ,Instruction = @Instruction
                                            ,ServiceType = @ServiceType
                                            ,DeliveryAgent = @DeliveryAgent
                                            ,POD = @POD
                                            ,Comments = @Comments
                                            ,ModifiedBy = @ModifiedBy
                                            ,ModifiedAt = getutcdate()
                                        where JobTicketId = @JobTicketId
                                            ", jt);

                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return 0;
            }
        }


        public IEnumerable<JobTicket> GetTodaysTickes(LogisticsCompany lc)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    return conn.Query<JobTicket>(@"
                                                    select mc.CompanyName as ClientName, jt.*
                                                    from invoice.JobTickets jt
                                                    inner join invoice.MyClients mc on jt.ClientCompanyId = mc.ClientCompanyId
                                                    where jt.CreatedAt >= (cast(GETDATE()-6 as date))                                                
                                                      and jt.CompanyId = @CompanyId
                                            ", new { CompanyId = lc.CompanyId });

                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }


        public JobTicket GetJobTicket(int jobTicketId)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    return conn.Query<JobTicket>(@"
                                                    select mc.CompanyName as ClientName, jt.*
                                                    from invoice.JobTickets jt
                                                    inner join invoice.MyClients mc on jt.ClientCompanyId = mc.ClientCompanyId
                                                    where jt.JobTicketId = @jobTicketId
                                            ", new { jobTicketId = jobTicketId }).ElementAtOrDefault(0);

                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }
    }
}
