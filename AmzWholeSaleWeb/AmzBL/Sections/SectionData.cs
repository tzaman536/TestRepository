using AmzModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Web;

namespace AmzBL.Sections
{
    public class SectionDataHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SectionDataHandler));

        public IEnumerable<Section> GetSections(string filterText = null)
        {
            IEnumerable<Section> result = new List<Section>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<Section>(@"
                    --SELECT SectionID , SectionName 
                    SELECT *
                    FROM amz.Sections
                    order by SectionName desc");
                }
                catch (Exception ex)
                {
                    //PhenixMail.SendMail("daContactDetail.GetClientContactDetail()-ERROR", string.Format("{0}", ex.Message), ConfigurationManager.AppSettings["MAIL_SALES_TEAM"]);
                }

            }
            if (!string.IsNullOrEmpty(filterText))
            {
                var r = result.Where(x => x.SectionName.ToUpper().Contains(filterText.ToUpper()));
                if (r == null || !r.Any())
                {
                    r = result.Where(x => x.SectionDescription.ToUpper().Contains(filterText.ToUpper()));
                    if (r == null || !r.Any())
                        r = result.Where(x => x.SectionDescription.ToUpper().Contains(filterText.ToUpper()));

                    return r;
                }
                else
                    return r;

            }
            else
                return result;

        }



        public Section GetSection(int sectionID)
        {
            IEnumerable<Section> result = new List<Section>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    result = conn.Query<Section>(@"
                        SELECT *
                        FROM amz.Sections p
                        WHERE SectionID = @sectionID
                    ", new { sectionID });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }


            if (result != null && result.Any())
                return result.FirstOrDefault();


            return null;

        }

        public Section AddSection(Section c)
        {

            logger.InfoFormat("Adding section: {0} - {1}", c.SectionName, c.SectionDescription);
            if (c.SectionName.Length >= 50)
                c.SectionName = c.SectionName.Substring(0, 49);

            if (c.SectionDescription.Length >= 255)
                c.SectionDescription = c.SectionDescription.Substring(0, 254);


            DateTime addDate = DateTime.UtcNow;
            string addedBy = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(addedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                addedBy = Environment.UserName;
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    var result = conn.Query<int>(@"
                                                        insert into amz.Sections
                                                        (SectionName
                                                        , SectionDescription
                                                        , AddDate
                                                        , AddedBy
                                                        )
                                                        values(@SectionName
                                                            , @SectionDescription
                                                            , @addDate
                                                            , @addedBy
                                                        )
                                                        SELECT SCOPE_IDENTITY()

                        ", new
                    {
                        c.SectionName
                                ,
                        c.SectionDescription
                                ,
                        addDate
                                ,
                        addedBy
                    });


                    if (result.Any())
                    {
                        int insertedSectionId = (int)result.ElementAtOrDefault(0);
                        return GetSection(insertedSectionId);
                    }


                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }
            return null;

        }

        public bool UpdateSection(Section c)
        {

            bool result = true;
            logger.InfoFormat("Updating section: {0} - {1}", c.SectionName, c.SectionDescription);
            DateTime updateDate = DateTime.UtcNow;
            string updatedBy = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(updatedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                updatedBy = Environment.UserName;
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                                                        update amz.Sections
                                                        set SectionName = @SectionName
                                                            , SectionDescription = @SectionDescription
                                                            , ModifiedDate = @updateDate
                                                            , ModifiedBy = @updatedBy
                                                        where SectionID = @SectionID

                                                    ",
                                                    new
                                                    {
                                                        c.SectionName
                                                        ,
                                                        c.SectionDescription
                                                        ,updateDate
                                                        ,updatedBy
                                                        ,c.SectionID
                                                    });


                }
                catch (Exception ex)
                {
                    result = false;
                    logger.Error(ex);
                }

            }
            return result;

        }

        public bool DeleteSection(Section c)
        {

            bool result = true;
            logger.InfoFormat("Deleting section: {0} - {1}", c.SectionName,c.SectionDescription);
            DateTime updateDate = DateTime.UtcNow;
            string updatedBy = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(updatedBy))
            {
                logger.Warn("Couldn't figure out HttpContext user identity. Using Environment.UserName instead");
                updatedBy = Environment.UserName;
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    conn.Execute(@"
                                                        delete amz.Sections
                                                        where SectionID = @SectionID

                                                    ",
                                                    new
                                                    {
                                                        c.SectionID
                                                    });


                }
                catch (Exception ex)
                {
                    result = false;
                    logger.Error(ex);
                }

            }
            return result;

        }

    }
}
