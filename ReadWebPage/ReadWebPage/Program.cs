using Dapper;
using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ReadWebPage
{
    class Company
    {
        public int RegisteredCompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime FilingDate { get; set; }
        public string DOSID { get; set; }
        public string CareOf { get; set; }
        public string AddressLine1 { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }


        public bool MailSent { get; set; }
        public DateTime AddDate { get; set; }
        public string AddedBy { get; set; }


    }

    class Program
    {
        static List<Company> companyList = new List<Company>();
        static Dictionary<string, string> dictDOSID = new Dictionary<string, string>();


        public static IEnumerable<Company> GetLoadedCompanies()
        {
            IEnumerable<Company> resultList = null;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    resultList = conn.Query<Company>(@"
                    SELECT * 
                    FROM Phenix.dbo.RegisteredCompanies");
                }
                catch (Exception ex)
                {
                }

            }
            return resultList;
        }

        public static IEnumerable<string> GetDOSID()
        {
            IEnumerable<string> resultList = null;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    resultList = conn.Query<string>(@"
                    SELECT DOSID
                    FROM Phenix.dbo.RegisteredCompanies");
                }
                catch (Exception ex)
                {
                }

            }


            return resultList;

        }


        public static void UpdateCompanyInfo(Company ci)
        {
            if(!string.IsNullOrEmpty(ci.AddressLine1))
                ci.AddressLine1.Replace('\n', ' ');

            if (!string.IsNullOrEmpty(ci.CareOf))
                ci.CareOf.Replace('\n', ' ');
            if (!string.IsNullOrEmpty(ci.Town))
                ci.Town.Replace('\n', ' ');
            if (!string.IsNullOrEmpty(ci.State))
                ci.State.Replace('\n', ' ');
            if (!string.IsNullOrEmpty(ci.Zip))
                ci.Zip.Replace('\n', ' ');

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    IEnumerable<Company> resultList = conn.Query<Company>(@"
                        UPDATE [Phenix].[dbo].[RegisteredCompanies]
                                    SET CareOf = @CareOf
                                        ,AddressLine1 = @AddressLine1
                                        ,Town = @Town
                                        ,State = @State
                                        ,Zip = @Zip
                        WHERE RegisteredCompanyId = @RegisteredCompanyId
                    ", ci);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.ReadLine();
                }

            }

        }

        public static void AddCompanyInfo(Company ci)
        {

            ci.AddedBy = Environment.UserName;
            ci.AddDate = DateTime.UtcNow;
            ci.MailSent = false;
            ci.CompanyAddress.Trim();
            ci.CompanyName.Trim();
            ci.DOSID.Trim();

            if (dictDOSID.ContainsKey(ci.DOSID))
                return;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    IEnumerable<Company> resultList = conn.Query<Company>(@"
                        INSERT INTO [Phenix].[dbo].[RegisteredCompanies]
                                    ([CompanyName]
                                    ,[CompanyAddress]
                                    ,[FilingDate]
                                    ,[DOSID]
                                    ,[MailSent]
                                    ,[AddDate]
                                    ,[AddedBy]
                                    )
                                VALUES
                                    (@CompanyName
                                    ,@CompanyAddress
                                    ,@FilingDate
                                    ,@DOSID
                                    ,@MailSent
                                    ,@AddDate
                                    ,@AddedBy)
                    ", new
                    {
                        ci.CompanyName
                            ,
                        ci.CompanyAddress
                            ,
                        ci.FilingDate
                            ,
                        ci.DOSID
                            ,
                        ci.MailSent
                            ,
                        ci.AddDate
                            ,
                        ci.AddedBy
                    }
                     );
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.ReadLine();
                }

            }



        }



        public static void EnrichCompany(string innterText, ref Company c)
        {
            try
            {
                string[] temp = innterText.Split('\n');
                if (innterText.Contains("Current Entity Name"))
                {
                    foreach (var w in temp)
                    {
                        w.Trim();

                        if (string.IsNullOrEmpty(w))
                            continue;
                        if (w.Contains("Current Entity Name"))
                            continue;

                        c.CompanyName = w.Replace("amp;", "");
                        break;
                    }

                }

                if (innterText.Contains("Initial DOS Filing Date"))
                {
                    DateTime t = new DateTime();
                    foreach (var w in temp)
                    {
                        w.Trim();
                        if (string.IsNullOrEmpty(w))
                            continue;
                        if (w.Contains("Initial DOS Filing Date"))
                            continue;

                        if (DateTime.TryParse(w, out t))
                            c.FilingDate = t;
                        break;
                    }

                }


                if (innterText.Contains("DOS Process"))
                {
                    innterText.Replace("DOS Process", "");
                    innterText.Replace("(Address to which DOS will mail process if accepted on behalf of the entity)", "");
                    c.CompanyName = innterText;
                }

                if (innterText.Contains("DOS ID"))
                {
                    foreach (var w in temp)
                    {
                        w.Trim();

                        if (string.IsNullOrEmpty(w))
                            continue;
                        if (w.Contains("DOS ID"))
                            continue;

                        c.DOSID = w.Replace("amp;", "");
                        break;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        static void ReadCompanyInfo(string href)
        {

            string url = "https://appext20.dos.ny.gov/corp_public/" + href.Replace("amp;", "");
            try
            {
                using (var client = new WebClient())
                {
                    string result = client.DownloadString(url);
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                    // There are various options, set as needed
                    htmlDoc.OptionFixNestedTags = true;

                    // filePath is a path to a file containing the html
                    htmlDoc.LoadHtml(result);

                    // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)



                    // ParseErrors is an ArrayList containing any errors from the Load statement
                    if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
                    {
                        // Handle any parse errors as required

                    }
                    else
                    {

                        if (htmlDoc.DocumentNode != null)
                        {
                            HtmlAgilityPack.HtmlNodeCollection tableNodes = htmlDoc.DocumentNode.SelectNodes("//table");
                            Company c = new Company();

                            foreach (var node in tableNodes)
                            {
                                // Do something with bodyNode
                                if (node != null)
                                {
                                    if (node.InnerText.Contains("Selected Entity Status Information"))
                                    {

                                        foreach (HtmlNode row in node.SelectNodes("tr"))
                                        {
                                            EnrichCompany(row.InnerText, ref c);
                                            //Console.WriteLine("row");
                                            //foreach (HtmlNode cell in row.SelectNodes("th|td"))
                                            //{
                                            //    Console.WriteLine("cell: " + cell.InnerText);
                                            //}
                                        }
                                    }

                                    if (node.InnerText.Contains("Selected Entity Address Information"))
                                    {
                                        foreach (HtmlNode row in node.SelectNodes("tr"))
                                        {
                                            if (row.InnerText.Contains("DOS Process"))
                                                continue;

                                            if (row.InnerText.Contains("Address to which DOS will "))
                                                continue;

                                            if (row.InnerText.Contains("Chief Executive Officer"))
                                                break;

                                            

                                            if(string.IsNullOrEmpty(c.CompanyAddress))
                                            {
                                                c.CompanyAddress = row.InnerText.Replace("amp;", "");
                                            }
                                            if(c.CompanyAddress.Contains("NONE"))
                                            {

                                            }
                                        }
                                    }

                                }
                            }
                            if (!string.IsNullOrEmpty(c.CompanyName))
                            {
                                companyList.Add(c);
                                AddCompanyInfo(c);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }


        }

        static void ProcessPageStartsWith(string companyStartsWith, string pageNumber)
        {
            try
            {
                using (var client = new WebClient())
                {

                    //string result = client.DownloadString("https://appext20.dos.ny.gov/corp_public/CORPSEARCH.SELECT_ENTITY?p_srch_results_page=20&p_entity_name=b&p_name_type=A&p_search_type=BEGINS");
                    string result = client.DownloadString(string.Format("https://appext20.dos.ny.gov/corp_public/CORPSEARCH.SELECT_ENTITY?p_srch_results_page={0}&p_entity_name={1}&p_name_type=A&p_search_type=BEGINS", pageNumber, companyStartsWith));
                    // TODO: do something with the downloaded result from the remote
                    // web site
                    //Console.WriteLine(result);

                    Match m;
                    string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";

                    try
                    {
                        m = Regex.Match(result, HRefPattern,
                                        RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        while (m.Success)
                        {
                            if (m.Groups[1] != null && m.Groups[1].Value.Contains("CORPSEARCH"))
                            {
                                Console.WriteLine("Found href " + m.Groups[1] + " at "
                                   + m.Groups[1].Index);

                                ReadCompanyInfo(m.Groups[1].Value);
                            }
                            m = m.NextMatch();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The matching operation timed out.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        static void ProcessPageContains(string companyContainsWith, string pageNumber)
        {
            try
            {
                using (var client = new WebClient())
                {

                    //string result = client.DownloadString("https://appext20.dos.ny.gov/corp_public/CORPSEARCH.SELECT_ENTITY?p_srch_results_page=20&p_entity_name=b&p_name_type=A&p_search_type=BEGINS");
                    string result = client.DownloadString(string.Format("https://appext20.dos.ny.gov/corp_public/CORPSEARCH.SELECT_ENTITY?p_srch_results_page={0}&p_entity_name={1}&p_name_type=A&p_search_type=CONTAINS", pageNumber, companyContainsWith));
                    // TODO: do something with the downloaded result from the remote
                    // web site
                    //Console.WriteLine(result);

                    Match m;
                    string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";

                    try
                    {
                        m = Regex.Match(result, HRefPattern,
                                        RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        while (m.Success)
                        {
                            if (m.Groups[1] != null && m.Groups[1].Value.Contains("CORPSEARCH"))
                            {
                                Console.WriteLine("Found href " + m.Groups[1] + " at "
                                   + m.Groups[1].Index);

                                ReadCompanyInfo(m.Groups[1].Value);
                            }
                            m = m.NextMatch();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The matching operation timed out.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        static void StartsWithSearch()
        {
            //https://appext20.dos.ny.gov/corp_public/CORPSEARCH.SELECT_ENTITY?p_srch_results_page=20&p_entity_name=b&p_name_type=A&p_search_type=BEGINS

            List<string> startingLetters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            foreach (var l in startingLetters)
            {
                for (int i = 1; i < 21; i++)
                {
                    ProcessPageStartsWith(l, i.ToString());
                }
            }

            var orderByDateList = companyList.OrderByDescending(x => x.FilingDate);
            foreach (var c in orderByDateList)
            {
                Console.WriteLine(string.Format("{0} {1}", c.CompanyName, c.FilingDate));
            }

        }

        static void ContainsSearch()
        {
            List<string> startingLetters = new List<string>() { "MEDI", "PAIN", "DOCT"};

            foreach (var l in startingLetters)
            {
                for (int i = 1; i < 21; i++)
                {
                    ProcessPageContains(l, i.ToString());
                }
            }

            var orderByDateList = companyList.OrderByDescending(x => x.FilingDate);
            foreach (var c in orderByDateList)
            {
                Console.WriteLine(string.Format("{0} {1}", c.CompanyName, c.FilingDate));
            }

        }

        static void PopulateDictDOSID()
        {
            IEnumerable<string> dosids = GetDOSID();
            foreach(string i in dosids)
            {
                if (!dictDOSID.ContainsKey(i))
                    dictDOSID.Add(i, i);
            }
        }


        static void EnrichCompanyAddress(string inputLine, ref Company updateCompany)
        {
            string[] lines = inputLine.Split('\n');
            int i = 0;
            string corpType = string.Empty;
            List<string> addressFragmentList = new List<string>();
            foreach (var l in lines)
            {
                
                if (string.IsNullOrEmpty(l))
                    continue;

                if(l.Trim().ToUpper().Equals("LLC") || l.Trim().ToUpper().Equals("INC"))
                {
                    corpType = l;
                }

                addressFragmentList.Add(l);
            }

            if(string.IsNullOrEmpty(updateCompany.CareOf))
                updateCompany.CareOf = addressFragmentList.ElementAtOrDefault(0);

            updateCompany.AddressLine1 = addressFragmentList.ElementAtOrDefault(1);
            updateCompany.Town = addressFragmentList.ElementAtOrDefault(2);
            if (!string.IsNullOrEmpty(corpType))
            {
                updateCompany.CareOf = string.Format("{0} {1}", updateCompany.CareOf, corpType);
            }

        }

        static void ParseAddress()
        {
            IEnumerable<Company> companies = GetLoadedCompanies();

            foreach(Company c in companies)
            {
                Company updateCompany = c;
                string[] addressFregment = c.CompanyAddress.Split(',');
                if (addressFregment.Length == 5)
                {
                    updateCompany.CareOf = addressFregment.ElementAtOrDefault(0);
                    updateCompany.AddressLine1 = addressFregment.ElementAtOrDefault(1);
                    updateCompany.Town = addressFregment.ElementAtOrDefault(2);
                    updateCompany.State = addressFregment.ElementAtOrDefault(3);
                    updateCompany.Zip = addressFregment.ElementAtOrDefault(4);

                } else  if (addressFregment.Length == 4 || addressFregment.Length == 3)
                {
                    EnrichCompanyAddress(addressFregment[0], ref updateCompany);
                    if(string.IsNullOrEmpty(updateCompany.AddressLine1) && addressFregment.Length == 4)
                    {
                        if(addressFregment.ElementAt(1) != null)
                        {
                            EnrichCompanyAddress(addressFregment.ElementAt(1), ref updateCompany);
                        }
                       
                    }
                }
                else if (addressFregment.Length == 2)
                {
                    continue;
                }
                else if (addressFregment.Length == 1)
                {
                    continue;
                }
                else
                {
                    continue;
                }

                if (string.IsNullOrEmpty(updateCompany.Town))
                {
                    if (addressFregment.Length == 4)
                    {
                        if (addressFregment.ElementAt(1) != null)
                        {
                            updateCompany.Town = addressFregment.ElementAtOrDefault(1);
                        }
                    }
                }

                if (string.IsNullOrEmpty(updateCompany.State))
                {
                    if (addressFregment.Length == 4)
                    {
                        if (addressFregment.ElementAt(2) != null)
                        {
                            updateCompany.State = addressFregment.ElementAt(2).Trim();
                        }
                    }
                    if (addressFregment.Length == 3)
                    {
                        if (addressFregment.ElementAt(1) != null)
                        {
                            updateCompany.State = addressFregment.ElementAt(1).Trim();
                        }
                    }
                }

                if (string.IsNullOrEmpty(updateCompany.Zip))
                {
                        if (addressFregment.Length == 4)
                        {
                            if (addressFregment.ElementAt(3) != null)
                            {
                                updateCompany.Zip = addressFregment.ElementAt(3).Replace("\n", "").Trim();
                            }
                        }
                        if (addressFregment.Length == 3)
                        {
                            if (addressFregment.ElementAt(2) != null)
                            {
                                updateCompany.Zip = addressFregment.ElementAt(2).Replace("\n", "").Trim();
                            }
                        }

                }

                Console.WriteLine("CareOf:{0}\r\nAddress:{1}\r\nTown:{2}\r\nState:{3}\r\nZip:{4}\r\n\r\n",updateCompany.CareOf,updateCompany.AddressLine1,updateCompany.Town, updateCompany.State, updateCompany.Zip);
                UpdateCompanyInfo(updateCompany);
                string h = "hello";

            }
            string hello = "hello";

        }

        static void Main(string[] args)
        {
            //PopulateDictDOSID();
            //ContainsSearch();

            ParseAddress();

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
