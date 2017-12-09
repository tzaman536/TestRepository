using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace RnzssWeb.Models
{
    public class InputHtml
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int InputHtmlId { get; set; }
        public string Source { get; set; }
        public string HtmlText { get; set; }
        public bool ParseStatus { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        

        public static bool Delete(InputHtml v)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                    DELETE FROM [rnz].[InputHtml]
                                                    WHERE InputHtmlId = @InputHtmlId
                                                        ", v, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }
            }
            return true;
        }

        public static bool Update(InputHtml v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [rnz].[InputHtml]
                                                SET 
                                                    Source = @Source
                                                    ,HtmlText = @HtmlText
                                                    ,[UpdatedBy] = @UpdatedBy
                                                    ,[UpdateDate] = getutcdate()
                                                 WHERE InputHtmlId = @InputHtmlId
                                                        ", v, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool SetParseStatus(InputHtml v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;


            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                UPDATE [rnz].[InputHtml]
                                                SET 
                                                    ParseStatus = @ParseStatus
                                                    ,[UpdatedBy] = @UpdatedBy
                                                    ,[UpdateDate] = getutcdate()
                                                 WHERE InputHtmlId = @InputHtmlId
                                                        ", v, commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }

            }

            return true;

        }

        public static bool Add(ref InputHtml v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;


            #region Add RFQ
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    v.InputHtmlId = connection.Query<int>(@"
                                        INSERT INTO [rnz].[InputHtml]
                                               ([Source]
                                               ,[HtmlText]
                                               ,[UpdatedBy]
                                               )
                                         VALUES
                                               (@Source
                                               ,@HtmlText
                                               ,@UpdatedBy
                                               );
                                           SELECT CAST(SCOPE_IDENTITY() as int)
                                                        ", v, commandTimeout: 0).Single();

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                    return false;
                }
            }
            #endregion

            return true;


        }


        public static IEnumerable<InputHtml> GetAll()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<InputHtml>(@"
                                                        select *     
                                                        from [rnz].[InputHtml]
                                                        ", commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<InputHtml> GetInputHtmlLike(string matchString, bool applySmartMatch = true)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<InputHtml>(string.Format(@"
                                                        select *     
                                                        from [rnz].[InputHtml]
                                                        where HtmlText like '%{0}%'
                                                        ", matchString), commandTimeout: 0);

                    if (applySmartMatch)
                    {
                        if (result != null && result.Count() > 1)
                        {
                            var r1 = result.Where(x => x.HtmlText.ToLower().StartsWith(matchString.ToLower()));
                            if (r1 != null && r1.Any())
                                return r1;
                        }
                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }


        public static bool Parse(ref RequestForQuote rfq, string inputAddress)
        {

            inputAddress = inputAddress.Replace("Associated CAGE Code:", "").Replace("Replacement CAGE Code:", "");
            int matchCount = 0;

            //var temp = inputAddress.Split("Phone");
            Match match = Regex.Match(inputAddress, @"^(.*?)Phone:", RegexOptions.None);
            if (match.Success)
            {
                string companyName = Regex.Match(match.Value, @"^[^0-9]*").Value;
                rfq.CompanyAddress = match.Value.Replace("Phone:", "");
                if (!string.IsNullOrEmpty(companyName))
                {
                    rfq.CompanyAddress = rfq.CompanyAddress.Replace(companyName, "");
                    rfq.CompanyName = companyName;

                }

                matchCount++;
            }

            match = Regex.Match(inputAddress, @"(?<=Phone:)(.*)(?=Fax:)", RegexOptions.None);
            if (match.Success)
            {
                rfq.PhoneNo = match.Value.Trim();
                matchCount++;
            }

            match = Regex.Match(inputAddress, @"(?<=Fax:)(.*)(?=CAGE Code:)", RegexOptions.None);
            if (match.Success)
            {
                if (match.Value.Trim().Contains("http"))
                {
                    rfq.FaxNo = Regex.Match(match.Value, @"^[^a-z]*").Value;
                }
                else
                {
                    rfq.FaxNo = match.Value.Trim();
                }
                matchCount++;
            }

            match = Regex.Match(inputAddress, @"(?<=Government POC Email:)(.*)(?=Size:)", RegexOptions.None);
            if (match.Success)
            {
                rfq.Email = match.Value.Trim();
                matchCount++;
            }

            match = Regex.Match(inputAddress, @"(?<=Government POC:)(.*)(?=SIC:)", RegexOptions.None);
            if (match.Success)
            {
                rfq.Attention = match.Value.Trim();
                matchCount++;
            }


            if (matchCount == 5)
                return true;
            else
                return false;

        }

    }
}