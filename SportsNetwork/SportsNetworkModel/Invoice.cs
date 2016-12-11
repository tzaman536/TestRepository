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
        public string Instructions { get; set; }
        public string TypeOfService { get; set; }
        public string Qty { get; set; }
        public string Weight { get; set; }
        public string DeliveryDate { get; set; }
        public string POD { get; set; }
        public string DeliveryAgent { get; set; }
        public string MilageCharge { get; set; }
        public string TollCharge { get; set; }
        public string FuelCharge { get; set; }
        public string MiscServiceCharge { get; set; }
        public string TotalCharge { get; set; }
        public string Comment { get; set; }

    }
}
