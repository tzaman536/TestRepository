using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex.Tools.Cart
{
    public class CartView
    {
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CheckedOut { get; set; }
    }
}
