using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceBL
{
    public class InvoiceHelper
    {
        public IEnumerable<string> GetStates()
        {
            return new List<string>() { "New York", "New Jersey" };
        }
    }
}
