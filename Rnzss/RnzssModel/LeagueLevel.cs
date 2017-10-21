using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SportsNetworkModel
{
    public class LeagueLevels: SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int LeagueLevelId { get; set; }
        public string LeagueLevel { get; set; }

        public static IEnumerable<LeagueLevels> GetAll()
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<LeagueLevels>(@"
                    SELECT *
                    FROM dbo.LeagueLevels
                    order by LeagueLevel");
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

    }
}
