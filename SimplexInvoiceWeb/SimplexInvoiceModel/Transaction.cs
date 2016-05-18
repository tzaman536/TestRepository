using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceModel

{
    public class Transaction: ClientCompany
    {
        public int TransactionId { get; set; }
        public string PickupFromCompanyName { get; set; }
        public string PickupFromPersonName { get; set; }
        public string PickupFromAddressLine1 { get; set; }
        public string PickupFromAddressLine2 { get; set; }
        public string PickupFromCity { get; set; }
        public string PickupFromState { get; set; }
        public string PickupFromZip { get; set; }
        public string PickupFromPhone { get; set; }

        public string DeliverToCompanyName { get; set; }
        public string DeliverToPersonName { get; set; }
        public string DeliverToAddressLine1 { get; set; }
        public string DeliverToAddressLine2 { get; set; }
        public string DeliverToCity { get; set; }
        public string DeliverToState { get; set; }
        public string DeliverToZip { get; set; }
        public string DeliverToPhone { get; set; }
        public int Qty { get; set; }
        public decimal Weight { get; set; }

        public string TypeOfService { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string POD { get; set; }
        public string DeliveryAgent { get; set; }

        public string Comments { get; set; }

    }
}
