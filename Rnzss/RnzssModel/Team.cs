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
    public class Team : SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int TeamId { get; set; }
        public string Name { get; set; }

        public static IEnumerable<Team> GetSinglesTeams(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Team>(@"
                                            select 
	                                            TeamId
	                                            ,p.Name
                                            from SportsNetwork.dbo.Teams t
                                            inner join SportsNetwork.dbo.Players p on t.TeamName = p.Email
                                            where TeamType = 1
                                              and t.AddUserName = @adminUserName
                                                        ", new { adminUserName });
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
