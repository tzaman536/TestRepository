using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsNetworkModel
{
    public class LeagueTypes: SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int LeagueTypeId { get; set; }
        public string LeagueType { get; set; }

        public static IEnumerable<LeagueTypes> GetAll()
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<LeagueTypes>(@"
                    SELECT *
                    FROM dbo.LeagueTypes
                    order by LeagueType");
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["SupportEmailAddress"]);
                    logger.Fatal(ex);
                }

            }

            return null;

        }
    }



}
