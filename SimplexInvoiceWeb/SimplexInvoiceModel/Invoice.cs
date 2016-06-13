using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceModel
{
    public class Invoice
    {
        public string MyCompanyAddress { get; set; }
        public string MyCompanyContactInfo { get; set; }
        public string JobDate { get; set; }
        public string JobNumber { get; set; }
        public string BillTo { get; set; }
        public string PUFromAddress { get; set; }
        public string PUFromContactInfo { get; set; }
        public string DeliverToAddress { get; set; }
        public string DeliverToContactInfo { get; set; }
        public string InvoiceLeftPane { get; set; }
        public string InvoiceRightPane { get; set; }
    }
}
