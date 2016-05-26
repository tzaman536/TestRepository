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
    public class JobTicket: ClientCompany
    {
        public int JobTicketId { get; set; }
        public string ClientName { get; set; }
        public int ClientCompanyId { get; set; }
        public DateTime JobDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Milage { get; set; }
        public decimal Toll { get; set; }
        public decimal FuelSurcharge { get; set; }
        public decimal MiscFee { get; set; }
        public decimal TotalCharge { get; set; }
        public string WaitTime { get; set; }
        public string PickupFrom { get; set; }
        public string DeliverTo { get; set; }
        public string Instruction { get; set; }
        public string ServiceType { get; set; }
        public string DeliveryAgent { get; set; }
        public string POD { get; set; }
        public string Comments { get; set; }

    }
}
