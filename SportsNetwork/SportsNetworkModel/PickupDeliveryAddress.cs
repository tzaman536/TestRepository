using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceModel
{
    public class PickupDeliveryAddress
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PickupDeliveryAddress));

        public PickupDeliveryAddress() { }
        public PickupDeliveryAddress(string userInput)
        {
             string[] lines = userInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if(lines.Count() == 1)
            {
                lines = userInput.Split('\n');
            }

            if (lines.ElementAtOrDefault(0) != null)
                Name = lines.ElementAt(0);
            if (lines.ElementAtOrDefault(1) != null)
                Line1 = lines.ElementAt(1);
            if (lines.ElementAtOrDefault(2) != null)
            {
                string[] cityStateLine = lines.ElementAt(2).Split(' ');
                if (cityStateLine.ElementAtOrDefault(0) != null)
                    City = cityStateLine.ElementAt(0);
                if (cityStateLine.ElementAtOrDefault(1) != null)
                    State = cityStateLine.ElementAt(1);
                if (cityStateLine.ElementAtOrDefault(2) != null)
                    Zip = cityStateLine.ElementAt(2);
            }

            Location = string.Format("{0} - {1} - {2}",Name,Line1,City );

        }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static bool Add(PickupDeliveryAddress a)
        {
            try {
                if (!(GetPickupAndDeliveryLocation(a.Location) == null))
                    return true;

                a.ModifiedAt = a.CreatedAt = DateTime.UtcNow;
                a.ModifiedBy = a.CreatedBy = Environment.UserName;



                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    var result = conn.Query<int>(@"
                    insert into invoice.PickupDeliveryAddress(Location,Name,Line1,City,State,Zip,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt)
                                           values(@Location,@Name,@Line1,@City,@State,@Zip,@CreatedBy,@CreatedAt,@ModifiedBy,@ModifiedAt);
                    SELECT SCOPE_IDENTITY()", a);
                    return true;
                }
            }
            catch(Exception ex)
            {
                logger.Fatal(ex);
                return false;
            }
        }

        public static int Update(PickupDeliveryAddress a)
        {
            try
            {
                a.ModifiedAt =  DateTime.UtcNow;
                a.ModifiedBy =  Environment.UserName;

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    var result = conn.Query<int>(@"
                                        update invoice.PickupDeliveryAddress
                                        set Location = @Location
                                            ,Name = @Name
                                            ,Line1 = @Line1
                                            ,City = @City
                                            ,State =  @State
                                            ,Zip = @Zip
                                            ,ModifiedBy = @ModifiedBy
                                            ,ModifiedAt = @ModifiedAt
                                        where PickupDeliveryAddressId = @PickupDeliveryAddressId
                                            ", a);

                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return 0;
            }
        }


        public static  IEnumerable<PickupDeliveryAddress> GetPickupAndDeliveryLocations()
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    return conn.Query<PickupDeliveryAddress>(@"
                                                    select distinct Location,LocationID
                                                    from invoice.PickupDeliveryAddress
                                            ");

                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }

        public static IEnumerable<PickupDeliveryAddress> GetPickupAndDeliveryLocation(string location)
        {
           
            try
            {
                if (string.IsNullOrEmpty(location))
                    return null;
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    return conn.Query<PickupDeliveryAddress>(@"
                                                    select *
                                                    from invoice.PickupDeliveryAddress
                                                    where [Location] = @location
                                            ", location);

                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return null;
            }
        }

        

    }
}
