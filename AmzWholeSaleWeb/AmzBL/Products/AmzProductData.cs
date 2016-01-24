using AmzModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AmzBL.Products
{
    public class AmzProductHandler
    {
        public static IEnumerable<AmzProduct> GetProducts()
        {
            IEnumerable<AmzProduct> result = new List<AmzProduct>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<AmzProduct>(@"
                    SELECT * 
                    FROM phenix.amz_Products");
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }
            return result;

        }

        public static AmzProduct GetProduct(int productId)
        {
            IEnumerable<AmzProduct> result = new List<AmzProduct>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<AmzProduct>(@"
                    SELECT * 
                    FROM phenix.amz_Products
                    where ProductId = @productId
                    ", new { productId });
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }

            if (result.Any())
                return result.ElementAtOrDefault(0);


            return null;

        }

        public static AmzProduct AddProduct(AmzProduct p)
        {

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<int>(@"
                                                        insert into phenix.amz_Products
                                                        (ProductName
                                                        , ProductDescription
                                                        , UnitPrice
                                                        , UnitsInStock
                                                        , UnitsOnOrder
                                                        , Discontinued
                                                        , ImageUploadSuccessful
                                                        , AddDate
                                                        , AddedBy
                                                        )
                                                        values(@ProductName
                                                            , @ProductDescription
                                                            , @UnitPrice
                                                            , @UnitsInStock
                                                            , @UnitsOnOrder
                                                            , @Discontinued
                                                            , @ImageUploadSuccessful
                                                            , getdate()
                                                            , 'tzaman')
                                                        SELECT SCOPE_IDENTITY()

                        ", new { p.ProductName
                                ,p.ProductDescription
                                ,p.UnitPrice
                                ,p.UnitsInStock
                                ,p.UnitsOnOrder
                                ,p.Discontinued
                                ,p.ImageUploadSuccessful
                                ,p.AddDate
                                ,p.AddedBy
                                });


                    if(result.Any())
                    {
                        int insertedProductId = (int)result.ElementAtOrDefault(0);
                        return GetProduct(insertedProductId);
                    }


                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }
            return null;

        }

    }
}
