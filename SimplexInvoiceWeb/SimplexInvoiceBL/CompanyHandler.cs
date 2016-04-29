using Dapper;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceBL
{
    public class CompanyHandler
    {
        public int Add(Company c)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<int>(@"
                    insert into invoice.Company(SimplexInvoiceUserId,CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,
	                                            CreatedBy,CreatedAt)
                                           values(@SimplexInvoiceUserId,@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,
	                                            @CreatedBy,getdate());
                   SELECT SCOPE_IDENTITY()

                   
                                            ", c);

                return result.FirstOrDefault();
            }
        }

        public int Update(Company c)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                int result = conn.Execute(@"
                    update invoice.Company
                    set CompanyName = @CompanyName
                        ,ContactPerson = @ContactPerson
                        ,AddressLine1 = @AddressLine1
                        ,AddressLine2 = @AddressLine2
                        ,City = @City
                        ,State = @State
                        ,Zip = @Zip
                        ,Email = @Email
                        ,MobileNumber = @MobileNumber
                        ,OfficeNumber = @OfficeNumber
                        ,FaxNumber = @FaxNumber
                        ,ModifiedBy = @ModifiedBy
                        ,ModifiedAt = getdate()
                    where CompanyId = @CompanyId
                                            ", c);

                return result;       
            }
        }


        public Company GetCompanyRegisteredByUser(string inputUser)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<Company>(@"
                                                select *
                                                from [SimplexInvoice].[invoice].[Company]
                                                where SimplexInvoiceUserId = @inputUser
                                            ", new { inputUser });

                return result.FirstOrDefault();
            }
        }

    }
}
