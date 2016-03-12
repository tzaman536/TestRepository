using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhenixModel.Ref;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using PhenixTools.Mail;
using log4net;

namespace PhenixBL
{
    public class daContactDetail
    {

        public static IEnumerable<ClientContactInfo> GetClientContactDetail()
        {
            IEnumerable<ClientContactInfo> resultList=null;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    resultList = conn.Query<ClientContactInfo>(@"
                    SELECT * 
                    FROM Phenix.dbo.ContactDetail");
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }
                
            }


            return resultList;

        }


        public static void SaveClientContactDetail(ClientContactInfo ci)
        {
            ci.CreatedBy = Environment.UserName;
            ci.CreatedAt = DateTime.UtcNow;
            
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    IEnumerable<ClientContactInfo> resultList = conn.Query<ClientContactInfo>(@"
                        INSERT INTO [Phenix].[dbo].[ContactDetail]
                                    ([CompanyName]
                                    ,[FirstName]
                                    ,[LastName]
                                    ,[Email]
                                    ,[MobileNumber]
                                    ,[OfficeNumber]
                                    ,[Message]
                                    ,[CreatedAt]
                                    ,[CreatedBy])
                                VALUES
                                    (@CompanyName
                                    ,@FirstName
                                    ,@LastName
                                    ,@Email
                                    ,@MobileNumber
                                    ,@OfficeNumber
                                    ,@Message
                                    ,@CreatedAt
                                    ,@CreatedBy)
                    ", new { 
                            ci.CompanyName
                            ,ci.FirstName
                            ,ci.LastName
                            ,ci.Email
                            ,ci.MobileNumber
                            ,ci.OfficeNumber
                            ,ci.Message
                            ,ci.CreatedAt
                            ,ci.CreatedBy
                        }
                     );
                }
                catch (Exception ex)
                {
                    PhenixMail.SendMail("daContactDetail.SaveClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }


            
        }


    }
}
