using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class ListViewController : Controller
    {
        private ProductService productService;

        public ListViewController()
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
                    ProductName = string.Format("Name {0}",i),
                    UnitPrice = 10,
                    UnitsInStock = 50,
                    UnitsOnOrder = 10,
                    Discontinued = false,
                    LastSupply = DateTime.Today
                });
            }

            return result;
        }

        private static IEnumerable<ProductViewModel> GetProducts()
        {
            var northwind = new SampleEntities();

            var products = 
            northwind.Products.Select(product => new ProductViewModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice ?? 0,
                UnitsInStock = product.UnitsInStock ?? 0,
                UnitsOnOrder = product.UnitsOnOrder ?? 0,
                Discontinued = product.Discontinued,
                LastSupply = DateTime.Today
            });

            return products;
        }
    }
}