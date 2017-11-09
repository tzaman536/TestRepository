using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SportsNetworkWeb.Models
{
    public class RequestForQuote
    {
        public int RequestForQuoteId { get; set; }
        public string RFQNo { get; set; }
        public string CompanyName { get; set; }
        public string Attention { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public ProductInformation Product { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



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


        public static IEnumerable<RequestForQuote> GetAll()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RequestForQuote>(@"
                                                        select *     
                                                        from [Carry].[RequestForQuote]
                                                        ", commandTimeout: 0).ToList();
                }
                catch (Exception ex)
                {

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
                                                        from [Carry].[RequestForQuote]
                                                        where RFQNo = @rfqNo
                                                        ", new { rfqNo }, commandTimeout: 0).FirstOrDefault();
                }
                catch (Exception ex)
                {

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
                                        INSERT INTO [FrontOffice].[Carry].[RequestForQuoteIdTable] DEFAULT VALUES;
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                    var id = connection.Query<int>(sql, commandTimeout: 3000).Single();
                    return string.Format("RZRFQ{0}", id);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }



        }

        public static bool Upsert(ref RequestForQuote rfq)
        {

            rfq.UpdatedBy = Environment.UserName;

            if (string.IsNullOrEmpty(rfq.RFQNo))
            {
                rfq.RFQNo = GetNextRfqId();
            }
            else
            {
                // Call update method here 
                #region Update RFQ
                using (IDbConnection connection = CommonMethods.OpenConnection())
                {
                    try
                    {
                        var result = connection.Execute(@"
                                    UPDATE [Carry].[RequestForQuote]
                                       SET [CompanyName] = @CompanyName
                                          ,[Attention] = @Attention
                                          ,[CompanyAddress] = @CompanyAddress
                                          ,[PhoneNo] = @PhoneNo
                                          ,[FaxNo] = @FaxNo
                                          ,[Email] = @Email
                                          ,[Comment] = @Comment
                                          ,[UpdatedBy] = @UpdatedBy
                                     WHERE [RFQNo] = @RFQNo
                                                        ", rfq, commandTimeout: 0);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }

                #endregion

                return true;
            }

            #region Add RFQ
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                        INSERT INTO [Carry].[RequestForQuote]
                                               ([RFQNo]
                                               ,[CompanyName]
                                               ,[Attention]
                                               ,[CompanyAddress]
                                               ,[PhoneNo]
                                               ,[FaxNo]
                                               ,[Email]
                                               ,[Comment]
                                               ,[UpdatedBy]
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
                                               )

                                                        ", rfq, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            #endregion

            return true;


        }

        public static bool Delete(RequestForQuote p)
        {

            p.UpdatedBy = Environment.UserName;

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
                                                    DELETE FROM [Carry].[RequestForQuote]
                                                    WHERE RequestForQuoteId = @RequestForQuoteId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }
        public static bool Update(RequestForQuote p)
        {

            p.UpdatedBy = Environment.UserName;



            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [Carry].[RequestForQuote]
                                                   SET [CompanyName] = @CompanyName
                                                      ,[Attention] = @Attention
                                                      ,[CompanyAddress] = @CompanyAddress
                                                      ,[PhoneNo] = @PhoneNo
                                                      ,[FaxNo] = @FaxNo
                                                      ,[Email] = @Email
                                                      ,[Comment] = @Comment
                                                      ,[UpdatedBy] = @UpdatedBy
                                                 WHERE RequestForQuoteId = @RequestForQuoteId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }

    }
}