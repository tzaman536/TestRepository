using log4net;
using SimplexInvoiceBL;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        CompanyHandler ch = new CompanyHandler();


        private Product[] products = {
                                         new Product {
                                             ProductID = 1, ProductName = "Audi A3",
                                             Description = "New Audi A3", Category = "Car",
                                             Price = 25000
                                         },
                                         new Product {
                                             ProductID = 2, ProductName = "VW Golf",
                                             Description = "New VW Golf", Category = "Car",
                                             Price = 22000
                                         },
                                         new Product {
                                             ProductID = 3, ProductName = "Boing 747",
                                             Description = "The new Boing airplane", Category = "Airplane",
                                             Price = 2000000
                                         },
                                         new Product {
                                             ProductID = 4, ProductName = "Boing 747",
                                             Description = "The new Boing airplane", Category = "Airplane",
                                             Price = 2000000
                                         },
                                         new Product {
                                             ProductID = 5, ProductName = "Yamaha 250",
                                             Description = "Yamaha's new motorcycle", Category = "Motorcycle",
                                             Price = 5000
                                         },
                                         new Product {
                                             ProductID = 6, ProductName = "honda 750",
                                             Description = "Honda's new motorcycle", Category = "Motorcycle",
                                             Price = 7000
                                         }

                                     };
        // GET: MyClients
        public ActionResult Index()
        {
            logger.Info("Simplex Invoice MyCompanyController.Index()");

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            LogisticsCompany c = (LogisticsCompany)ch.GetCompanyRegisteredByUser(User.Identity.Name);
            if (c == null)
            {
                c = new LogisticsCompany();
                c.CompanyName = string.Empty;
                c.ContactPerson = string.Empty;
                c.AddressLine1 = string.Empty;
                c.AddressLine2 = string.Empty;
                c.City = string.Empty;
                c.State = "Choose one";
                c.Zip = string.Empty;
                c.Email = string.Empty;
                c.MobileNumber = string.Empty;
                c.OfficeNumber = string.Empty;
                c.FaxNumber = string.Empty;
                c.ComplimentaryWeight = 100;
                c.WeightRate = 2;
                c.BasePickupCharge = 25;
            }

            return View(c);
        }

        public JsonResult GetProductDataJson()
        {
            IEnumerable<Product> data = products.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}