using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using RnzssWeb;

namespace RnzssWeb.Models
{
    public class RequestForQuote
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int RequestForQuoteId { get; set; }
        public string RFQNo { get; set; }
        public string CompanyName { get; set; }
        public string Attention { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime? DueDate { get; set; }
        public string SolicitationNumber { get; set; }
        public string RfqStatus { get; set; }
        public ProductInformation Product { get; set; }
        public int VendorId { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public string RfqEvent { get; set; }
        
        public void Reset()
        {
            RequestForQuoteId = 0;
            CompanyName = "";
            Attention = "";
            CompanyAddress = "";
            PhoneNo = "";
            FaxNo = "";
            Email = "";
            Comment = "";
            UpdatedBy = "";
        }
        
        public static int GetNextPkgRfqId(string cloneFromRfq)
        {
            cloneFromRfq = cloneFromRfq + "PKG";
            var pkgRfqs = GetAllStartMatch(cloneFromRfq);
            if (pkgRfqs != null && pkgRfqs.Any())
            {
                return pkgRfqs.Count() + 1;
            }
            return 1;
        }
        /* TZ */
        public static IEnumerable<RequestForQuote> GetAllStartMatch(string startMatchString)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RequestForQuote>(string.Format(@"

                                                        select 
                                                               rfq.*     
                                                        from [rnz].[RequestForQuote] rfq 
                                                        where RFQNo like '{0}%'
                                                        ", startMatchString), commandTimeout: 0).ToList();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }
        /* TZ */
        public static string CreatePackagingRfq(string cloneFromRfq)
        {
            try
            {
                int nextPkgRfqId = GetNextPkgRfqId(cloneFromRfq);
                var pkgRfq = GetRfq(cloneFromRfq);
                if (pkgRfq != null)
                {
                    string pkgRfqNo = string.Format("{0}PKG{1}", cloneFromRfq, nextPkgRfqId);
                    if (string.IsNullOrEmpty(pkgRfqNo))
                    {
                        return null;
                    }

                    pkgRfq.RFQNo = pkgRfqNo;
                    pkgRfq.RequestForQuoteId = 0;
                    if (Add(ref pkgRfq))
                    {
                        //Copy products here 
                        var products = ProductInformation.GetAll(cloneFromRfq);
                        if (products != null && products.Any())
                        {
                            foreach (var p in products)
                            {
                                p.ProductInformationId = 0;
                                p.RFQNo = pkgRfqNo;
                                ProductInformation.Upsert(p);
                            }
                        }


                        return pkgRfq.RFQNo;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }

            return string.Empty;
        }

        public static bool Add(ref RequestForQuote rfq)
        {

            rfq.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            if(string.IsNullOrEmpty(rfq.RfqStatus))
            {
                rfq.RfqStatus = RnzssWeb.RfqStatusList.Open.ToString();
            }

            #region Add RFQ
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                        INSERT INTO [rnz].[RequestForQuote]
                                               ([RFQNo]
                                               ,[CompanyName]
                                               ,[Attention]
                                               ,[CompanyAddress]
                                               ,[PhoneNo]
                                               ,[FaxNo]
                                               ,[Email]
                                               ,[Comment]
                                               ,[UpdatedBy]
                                               ,DueDate
                                               ,SolicitationNumber
                                               ,RfqStatus
                                               )
                                         VALUES
                                               (@RFQNo
                                               ,@CompanyName
                                               ,@Attention
                                               ,@CompanyAddress
                                               ,@PhoneNo
                                               ,@FaxNo
                                               ,@Email
                                               ,@Comment
                                               ,@UpdatedBy
                                               ,@DueDate
                                               ,@SolicitationNumber
                                               ,@RfqStatus
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

        public static IEnumerable<RequestForQuote> GetAll(bool includeClosedRfq = false)
        {
            string closedRfqStatusCode = RfqStatusList.Closed.ToString();
            string sql = string.Format(@" select 
                                rfq.*     
                                ,case when e.RFQNo  is null then 'Start' else 'View Log' end as RfqEvent 
                            from [rnz].[RequestForQuote] rfq 
                            left join ( select distinct RFQNo from [rnz].RequestForQuoteEvents ) e 
                                    on rfq.RFQNo = e.RFQNo
                            where RfqStatus not in ('{0}')
                            order by rfq.[UpdateDate] desc", closedRfqStatusCode);

            if(includeClosedRfq)
            {
                sql = string.Format(@" select 
                                rfq.*     
                                ,case when e.RFQNo  is null then 'Start' else 'View Log' end as RfqEvent 
                            from [rnz].[RequestForQuote] rfq 
                            left join ( select distinct RFQNo from [rnz].RequestForQuoteEvents ) e 
                                    on rfq.RFQNo = e.RFQNo
                            order by rfq.[UpdateDate] desc");
            }

            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RequestForQuote>(sql, commandTimeout: 0).ToList();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static RequestForQuote GetRfq(string rfqNo)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RequestForQuote>(@"
                                                        select *     
                                                        from [rnz].[RequestForQuote]
                                                        where RFQNo = @rfqNo
                                                        ", new { rfqNo }, commandTimeout: 0).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static string GetNextRfqId()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    string sql = @"
                                        INSERT INTO [rnzss].[rnz].[RequestForQuoteIdTable] DEFAULT VALUES;
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                    var id = connection.Query<int>(sql, commandTimeout: 3000).Single();
                    return string.Format("RZRFQ{0}", id);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return null;
                }

            }



        }

        public static bool Upsert(ref RequestForQuote rfq)
        {
            try
            {
                // Save vendir information
                Vendor v = rfq.GetVendor();
                Vendor.Upsert(v);



                rfq.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrEmpty(rfq.RFQNo))
                {
                    rfq.RFQNo = GetNextRfqId();
                }
                else
                {
                    // Call update method here 
                    return Update(rfq);
                }

                Add(ref rfq);
                return true;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return false;
            }


        }

        public static bool Delete(RequestForQuote p)
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

                                                    DELETE [rnz].[ProductInformation] WHERE RFQNo=@RFQNo
                                                    DELETE rnz.RequestForQuoteEvents  WHERE RFQNo=@RFQNo
                                                    DELETE FROM [rnz].[RequestForQuote] WHERE RequestForQuoteId = @RequestForQuoteId
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

        public static bool Update(RequestForQuote p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            if(p.RequestForQuoteId ==0)
            {
                if (string.IsNullOrEmpty(p.RFQNo))
                    return false;

                var rfq = RequestForQuote.GetRfq(p.RFQNo);
                p.RequestForQuoteId = rfq.RequestForQuoteId;
                if (string.IsNullOrEmpty(p.RfqStatus))
                    p.RfqStatus = rfq.RfqStatus;
            }




            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [rnz].[RequestForQuote]
                                                   SET [CompanyName] = @CompanyName
                                                      ,[Attention] = @Attention
                                                      ,[CompanyAddress] = @CompanyAddress
                                                      ,[PhoneNo] = @PhoneNo
                                                      ,[FaxNo] = @FaxNo
                                                      ,[Email] = @Email
                                                      ,[Comment] = @Comment
                                                      ,DueDate = @DueDate
                                                      ,SolicitationNumber = @SolicitationNumber
                                                      ,RfqStatus = @RfqStatus
                                                      ,[UpdatedBy] = @UpdatedBy
                                                      ,[UpdateDate] = getutcdate()
                                                 WHERE RequestForQuoteId = @RequestForQuoteId
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

        public Vendor GetVendor()
        {
            return new Vendor() {
                CompanyName = CompanyName
                ,Attention = Attention
                ,CompanyAddress = CompanyAddress
                ,PhoneNo = PhoneNo
                ,FaxNo = FaxNo
                ,Email = Email
                ,VendorId = VendorId
            };
        }

        public static bool UpdateRfqStatus(string rfqNo, RfqStatusList status)
        {
            try
            {
                if(string.IsNullOrEmpty(rfqNo))
                {
                    return false;
                }
                var rfq = GetRfq(rfqNo);
                if (rfq == null)
                    return false;



                switch(status){
                    case RfqStatusList.Sent:
                        if (rfq.RfqStatus.Equals(RfqStatusList.Open.ToString()))
                        {
                            rfq.RfqStatus = status.ToString();
                            return Update(rfq);
                        }
                        break;
                    case RfqStatusList.ReadyToBid:
                        if (rfq.RfqStatus.Equals(RfqStatusList.Open.ToString()) ||
                            rfq.RfqStatus.Equals(RfqStatusList.Sent.ToString())
                            )
                        {
                            rfq.RfqStatus = status.ToString();
                            return Update(rfq);
                        }
                        break;

                    default:
                        return false;
                }


                


            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return false;
            }

            return false;
        }


    }
}