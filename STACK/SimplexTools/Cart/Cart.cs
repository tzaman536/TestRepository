using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.Tools.Cart
{
    public class Cart
    {
        public Cart()
        {
            IsValid = true;
        }
        public int CartID { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CheckedOut { get; set; }
        public int TotalItems { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }

        public IEnumerable<CartItems> CartItems
        {
            get
            {
                return cartItems;
            }

            set
            {
                cartItems = value;
                if(value != null)
                    TotalItems = cartItems.Sum(x => x.Quantity);
            }
        }

        private IEnumerable<CartItems> cartItems;
        
    }
}
