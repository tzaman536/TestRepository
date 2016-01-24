using AmzBL.Products;
using AmzModel;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmzWholeSaleWeb.Controllers
{
    public class AmzHomeController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AmzHomeController));
        private AmzProductHandler productHandler;

        public AmzHomeController()
        {
            productHandler = new AmzProductHandler();
        }

        public ActionResult Index()
        {
            return View(productHandler.GetProducts());


    }

    public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(productHandler.GetProducts().ToDataSourceResult(request));
        }


        //private static IEnumerable<AmzProduct> GetProductsLocal()
        //{
        //    List<AmzProduct> result = new List<AmzProduct>();
        //    for (int i = 1; i < 80; i++)
        //    {
        //        result.Add(new AmzProduct
        //        {
        //            ProductID = i,
        //            ProductName = string.Format("Name {0}", i),
        //            UnitPrice = 10,
        //            UnitsInStock = 50,
        //            UnitsOnOrder = 10,
        //            Discontinued = false
        //        });
        //    }

        //    return result;
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}