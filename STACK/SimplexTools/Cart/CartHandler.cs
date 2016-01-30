using Dapper;
using log4net;
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


        public Cart GetUserCart(out string message)
        {
            message = "Can't identify current user. Please login using your credential.";
            string currentUser = HttpContext.Current.Request.LogonUserIdentity.Name;
            if(string.IsNullOrEmpty(currentUser))
            {
                return null;
            }

            bool checkedOut = false;
            DateTime dateCreated = DateTime.Today;
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
