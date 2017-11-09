using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SportsNetworkWeb.Models
{
    public class ProductInformation
    {
        public int ProductInformationId { get; set; }
        public string RFQNo { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string Quantity { get; set; }
        public string CN { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(ProductInformation p)
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
                                                    DELETE FROM [rnz].[ProductInformation]
                                                    WHERE ProductInformationId = @ProductInformationId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }
        public static bool Update(ProductInformation p)
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
                                                UPDATE [rnz].[ProductInformation]
                                                   SET [PartName] = @PartName
                                                      ,[PartNumber] = @PartNumber
                                                      ,[PartDescription] = @PartDescription
                                                      ,[Quantity] = @Quantity
                                                      ,[CN] = @CN
                                                      ,[UpdatedBy] = @UpdatedBy
                                                      
                                                 WHERE ProductInformationId = @ProductInformationId

                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;

        }

        public static bool Upsert(ProductInformation p)
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
                                                        INSERT INTO [rnz].[ProductInformation]
                                                                   ([RFQNo]
                                                                   ,[PartName]
                                                                   ,[PartNumber]
                                                                   ,[PartDescription]
                                                                   ,[Quantity]
                                                                   ,[CN]
                                                                   ,[UpdatedBy]
                                                                   )
                                                             VALUES
                                                                   (@RFQNo
                                                                   ,@PartName
                                                                   ,@PartNumber
                                                                   ,@PartDescription
                                                                   ,@Quantity
                                                                   ,@CN
                                                                   ,@UpdatedBy
                                                                  )
                                               

                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
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

                }

            }

            return null;

        }
    }
}