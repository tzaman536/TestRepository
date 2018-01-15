using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Simplex.Tools.AppSettings
{
    public class AppSettingsHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AppSettingsHandler));



        public static string GetAppSettingsValue(string key)
        {

            //TODO: Add caching here
            
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<string>(@"
                    SELECT AppValue 
                    FROM dbo.AppSettings
                    where AppKey = @key
                      
                    ", new { key });


                    if (result != null && result.Any())
                        return result.FirstOrDefault();

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return null;
        }


    }
}
