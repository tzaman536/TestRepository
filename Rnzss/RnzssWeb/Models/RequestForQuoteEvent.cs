using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RnzssWeb.Models
{
    public class RequestForQuoteEvent
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int RequestForQuoteEventId { get; set; }
        public string RFQNo { get; set; }
        public string EventDescription { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }






        public static IEnumerable<RequestForQuoteEvent> GetRfqEvent(string rfqNo)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RequestForQuoteEvent>(@"
                                                        select *     
                                                        from [rnz].[RequestForQuoteEvents]
                                                        where RFQNo = @rfqNo
                                                        order by [UpdateDate] desc
                                                        ", new { rfqNo }, commandTimeout: 0).ToList();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static bool Add(RequestForQuoteEvent rfq)
        {

            rfq.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;


            #region Add RFQ Event
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                            INSERT INTO [rnz].[RequestForQuoteEvents]
                                                       ([RFQNo]
                                                       ,[EventDescription]
                                                       ,[UpdatedBy]
                                                       )
                                                 VALUES
                                                       (@RFQNo
                                                       ,@EventDescription
                                                       ,@UpdatedBy
                                                       )
                                                        ", rfq, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }
            }
            #endregion

            return true;


        }

        public static bool Delete(RequestForQuoteEvent p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            // TODO: Check if this RFQNo and part number exists alreayd then call update and return from here
            //if (ProductExists(p.RFQNo,p.PartNumber))
            //{
            //    return true;
            //}


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                        DELETE FROM [rnz].[RequestForQuoteEvents]
                                                              WHERE RequestForQuoteEventId = @RequestForQuoteEventId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }
        public static bool Update(RequestForQuoteEvent p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;



            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                            UPDATE [rnz].[RequestForQuoteEvents]
                                               SET [RFQNo] = @RFQNo
                                                  ,[EventDescription] = @EventDescription
                                                  ,[UpdatedBy] = @UpdatedBy
                                                  ,[UpdateDate] = getutcdate()
                                               WHERE RequestForQuoteEventId = @RequestForQuoteEventId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

    }
}
