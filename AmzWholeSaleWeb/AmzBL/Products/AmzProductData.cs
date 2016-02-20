using AmzModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;
using System.Web;

namespace AmzBL.Products
{
    public class AmzProductHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AmzProductHandler));

        public IEnumerable<AmzProduct> GetProducts(string filterText = null)
        {
            IEnumerable<AmzProduct> result = new List<AmzProduct>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<AmzProduct>(@"
                    SELECT * 
                    FROM amz.Products
                    order by ProductID desc");
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }
            if (!string.IsNullOrEmpty(filterText))
            {
                var r = result.Where(x => x.ProductName.ToUpper().Contains(filterText.ToUpper()));
                if (r == null || !r.Any())
                {
                    r = result.Where(x => x.ProductDescription.ToUpper().Contains(filterText.ToUpper()));
                    if(r == null || !r.Any())
                        r = result.Where(x => x.ProductLongDescription.ToUpper().Contains(filterText.ToUpper()));

                    return r;
                }
                else
                    return r;

            }
            else
                return result;

        }


        public IEnumerable<AmzProduct> GetCartProducts(int cartId)
        {
            IEnumerable<AmzProduct> result = new List<AmzProduct>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<AmzProduct>(@"
                        SELECT p.ProductId,p.ProductName,ProductDescription,UnitPrice,SmallImageId,MediumImageId,LargeImageId,sum(Quantity) as TotalItemsInCart, sum(Quantity) * UnitPrice as CostOfItemsInCart 
                        FROM amz.Products p
                        INNER JOIN dbo.CartItems ci on p.ProductId = ci.ProductId
                        WHERE CartId = @cartId
                        group by p.ProductId,p.ProductName,ProductDescription,UnitPrice,SmallImageId,MediumImageId,LargeImageId                        
                    ", new { cartId = cartId}
                    );
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }

            return result;

        }

        public AmzProduct GetProduct(int productId)
        {
            IEnumerable<AmzProduct> result = new List<AmzProduct>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<AmzProduct>(@"
                    SELECT * 
                    FROM amz.Products
                    where ProductId = @productId
                    ", new { productId });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }

            if (result.Any())
                return result.ElementAtOrDefault(0);


            return null;

        }

        public  AmzProduct AddProduct(AmzProduct p)
        {

            logger.InfoFormat("Adding product: {0} - {1}", p.ProductName, p.ProductDescription);
            if (p.ProductName.Length >= 150)
                p.ProductName = p.ProductName.Substring(0, 149);

            if (p.ProductDescription.Length >= 255)
                p.ProductDescription = p.ProductDescription.Substring(0, 254);


            DateTime addDate = DateTime.UtcNow;
            string addedBy = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(addedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                addedBy  = Environment.UserName;
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<int>(@"
                                                        insert into amz.Products
                                                        (ProductName
                                                        , ProductDescription
                                                        , ProductLongDescription
                                                        , UnitPrice
                                                        , UnitsInStock
                                                        , UnitsOnOrder
                                                        , Discontinued
                                                        , ImageUploadSuccessful
                                                        , SmallImageId
                                                        , MediumImageId
                                                        , LargeImageId
                                                        , OriginalImageId
                                                        , ImageIdOne
                                                        , ImageIdTwo
                                                        , ImageIdThree
                                                        , ImageIdFour
                                                        , ImageIdFive
                                                        , AddDate
                                                        , AddedBy
                                                        )
                                                        values(@ProductName
                                                            , @ProductDescription
                                                            , @ProductLongDescription
                                                            , @UnitPrice
                                                            , @UnitsInStock
                                                            , @UnitsOnOrder
                                                            , @Discontinued
                                                            , @ImageUploadSuccessful
                                                            , @SmallImageId
                                                            , @MediumImageId
                                                            , @LargeImageId
                                                            , @OriginalImageId
                                                            , @ImageIdOne
                                                            , @ImageIdTwo
                                                            , @ImageIdThree
                                                            , @ImageIdFour
                                                            , @ImageIdFive
                                                            , getdate()
                                                            , @addedBy
                                                        )
                                                        SELECT SCOPE_IDENTITY()

                        ", new { p.ProductName
                                ,p.ProductDescription
                                ,p.ProductLongDescription
                                ,p.UnitPrice
                                ,p.UnitsInStock
                                ,p.UnitsOnOrder
                                ,p.Discontinued
                                ,p.ImageUploadSuccessful
                                ,p.SmallImageId
                                ,p.MediumImageId
                                ,p.LargeImageId
                                ,p.OriginalImageId
                                ,p.ImageIdOne
                                ,p.ImageIdTwo
                                ,p.ImageIdThree
                                ,p.ImageIdFour
                                ,p.ImageIdFive
                                ,addDate
                                ,addedBy
                                });


                    if(result.Any())
                    {
                        int insertedProductId = (int)result.ElementAtOrDefault(0);
                        return GetProduct(insertedProductId);
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }
            return null;

        }

        public bool UpdateProduct(AmzProduct p)
        {

            bool result = true;
            logger.InfoFormat("Updating product: {0} - {1}", p.ProductName, p.ProductDescription);

            if (p.ProductName.Length >= 150)
                p.ProductName = p.ProductName.Substring(0, 149);

            if (p.ProductDescription.Length >= 255)
                p.ProductDescription = p.ProductDescription.Substring(0, 254);


            DateTime updateDate = DateTime.UtcNow;
            string updatedBy = HttpContext.Current.Request.LogonUserIdentity.Name; 
            if(string.IsNullOrEmpty(updatedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                updatedBy = Environment.UserName;
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                                                        update amz.Products
                                                        set ProductName = @ProductName
                                                            , ProductDescription = @ProductDescription
                                                            , ProductLongDescription = @ProductLongDescription
                                                            , UnitPrice = @UnitPrice
                                                            , UnitsInStock = @UnitsInStock
                                                            , UnitsOnOrder = @UnitsOnOrder
                                                            , Discontinued = @Discontinued
                                                            , ImageUploadSuccessful = @ImageUploadSuccessful
                                                            , ModifiedDate = @updateDate
                                                            , ModifiedBy = @updatedBy
                                                            , ImageIdOne = @ImageIdOne
                                                            , ImageIdTwo = @ImageIdTwo
                                                            , ImageIdThree = @ImageIdThree
                                                            , ImageIdFour = @ImageIdFour
                                                            , ImageIdFive = @ImageIdFive
                                                            , SectionID = @SectionID
                                                        where ProductID = @ProductID

                                                    ",
                                                    new {
                                                        p.ProductName
                                                        ,p.ProductDescription
                                                        ,p.ProductLongDescription
                                                        ,p.UnitPrice
                                                        ,p.UnitsInStock
                                                        ,p.UnitsOnOrder
                                                        ,p.Discontinued
                                                        ,p.ImageUploadSuccessful
                                                        ,updateDate
                                                        ,updatedBy
                                                        ,p.ImageIdOne
                                                        ,p.ImageIdTwo
                                                        ,p.ImageIdThree
                                                        ,p.ImageIdFour
                                                        ,p.ImageIdFive
                                                        ,p.ProductID
                                                        ,p.SectionID
                                                    });


                }
                catch (Exception ex)
                {
                    result = false;
                    logger.Error(ex);
                }

            }
            return result;

        }

        public bool DeleteProduct(AmzProduct p)
        {

            bool result = true;
            logger.InfoFormat("Deleting product: {0} - {1}", p.ProductName, p.ProductDescription);
            DateTime updateDate = DateTime.UtcNow;
            string updatedBy = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(updatedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                updatedBy = Environment.UserName;
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                                                        delete amz.Products
                                                        where ProductID = @ProductID

                                                    ",
                                                    new
                                                    {
                                                        p.ProductID
                                                    });


                }
                catch (Exception ex)
                {
                    result = false;
                    logger.Error(ex);
                }

            }
            return result;

        }

    }
}
