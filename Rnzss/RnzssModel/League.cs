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


        public static IEnumerable<League> GetAll(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<League>(@"
                    SELECT *
                    FROM dbo.Leagues
                    WHERE AddUserName = @adminUserName
                    order by LeagueName", new { adminUserName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Player> GetAllPlayersInLeague(string adminUserName, string leagueName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Player>(@"
                    select p.*
                    from Players p
                    inner join LeaguePlayers lp on p.PlayerId = lp.PlayerId
                    inner join Leagues l on lp.LeagueId = l.LeagueId
                    where l.LeagueName = @leagueName
                      and l.AddUserName = @adminUserName
                    order by Name", new { adminUserName, leagueName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Player> GetAllPlayersNotInLeague(string adminUserName, string leagueName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Player>(@"
                    select p.*
                    from Players p
                    left join (select lp.PlayerId
                               from LeaguePlayers lp 
                               inner join Leagues l on lp.LeagueId = l.LeagueId
                               where l.LeagueName = @leagueName
                                 and l.AddUserName = @adminUserName
                              ) x on p.PlayerId = x.PlayerId
                    where x.PlayerId is null
                      and p.AddUserName = @adminUserName
                    order by Name", new { adminUserName, leagueName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static bool AddPlayerToLeague(string adminUserName, string leagueName, int playerId)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        INSERT INTO SportsNetwork.[dbo].LeaguePlayers
                        ([LeagueId],[PlayerId])
                        select LeagueId, @playerId
                        from SportsNetwork.dbo.Leagues
                        where LeagueName =  @leagueName
                          and AddUserName = @adminUserName
                    ", new { adminUserName, leagueName, playerId });
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

        public static bool RemovePlayerFromLeague(string adminUserName, string leagueName, int playerId)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        DELETE lp
                        FROM SportsNetwork.[dbo].LeaguePlayers lp
                        INNER JOIN SportsNetwork.dbo.Leagues l on lp.LeagueId = l.LeagueId
                        INNER JOIN SportsNetwork.dbo.Players p on p.PlayerId = lp.PlayerId
                        where l.LeagueName =  @leagueName
                          and l.AddUserName = @adminUserName
                          and p.PlayerId = @playerId
                          
                    ", new { adminUserName, leagueName, playerId });
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
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
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
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
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
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }



    }
}
