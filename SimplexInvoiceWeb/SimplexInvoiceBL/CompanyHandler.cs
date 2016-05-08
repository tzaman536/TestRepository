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
    public class LogisticsCompanyHandler
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
	                                            BasePickupCharge,CreatedBy,CreatedAt)
                                           values(@SimplexInvoiceUserId,@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,@ComplimentaryWeight,@WeightRate,
	                                            @BasePickupCharge,@CreatedBy,getdate());
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
                        ,BasePickupCharge = @BasePickupCharge
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

    public class ClientsCompanyHandler
    {
        public int Add(ClientCompany c)
        {


            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<int>(@"
                    insert into invoice.[MyClients](SimplexInvoiceUserId,CompanyId,CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,ComplimentaryWeight,WeightRate,
                                                BillToName,BillToAddressLine1,BillToAddressLine2,BillToCity,BillToState,BillToZip,
	                                            BasePickupCharge,CreatedBy,CreatedAt)
                                           values(@SimplexInvoiceUserId,@CompanyId,@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,@ComplimentaryWeight,@WeightRate,
                                                @BillToName,@BillToAddressLine1,@BillToAddressLine2,@BillToCity,@BillToState,@BillToZip,
	                                            @BasePickupCharge,@CreatedBy,getdate());
                   SELECT SCOPE_IDENTITY()

                   
                                            ", c);

                return result.FirstOrDefault();
            }
        }

        public int Update(ClientCompany c)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                int result = conn.Execute(@"
                    update invoice.[MyClients]
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
                        ,BillToName = @BillToName
                        ,BillToAddressLine1 = @BillToAddressLine1
                        ,BillToAddressLine2 = @BillToAddressLine2
                        ,BillToCity = @BillToCity
                        ,BillToState = @BillToState
                        ,BillToZip = @BillToZip
                        ,ComplimentaryWeight = @ComplimentaryWeight
                        ,BasePickupCharge = @BasePickupCharge
                        ,WeightRate = @WeightRate
                        ,ModifiedBy = @ModifiedBy
                        ,ModifiedAt = getdate()
                    where CompanyId = @CompanyId
                                            ", c);

                return result;
            }
        }


        public int Delete(ClientCompany c)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                int result = conn.Execute(@"
                    delete invoice.[MyClients]
                    where CompanyId = @CompanyId
                                            ", c);

                return result;
            }
        }

        public ClientCompany GetCompanyByName(string companyName)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<ClientCompany>(@"
                                                select *
                                                from [SimplexInvoice].[invoice].[MyClients]
                                                where CompanyName = @companyName
                                            ", new { companyName });

                return result.FirstOrDefault();
            }
        }

        public IEnumerable<ClientCompany> GetClientCompanies(int companyId)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<ClientCompany>(@"
                                                select *
                                                from [SimplexInvoice].[invoice].[MyClients]
                                                where CompanyId = @companyId
                                            ", new { companyId });

                return result;
            }
        }



    }
}
