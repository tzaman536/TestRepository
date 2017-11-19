using log4net;
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
    }
}