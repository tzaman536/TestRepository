using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RnzssWeb.Models
{
    public class Vendor
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Attention { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }



        public static bool Delete(Vendor v)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                                    DELETE FROM [rnz].[Vendors]
                                                    WHERE VendorId = @VendorId
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

        public static bool Update(Vendor v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

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
                                                UPDATE [rnz].[Vendors]
                                                SET [CompanyName] = @CompanyName
                                                    ,[Attention] = @Attention
                                                    ,[CompanyAddress] = @CompanyAddress
                                                    ,[PhoneNo] = @PhoneNo
                                                    ,[FaxNo] = @FaxNo
                                                    ,[Email] = @Email
                                                    ,[UpdatedBy] = @UpdatedBy
                                                      ,[UpdateDate] = getutcdate()
                                                 WHERE VendorId = @VendorId
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

        public static bool Add(ref Vendor v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;


            #region Add RFQ
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Execute(@"
                                        INSERT INTO [rnz].[Vendors]
                                               ([CompanyName]
                                               ,[Attention]
                                               ,[CompanyAddress]
                                               ,[PhoneNo]
                                               ,[FaxNo]
                                               ,[Email]
                                               ,[UpdatedBy]
                                               )
                                         VALUES
                                               (@CompanyName
                                               ,@Attention
                                               ,@CompanyAddress
                                               ,@PhoneNo
                                               ,@FaxNo
                                               ,@Email
                                               ,@UpdatedBy
                                               )
                                                        ", v, commandTimeout: 0);
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

        public static bool Upsert(Vendor v)
        {

            v.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            var existingVendor = VendorExists(v.CompanyName);
            if (existingVendor != null)
            {
                v.VendorId = existingVendor.VendorId;
                return Update(v);
            }


            Add(ref v);

            return true;

        }

        public static Vendor VendorExists(string companyName)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<Vendor>(@"
                                                        select *     
                                                        from [rnz].[Vendors]
                                                        where CompanyName = @companyName
                                                        ", new { companyName }, commandTimeout: 0).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Vendor> GetAll()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<Vendor>(@"
                                                        select *     
                                                        from [rnz].[Vendors]
                                                        ", commandTimeout: 0);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }

        public static IEnumerable<Vendor> GetVendorLike(string matchString, bool applySmartMatch = true)
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    var result = connection.Query<Vendor>(string.Format(@"
                                                        select *     
                                                        from [rnz].[Vendors]
                                                        where CompanyName like '%{0}%'
                                                        ", matchString), commandTimeout: 0);

                    if (applySmartMatch)
                    {
                        if (result != null && result.Count() > 1)
                        {
                            var r1 = result.Where(x => x.CompanyName.ToLower().StartsWith(matchString.ToLower()));
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
    }
}