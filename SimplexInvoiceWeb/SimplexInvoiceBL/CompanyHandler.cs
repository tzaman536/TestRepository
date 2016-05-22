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
        public int Add(LogisticsCompany c, string currentUser)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<int>(@"
                    insert into invoice.Company(CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,ComplimentaryWeight,WeightRate,
	                                            BasePickupCharge,CreatedBy,CreatedAt)
                                           values(@CompanyName,@ContactPerson,
	                                            @AddressLine1,@AddressLine2,@City,@State,@Zip,@Email,
	                                            @MobileNumber,@OfficeNumber,@FaxNumber,@ComplimentaryWeight,@WeightRate,
	                                            @BasePickupCharge,@CreatedBy,getdate());
                   SELECT SCOPE_IDENTITY()

                   
                                            ", c);

                var result1 = conn.Query<int>(@"
                                            insert into invoice.UserCompany(UserId, CompanyId, CreatedBy, CreatedAt)
                                            values(@CurrentUser, @CompanyId, @EnvUser, getdate());
                                            ", new { CurrentUser = currentUser , CompanyId = result.FirstOrDefault(), EnvUser = Environment.UserName });

                


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
                                                from [invoice].[Company] c
                                                inner join invoice.UserCompany uc on c.CompanyId = uc.CompanyId
                                                where uc.UserId = @inputUser
                                            ", new { inputUser });

                return result.FirstOrDefault();
            }
        }

        public IEnumerable<ClientCompany> GetClients(LogisticsCompany lc)
        {
            if (lc == null)
                return null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<ClientCompany>(@"
                                                select*
                                                from invoice.MyClients
                                                where CompanyId = @companyId
                   
                                            ", new { companyId = lc.CompanyId });

                return result;
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
                    insert into invoice.[MyClients](CompanyId,CompanyName,ContactPerson,
	                                            AddressLine1,AddressLine2,City,State,Zip,Email,
	                                            MobileNumber,OfficeNumber,FaxNumber,ComplimentaryWeight,WeightRate,
                                                BillToName,BillToAddressLine1,BillToAddressLine2,BillToCity,BillToState,BillToZip,
	                                            BasePickupCharge,CreatedBy,CreatedAt)
                                           values(@CompanyId,@CompanyName,@ContactPerson,
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
                    where ClientCompanyId = @ClientCompanyId
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

        public ClientCompany GetCompanyByName(string companyName, LogisticsCompany lc)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                var result = conn.Query<ClientCompany>(@"
                                                select *
                                                from [SimplexInvoice].[invoice].[MyClients]
                                                where CompanyName = @companyName
                                                  and CompanyId = @companyId
                                            ", new { companyName, companyId = lc.CompanyId });

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
