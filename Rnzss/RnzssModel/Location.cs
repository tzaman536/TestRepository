using Dapper;
using log4net;
using PhenixTools.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace SportsNetworkModel
{
    public class Location: SportsNetwork
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LoctionDetail { get; set; }
        public string AddressLink { get; set; }
        public string AddUserName { get; set; }

        public static IEnumerable<Location> GetMyLocations(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Location>(@"
                    SELECT *
                    from [SportsNetwork].[dbo].[Locations]
                    WHERE AddUserName = @adminUserName
                    order by LocationName", new { adminUserName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Location> GetAllLocations(string adminUserName)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    return conn.Query<Location>(@"
                    SELECT LocationId,LocationName,LoctionDetail, 0 as SortOrder
                    from [SportsNetwork].[dbo].[Locations]
                    WHERE AddUserName = @adminUserName
                    union 
                    SELECT LocationId,LocationName,LoctionDetail, 1 as SortOrder
                    from [SportsNetwork].[dbo].[Locations]
                    WHERE AddUserName not in ( @adminUserName )
                    ORDER BY SortOrder
                    ", new { adminUserName });
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail(string.Format("ERROR From: {0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType), string.Format("{0}", ex.Message), SupportEmail);
                    logger.Fatal(ex);
                }

            }

            return null;

        }



        public static bool Add(Location o)
        {


            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        INSERT INTO [SportsNetwork].[dbo].[Locations]([LocationName]
                                                ,[LoctionDetail]
                                                ,[AddressLink]
                                                ,[AddUserName]
                                                ,[AddUpdateDt])
                                        VALUES (@LocationName
                                                ,@LoctionDetail
                                                ,@AddressLink
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

        public static bool Update(Location o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
            
                    conn.Execute(@"
                        UPDATE [SportsNetwork].[dbo].[Locations]
                        SET [LocationName] = @LocationName
                            ,[LoctionDetail] = @LoctionDetail
                            ,[AddressLink] = @AddressLink
                            ,[AddUserName] = @AddUserName
                            ,[AddUpdateDt] = getutcdate()
                        WHERE LocationId = @LocationId
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

        public static bool Delete(Location o)
        {
            using (var conn = new SqlConnection(DefaultConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                        DELETE [SportsNetwork].[dbo].[Locations]
                        WHERE LocationId = @LocationId
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
