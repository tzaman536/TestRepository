using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace RnzssWeb.Models
{
    public class Solicitation
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int SolicitationId { get; set; }
        public string SolicitationNo { get; set; }
        public string SolicitationDescription { get; set; }

        public int AwardQuantity { get; set; }

        public decimal AwardAmount { get; set; }

        public DateTime? DueDate { get; set; }


        public string Document { get; set; }
        public string SolicitaionStatus { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(Solicitation p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;



            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                    DELETE FROM [rnz].[DocumentStore] Where LinkId = @SolicitationNo

                                                    DELETE FROM [rnz].[Solicitations]
                                                    WHERE SolicitationId = @SolicitationId
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }
        public static bool Update(Solicitation p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            if (p.DueDate == DateTime.MinValue)
                p.DueDate = null;



            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [rnz].[Solicitations]
                                                   SET [SolicitationNo] = @SolicitationNo
                                                      ,[SolicitationDescription] = @SolicitationDescription
	                                                  ,[AwardQuantity] = @AwardQuantity
	                                                  ,[AwardAmount] = @AwardAmount
	                                                  ,[DueDate] = @DueDate
                                                      ,[SolicitaionStatus] = @SolicitaionStatus
                                                      ,[UpdatedBy] = @UpdatedBy
                                                      ,[UpdateDate] = getutcdate()
                                                 WHERE SolicitationId = @SolicitationId

                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Upsert(Solicitation p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            if(string.IsNullOrEmpty(p.SolicitaionStatus))
            {
                p.SolicitaionStatus = SolicitaionStatusList.Open.ToString();
            }


            var sol = Solicitation.GetSolicitation(p.SolicitationNo);
            if (sol != null)
            {
                p.SolicitationId = sol.SolicitationId;
                return Update(p);

            }

            if (p.DueDate == DateTime.MinValue)
                p.DueDate = null;

            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                        INSERT INTO [rnz].[Solicitations]
                                                               ([SolicitationNo]
                                                               ,[SolicitationDescription]
                                                               ,[AwardQuantity]
	                                                           ,[AwardAmount]
	                                                           ,[DueDate]
                                                               ,SolicitaionStatus
                                                               ,[UpdatedBy])
                                                         VALUES
                                                               (@SolicitationNo
                                                               ,@SolicitationDescription
                                                               ,@AwardQuantity
                                                               ,@AwardAmount
                                                               ,@DueDate
                                                               ,@SolicitaionStatus
                                                               ,@UpdatedBy)
                                                        ", p, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static IEnumerable<Solicitation> GetAll()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<Solicitation>(@"
                                                        select s.*     
                                                               ,case when dc.LinkId is null then '' else 'Solicitation' end as Document
                                                        from [rnz].[Solicitations] s
                                                        left join [rnz].[DocumentStore] dc on s.SolicitationNo = dc.LinkId
                                                       
                                                        ", commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static Solicitation GetSolicitation(string solicitationNo)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<Solicitation>(@"
                                                        select *     
                                                        from [rnz].[Solicitations]
                                                        where solicitationNo = @solicitationNo
                                                        ", new { solicitationNo }, commandTimeout: 0).ToList();
                    if (result != null && result.Any())
                        return result.FirstOrDefault();
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }


        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }
        public static void SynchSolicitation()
        {

            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [rnz].[Solicitations]
                                                    SET [SolicitaionStatus] = @SolicitaionStatusSynch
                                                    WHERE SolicitaionStatus = @SolicitaionStatusOpen

                                                        ", new { SolicitaionStatusSynch = SolicitaionStatusList.Synch.ToString(), SolicitaionStatusOpen = SolicitaionStatusList.Open.ToString() }, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

        }
    }

}