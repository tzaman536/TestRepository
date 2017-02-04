using Dapper;
using log4net;
using PhenixTools.Mail;
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
        //public string LeagueType { get; set; }
        public string LeagueLevelId { get; set; }
        //public string LeagueLevel { get; set; }
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




        public static bool Add(League o)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        INSERT INTO [dbo].Leagues([LeagueName]
                                                ,[LeagueDescription]
                                                ,[LeagueTypeId]
                                                ,[LeagueLevelId]
                                                ,[AddUserName]
                                                ,[AddUpdateDt])
                                        VALUES (@LeagueName
                                                ,@LeagueDescription
                                                ,@LeagueTypeId
                                                ,@LeagueLevelId
                                                ,@AddUserName
                                                ,getutcdate())
                    ",o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + "ERROR", string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Update(League o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        UPDATE [dbo].Leagues
                        SET [LeagueName] = @LeagueName
                            ,[LeagueDescription] = @LeagueDescription
                            ,[LeagueTypeId] = @LeagueTypeId
                            ,[LeagueLevelId] = @LeagueLevelId
                            ,[AddUserName] = @AddUserName
                            ,[AddUpdateDt] = getutcdate()
                        WHERE LeagueId = @LeagueId
                    ", o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + "ERROR", string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Delete(League o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        DELETE [dbo].Leagues
                        WHERE LeagueId = @LeagueId
                    ", o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString() + "ERROR", string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }



    }
}
