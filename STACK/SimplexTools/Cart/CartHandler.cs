using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Simplex.Tools.Cart
{
    public class CartHandler
    {


        private static readonly ILog logger = LogManager.GetLogger(typeof(CartHandler));


        public bool SetCartProcessed(int cartID,out string message)
        {
            message = "Cart processed";
            bool checkedOut = false;
            DateTime dateCreated = DateTime.Today;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result =  conn.Query<Cart>(@"
                    UPDATE  dbo.Cart
                    SET CheckedOut = 1
                    WHERE CartID = @cartID
                    ", new { cartID });

                }
                catch (Exception ex)
                {

                    logger.Error(ex);
                    return false;
                }
            }

            return true;
        }



        public Cart GetUserCart(out string message)
        {
            message = "Can't identify current user. Please login using your credential.";
            string currentUser = System.Web.HttpContext.Current.Session.SessionID;
            if(string.IsNullOrEmpty(currentUser))
            {
                return null;
            }

            bool checkedOut = false;
            DateTime dateCreated = DateTime.UtcNow;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<Cart>(@"
                    SELECT * 
                    FROM dbo.Cart
                    where UserName = @currentUser
                      and CheckedOut = 0
                    ", new { currentUser });

                    if (result != null &&  result.Any())
                        return result.ElementAtOrDefault(0);

                    result = conn.Query<Cart>(@"
                    INSERT INTO dbo.Cart(UserName,DateCreated,CheckedOut) VALUES (@currentUser,@dateCreated,@checkedOut)
                    

                    SELECT * 
                    FROM dbo.Cart
                    where UserName = @currentUser
                      and CheckedOut = 0
                    ", new { currentUser,dateCreated,checkedOut });

                    if (result != null && result.Any())
                        return result.ElementAtOrDefault(0);


                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return null;
        }

        public bool AddCartItem(int cartID, int productID, int quantity, decimal unitPrice, bool addToExisting = true)
        {
            bool itemAddded = true;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {

                    var totalQty = conn.Query<int>(@"
                        SELECT Quantity FROM dbo.CartItems WHERE CartID = @cartID AND ProductID = @productID ", new { cartID,productID });

                    if (totalQty == null || !totalQty.Any())
                    {
                        var result = conn.Query<Cart>(@"
                        INSERT INTO dbo.CartItems(CartID,ProductID,Quantity,Price) VALUES (@cartID,@productID,@quantity,@unitPrice)
                    ", new { cartID, productID, quantity, unitPrice });
                    }
                    else
                    {
                        int totalItems = totalQty.ElementAtOrDefault(0);
                        if(addToExisting)
                        {
                            totalItems = totalItems + quantity;
                        }
                        else
                        {
                            totalItems = quantity;
                        }
                        
                        var result = conn.Execute(@"
                            UPDATE  dbo.CartItems
                            SET Quantity = @totalItems
                            WHERE CartID = @cartID
                              AND ProductID = @productID
                            ", new { cartID,productID, totalItems });

                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    PhenixMail.SendMail("CartHandler.AddCartItem()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["TechSupportEmail"]);
                    itemAddded = false;
                }
            }
            return itemAddded;
        }


        public bool RemoveCartItem(int cartID, int productID)
        {
            bool itemAddded = true;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<Cart>(@"
                            DELETE dbo.CartItems
                            WHERE CartItemId IN (
					                            SELECT MAX(CartItemId)
					                            FROM dbo.CartItems t
					                            WHERE cartid = @cartID
					                              and ProductId = @productID
					                            )
                    ", new { cartID, productID });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    PhenixMail.SendMail("CartHandler.AddCartItem()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["TechSupportEmail"]);
                    itemAddded = false;
                }
            }
            return itemAddded;
        }


        public IEnumerable<CartItems> GetCartItemsSummary(string sql)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<CartItems>(sql);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return null;
                }
            }
        }


        public IEnumerable<CartItems> GetCartItems(int cartID)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                   return conn.Query<CartItems>(@"
                    SELECT * 
                    FROM dbo.CartItems
                    where CartID = @cartID
                    ", new { cartID });

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return null;
                }
            }
        }

    }
}
