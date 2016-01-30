using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.Tools.Cart
{
    public class CartItems
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
	    public int ProductId { get; set; }
	    public int Quantity {get; set; }
	    public decimal Price { get; set; }

    }
}
