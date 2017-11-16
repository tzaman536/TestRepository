using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(Solicitation p)
        {

            p.UpdatedBy = Environment.UserName;

            // TODO: Check if this RFQNo and part number exists alreayd then call update and return from here
            //if (ProductExists(p.RFQNo,p.PartNumber))
            //{
            //    return true;
            //}


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                    DELETE FROM [rnz].[DocumentStore] Where SolicitationNo = @SolicitationNo

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

            p.UpdatedBy = Environment.UserName;
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

            p.UpdatedBy = Environment.UserName;

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
                                                               ,[UpdatedBy])
                                                         VALUES
                                                               (@SolicitationNo
                                                               ,@SolicitationDescription
                                                               ,@AwardQuantity
                                                               ,@AwardAmount
                                                               ,@DueDate
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
    }

}