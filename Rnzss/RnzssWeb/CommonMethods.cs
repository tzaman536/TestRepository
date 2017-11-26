using log4net;
using RnzssWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RnzssWeb
{
    public class CommonMethods
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string KW_Unknown = "Unknown";


        public static string _connectionString;
        public static bool IsDebugMode = false;

        static CommonMethods()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
#if DEBUG
            IsDebugMode = true;
#endif


        }
        public static IDbConnection OpenConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(_connectionString))
                    return null;

                if (!string.IsNullOrEmpty(_connectionString))
                {
                    IDbConnection connection =
                                new SqlConnection(_connectionString);
                    connection.Open();
                    return connection;
                }
                else
                {
                    throw new Exception("Invalid Connection string. ");
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
            return null;
        }

        public static List<RnzssDropDownItem> GetRfqStatusList()
        {
            var statusList = Enum.GetNames(typeof(RfqStatusList));
            List<RnzssDropDownItem> result = new List<RnzssDropDownItem>();
            foreach (var s in statusList)
            {
                result.Add(new RnzssDropDownItem() { Name = s });
            }

            return result;
        }
        public static List<RnzssDropDownItem> GetDeliveryInUnitList()
        {
            var list = Enum.GetNames(typeof(DeliveryInUnitList));
            List<RnzssDropDownItem> result = new List<RnzssDropDownItem>();
            foreach (var s in list)
            {
                result.Add(new RnzssDropDownItem() { Name = s });
            }

            return result;
        }
        

    }
}