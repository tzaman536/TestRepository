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
        public int Add(LogisticsCompany c)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<int>(@"
                    insert into invoice.Company(SimplexInvoiceUserId,CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,ComplimentaryWeight,WeightRate,
	                                            CreatedBy,CreatedAt)
                                           values(@SimplexInvoiceUserId,@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,@ComplimentaryWeight,@WeightRate,
	                                            @CreatedBy,getdate());
                   SELECT SCOPE_IDENTITY()

                   
                                            ", c);

                return result.FirstOrDefault();
            }
        }

        public int Update(LogisticsCompany c)
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
                        ,ComplimentaryWeight = @ComplimentaryWeight
                        ,WeightRate = @WeightRate
                        ,ModifiedBy = @ModifiedBy
                        ,ModifiedAt = getdate()
                    where CompanyId = @CompanyId
                                            ", c);

                return result;       
            }
        }


        public LogisticsCompany GetCompanyRegisteredByUser(string inputUser)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<LogisticsCompany>(@"
                                                select *
                                                from [SimplexInvoice].[invoice].[Company]
                                                where SimplexInvoiceUserId = @inputUser
                                            ", new { inputUser });

                return result.FirstOrDefault();
            }
        }

    }
}
