using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmzWholeSaleWeb.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int productID)
        {
            return View();
        }
    }
}