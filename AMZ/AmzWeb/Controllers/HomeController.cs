using Kendo.Mvc.Examples.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;

namespace Kendo.Mvc.Examples.Controllers
{
    public class HomeController : Controller
    {
        private ProductService productService;

        public HomeController()
        {
            productService = new ProductService(new SampleEntities());
        }

        protected override void Dispose(bool disposing)
        {
            productService.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return View(GetProductsLocal());
        }

        public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetProductsLocal().ToDataSourceResult(request));
        }

        private static IEnumerable<ProductViewModel> GetProductsLocal()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            for (int i = 1; i < 15; i++)
            {
                result.Add(new ProductViewModel
                {
                    ProductID = i,
                    ProductName = string.Format("Name {0}", i),
                    UnitPrice = 10,
                    UnitsInStock = 50,
                    UnitsOnOrder = 10,
                    Discontinued = false,
                    LastSupply = DateTime.Today
                });
            }

            return result;
        }

        public JsonResult GetProducts(string text)
        {
            var northwind = new SampleEntities();


            var products = northwind.Products.Select(product => new ProductViewModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice ?? 0,
                UnitsInStock = product.UnitsInStock ?? 0,
                UnitsOnOrder = product.UnitsOnOrder ?? 0,
                Discontinued = product.Discontinued
            });

            if (!string.IsNullOrEmpty(text))
            {
                products = products.Where(p => p.ProductName.Contains(text));
            }

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomers()
        {
            var customers = new SampleEntities().Customers.Select(customer => new CustomerViewModel
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Bool = customer.Bool
            });

            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}
