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
    public interface ICompany
    {

    }
    public class Company: ICompany
    {
        
        public int CompanyId { get; set; }
        public string SimplexInvoiceUserId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string FaxNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }


    public class LogisticsCompany : Company
    {
        public int ComplimentaryWeight { get; set; }
        public int WeightRate { get; set; }
        public decimal BasePickupCharge { get; set; }

    }

}
