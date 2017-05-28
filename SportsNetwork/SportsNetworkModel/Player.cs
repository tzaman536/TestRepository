using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace SportsNetworkModel
{
    public class Player : SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddUserName { get; set; }
        public DateTime AddUpdateDt { get; set; }

        public static IEnumerable<Player> GetAll(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Player>(@"
                    SELECT *
                    FROM dbo.Players
                    WHERE AddUserName = @adminUserName
                    order by Name", new { adminUserName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Player> GetPlayWithAnyonePlayers(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Player>(@"
                    SELECT *
                    FROM dbo.Players
                    WHERE coalesce(PlayWithAnyone,0) = 1
                      and Email not in (@adminUserName)
                    order by Name", new { adminUserName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }



        public static bool Add(Player o)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        INSERT INTO [dbo].Players([Name]
                                                ,[Email]
                                                ,[Phone]
                                                ,[AddUserName]
                                                ,[AddUpdateDt])
                                        VALUES (@Name
                                                ,@Email
                                                ,@Phone
                                                ,@AddUserName
                                                ,getutcdate())

                        INSERT INTO [SportsNetwork].[dbo].[Teams] (TeamName,TeamDetail,TeamType,AddUserName,AddUpdateDt)
                        VALUES (@Email,@Email,1,@AddUserName,getutcdate())
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

        public static bool Update(Player o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    var playerBeforeUpdate = conn.Query<Player>(@"
                    SELECT *
                    FROM dbo.Players
                    WHERE PlayerId = @PlayerId
                    ", o).FirstOrDefault();

                    conn.Execute(@"
                        UPDATE [dbo].Players
                        SET [Name] = @Name
                            ,[Email] = @Email
                            ,[Phone] = @Phone
                            ,[AddUserName] = @AddUserName
                            ,[AddUpdateDt] = getutcdate()
                        WHERE PlayerId = @PlayerId
                    ", o);

                    if(playerBeforeUpdate != null && !playerBeforeUpdate.Email.Equals(o.Email))
                    {
                        conn.Execute(@"
                        UPDATE [SportsNetwork].[dbo].[Teams]
                        SET [TeamName] = @NewEmail
                        WHERE TeamName = @OldEmail
                          AND TeamType = 1
                    ", new { OldEmail = playerBeforeUpdate.Email, NewEmail = o.Email  });

                    }

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

        public static bool Delete(Player o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                    
                        DELETE [SportsNetwork].[dbo].[Teams]
                        WHERE TeamName = @Email 
                        AND TeamType = 1

                        DELETE [dbo].Players
                        WHERE PlayerId = @PlayerId
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
