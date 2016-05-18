using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{
    public class PickupAndDelivery
    {
        public string Location { get; set; }
        public int LocationID { get; set; }
    }

    public class ReferenceDataController : Controller
    {
        // GET: ReferenceData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPickupAndDeliveryLocation()
        {
            List<PickupAndDelivery> result = new List<PickupAndDelivery>();

            for (int i = 0; i < 5; i++)
            {
                PickupAndDelivery c = new PickupAndDelivery() { Location = "Location - " + i.ToString(), LocationID = i };
                result.Add(c);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}