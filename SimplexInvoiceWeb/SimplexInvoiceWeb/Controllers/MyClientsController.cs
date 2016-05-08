using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using SimplexInvoiceBL;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SimplexInvoiceWeb.Controllers
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    public class MyClientsController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MyCompanyController));


        SimplexInvoiceHelper helper = new SimplexInvoiceHelper();
        LogisticsCompanyHandler ch = new LogisticsCompanyHandler();
        ClientsCompanyHandler clientCompanyHandler = new ClientsCompanyHandler();
        LogisticsCompany lc;



        // GET: MyClients
        public ActionResult Index()
        {
            logger.Info("Simplex Invoice MyClients. Index()");

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ClientCompany c = null;

            if (c == null)
            {
                c = new ClientCompany();
                c.CompanyName = string.Empty;
                c.ContactPerson = string.Empty;
                c.AddressLine1 = string.Empty;
                c.AddressLine2 = string.Empty;
                c.City = string.Empty;
                c.State = "NY";
                c.Zip = string.Empty;
                c.Email = string.Empty;
                c.MobileNumber = string.Empty;
                c.OfficeNumber = string.Empty;
                c.FaxNumber = string.Empty;
            }

            lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);
            if(lc != null)
            {
                c.WeightRate = lc.WeightRate;
                c.ComplimentaryWeight = lc.ComplimentaryWeight;
                c.BasePickupCharge = lc.BasePickupCharge;
            }


            ViewData["PU_FORM"] = c;

            return View();
        }

        [HttpPost]
        public ActionResult SaveClientInfo([DataSourceRequest]DataSourceRequest request, string clientCompanyString)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            string message = "Company saved.";
            var json_serializer = new JavaScriptSerializer();
           
            ClientCompany c = json_serializer.Deserialize<ClientCompany>(clientCompanyString);
            c.SimplexInvoiceUserId = User.Identity.Name;
            c.CreatedBy = User.Identity.Name;
            logger.InfoFormat("Saving company...");
            if (lc == null)
                lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);

            try
            {
                var existingCompany = clientCompanyHandler.GetCompanyByName(c.CompanyName);
                if (existingCompany == null)
                {
                    c.CompanyId = lc.CompanyId;
                    c.ClientCompanyId = clientCompanyHandler.Add(c);
                }
                else
                {
                    c.ClientCompanyId = existingCompany.CompanyId;
                    logger.InfoFormat("Company exists. Updating client company.");
                    c.ModifiedBy = User.Identity.Name;
                    logger.InfoFormat("{0} rows updated.", clientCompanyHandler.Update(c));
                }

                logger.InfoFormat("Company saved.");

            }
            catch (Exception ex)
            {
                message = string.Format("Failed to save company. Error: {0}", ex.Message);
                logger.Fatal(ex);
            }


            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (lc == null)
                lc = ch.GetCompanyRegisteredByUser(User.Identity.Name);



            IEnumerable<ClientCompany> result = clientCompanyHandler.GetClientCompanies(lc.CompanyId);

            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, ClientCompany company)
        {

            //var addedProduct = productHandler.AddProduct(product);
            //if (addedProduct != null)
            //{
            //    ViewBag.Message = "Record saved";
            //    ViewBag.ErrorFound = false;

            //    product.ProductID = addedProduct.ProductID;
            //}
            //else
            //{
            //    ViewBag.Message = "Failed to save record";
            //    ViewBag.ErrorFound = true;

            //}


            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, ClientCompany company)
        {
            //productHandler.UpdateProduct(product);

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, ClientCompany company)
        {
            string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");

            try
            {
                //string fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.OriginalImageId);
                //FileInfo fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.SmallImageId);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.MediumImageId);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.LargeImageId);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdOne);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }

                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdTwo);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdThree);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFour);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}
                //fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFive);
                //fi = new FileInfo(fileToDelete);
                //if (fi.Exists)
                //{
                //    try
                //    {
                //        fi.Delete();
                //    }
                //    catch { }
                //}



            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }


            //productHandler.DeleteProduct(product);

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }

    }
}