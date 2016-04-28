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
            return View();
        }

        public JsonResult GetProductDataJson()
        {
            IEnumerable<Product> data = products.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}