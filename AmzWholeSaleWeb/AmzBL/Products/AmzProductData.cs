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
    public class AmzProductData
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



            //for (int i = 1; i < 80; i++)
            //{
            //    result.Add(new AmzProduct
            //    {
            //        ProductID = i,
            //        ProductName = string.Format("Name {0}", i),
            //        UnitPrice = 10,
            //        UnitsInStock = 50,
            //        UnitsOnOrder = 10,
            //        Discontinued = false
            //    });
            //}

            return result;

        }
    }
}
