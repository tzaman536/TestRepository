using AmzBL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmzWholeSaleWeb.Controllers
{
    public class ProductDetailController : Controller
    {
        private AmzProductHandler productHandler;

        public ProductDetailController()
        {
            productHandler = new AmzProductHandler();
        }

        // GET: ProductDetail
        public ActionResult Index(int productID)
        {
            var result = productHandler.GetProduct(productID);

            ViewBag.Product = result;
            return View();
        }
    }
}