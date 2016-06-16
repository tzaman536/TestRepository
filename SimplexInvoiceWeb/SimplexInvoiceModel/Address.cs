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
    public class Address
    {
        public Address() { }
        public Address(string userInput)
        {
             string[] lines = userInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if(lines.Count() == 1)
            {
                lines = userInput.Split('\n');
            }

            if (lines.ElementAt(0) != null)
                Name = lines.ElementAt(0);
            if (lines.ElementAt(1) != null)
                Line1 = lines.ElementAt(1);
            if (lines.ElementAt(2) != null)
            {
                string[] cityStateLine = lines.ElementAt(2).Split(' ');
                if (cityStateLine.ElementAt(0) != null)
                    City = cityStateLine.ElementAt(0);
                if (cityStateLine.ElementAt(1) != null)
                    State = cityStateLine.ElementAt(1);
                if (cityStateLine.ElementAt(2) != null)
                    Zip = cityStateLine.ElementAt(2);
            }

            Description = string.Format("{0} - {1} - {2}",Name,Line1,City );

        }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }

        public static bool Add(Address a)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<int>(@"
                    insert into invoice.Company(CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,ComplimentaryWeight,WeightRate,
	                                            BasePickupCharge,CreatedBy,CreatedAt)
                                           values(@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,@ComplimentaryWeight,@WeightRate,
	                                            @BasePickupCharge,@CreatedBy,getdate());
                   SELECT SCOPE_IDENTITY()

                   
                                            ", a);

              



                return true;
            }
        }


    }
}
