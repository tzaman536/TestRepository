using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace RnzssWeb.Models
{
    public class DocumentStore
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int DocumentStoreId { get; set; }
        public string LinkId { get; set; }
        public string FileBaseName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(DocumentStore p)
        {

            p.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

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
                                                    DELETE FROM [rnz].[DocumentStore]
                                                    WHERE DocumentStoreId = @DocumentStoreId
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
        public static IEnumerable<DocumentStore> GetAllRfqDocuments(string rfqNo)
        {
            string solicitationNumber = CommonMethods.KW_Unknown;

            var rfq = RequestForQuote.GetRfq(rfqNo);
            if (rfq != null && !string.IsNullOrEmpty(rfq.SolicitationNumber))
                solicitationNumber = rfq.SolicitationNumber;

            

            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<DocumentStore>(@"
                                                        select s.DocumentStoreId
                                                               ,s.LinkId
                                                               ,s.FileBaseName    
                                                        from [rnz].[DocumentStore] s
                                                        where LinkId = @rfqNo
                                                        union
                                                        select s.DocumentStoreId
                                                               ,s.LinkId
                                                               ,s.FileBaseName    
                                                        from [rnz].[DocumentStore] s
                                                        where LinkId = @solicitationNumber

                                                        ", new { rfqNo, solicitationNumber }, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static DocumentStore GetSoliciationDocument(string solicitationNo)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<DocumentStore>(@"
                                                        select *     
                                                        from [rnz].[DocumentStore]
                                                        where LinkId = @solicitationNo
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

        public static DocumentStore GetDocument(int DocumentStoreId)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<DocumentStore>(@"
                                                        select *     
                                                        from [rnz].[DocumentStore]
                                                        where DocumentStoreId = @DocumentStoreId
                                                        ", new { DocumentStoreId }, commandTimeout: 0).ToList();
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