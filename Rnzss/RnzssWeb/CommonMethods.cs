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
        public static string _connectionString;

        static CommonMethods()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

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
            }
            return null;
        }
    }
}