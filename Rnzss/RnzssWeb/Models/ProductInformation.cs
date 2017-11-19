using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RnzssWeb.Models
{
    public class ProductInformation
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int ProductInformationId { get; set; }
        public string RFQNo { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string Quantity { get; set; }
        public string CN { get; set; }
        public decimal VendorPrice { get; set; }
        public decimal PkgCost { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(ProductInformation p)
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
                                                    DELETE FROM [rnz].[ProductInformation]
                                                    WHERE ProductInformationId = @ProductInformationId
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
        public static bool Update(ProductInformation p)
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
                                                UPDATE [rnz].[ProductInformation]
                                                   SET [PartName] = @PartName
                                                      ,[PartNumber] = @PartNumber
                                                      ,[PartDescription] = @PartDescription
                                                      ,[Quantity] = @Quantity
                                                      ,[CN] = @CN
                                                      ,VendorPrice = @VendorPrice
                                                      ,PkgCost = @PkgCost
                                                      ,[UpdatedBy] = @UpdatedBy
                                                      ,[UpdateDate] = getutcdate()
                                                 WHERE ProductInformationId = @ProductInformationId

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

        public static bool Upsert(ProductInformation p)
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
                                                        INSERT INTO [rnz].[ProductInformation]
                                                                   ([RFQNo]
                                                                   ,[PartName]
                                                                   ,[PartNumber]
                                                                   ,[PartDescription]
                                                                   ,[Quantity]
                                                                   ,[CN]
                                                                   ,VendorPrice 
                                                                   ,PkgCost
                                                                   ,[UpdatedBy]
                                                                   )
                                                             VALUES
                                                                   (@RFQNo
                                                                   ,@PartName
                                                                   ,@PartNumber
                                                                   ,@PartDescription
                                                                   ,@Quantity
                                                                   ,@CN
                                                                   ,@VendorPrice
                                                                   ,@PkgCost
                                                                   ,@UpdatedBy
                                                                  )
                                               

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

        public static IEnumerable<ProductInformation> GetAll(string rfqNo)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<ProductInformation>(@"
                                                        select *     
                                                        from [rnz].[ProductInformation]
                                                        where RFQNo = @rfqNo
                                                        ", new { rfqNo }, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }
    }
}