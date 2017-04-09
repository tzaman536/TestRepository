using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsNetworkModel
{
    public class Schedule : SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int ScheduleId { get; set; }
        public int TeamOneId { get; set; }
        public int TeamTwoId { get; set; }
        public DateTime GameTime { get; set; }
        public int LocationId { get; set; }
        public int LeagueId { get; set; }
        public string AddUserName { get; set; }
        public DateTime AddUpdateDt { get; set; }


        public static IEnumerable<Schedule> GetAll(string adminUserName, string leagueName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Schedule>(@"
                    select *
                    from SportsNetwork.dbo.Schedule ", new { adminUserName, leagueName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }


        public static bool Add(Schedule o)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        INSERT INTO [SportsNetwork].[dbo].[Schedules] (
                                                [TeamOneId]
                                                ,[TeamTwoId]
                                                ,[GameTime]
                                                ,[LocationId]
                                                ,[LeagueId]
                                                ,[AddUserName]
                                                ,[AddUpdateDt])
                                        VALUES (@TeamOneId
                                                ,@TeamTwoId
                                                ,@GameTime
                                                ,@LocationId
                                                ,@AddUserName
                                                ,getutcdate())
                    ", o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Update(Schedule o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        UPDATE [SportsNetwork].[dbo].[Schedules]
                        SET [TeamOneId] = @TeamOneId
                            ,[TeamTwoId] = @TeamTwoId
                            ,[GameTime] = @GameTime
                            ,[LocationId] = @LocationId
                            ,[LeagueId] = @LeagueId
                            ,[AddUserName] = @AddUserName
                            ,[AddUpdateDt] = getutcdate()
                        WHERE ScheduleId = @ScheduleId
                    ", o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Delete(Schedule o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        DELETE [SportsNetwork].[dbo].[Schedules]
                        WHERE ScheduleId = @ScheduleId
                    ", o);
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

    }
}
