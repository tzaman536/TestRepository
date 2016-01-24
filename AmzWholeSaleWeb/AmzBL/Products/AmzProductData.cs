using AmzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmzBL.Products
{
    public class AmzProductData
    {
        public static IEnumerable<AmzProduct> GetProducts()
        {
            List<AmzProduct> result = new List<AmzProduct>();
            for (int i = 1; i < 80; i++)
            {
                result.Add(new AmzProduct
                {
                    ProductID = i,
                    ProductName = string.Format("Name {0}", i),
                    UnitPrice = 10,
                    UnitsInStock = 50,
                    UnitsOnOrder = 10,
                    Discontinued = false
                });
            }

            return result;

        }
    }
}
