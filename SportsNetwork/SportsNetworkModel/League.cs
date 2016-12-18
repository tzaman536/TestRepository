using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace SportsNetworkModel
{
    public class League : SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueDescription { get; set; }
        public string LeagueTypeId { get; set; }
        public string LeagueLevelId { get; set; }
        public string AddUserName { get; set; }
        public DateTime AddUpdateDt { get; set; }


        public static IEnumerable<League> GetAll()
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<League>(@"
                    SELECT *
                    FROM dbo.Leagues
                    order by LeagueName");
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
