using SimplexInvoiceBL;
using SimplexInvoiceModel;
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

    public class ServiceType
    {
        public string Name { get; set; }
        public int ServiceTypeID { get; set; }
    }
    public class DeliveryAgent
    {
        public string Name { get; set; }
        public int DeliveryAgentID { get; set; }
    }

    public class ReferenceDataController : Controller
    {

        LogisticsCompanyHandler lch = new LogisticsCompanyHandler();
        ClientsCompanyHandler cch = new ClientsCompanyHandler();
        LogisticsCompany lc;

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


        public ActionResult GetSeviceTypes()
        {
            List<ServiceType> result = new List<ServiceType>();

            for (int i = 0; i < 5; i++)
            {
                ServiceType c = new ServiceType() { Name = "Service Type - " + i.ToString(), ServiceTypeID = i };
                result.Add(c);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeliveryAgent()
        {
            List<DeliveryAgent> result = new List<DeliveryAgent>();

            for (int i = 0; i < 5; i++)
            {
                DeliveryAgent c = new DeliveryAgent() { Name = "Delivery Agent - " + i.ToString(), DeliveryAgentID = i };
                result.Add(c);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetClientNames()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            if (lc == null)
                lc = lch.GetCompanyRegisteredByUser(User.Identity.Name);


            var clients =  lch.GetClients(lc);
            List<ClientDDL> result = new List<ClientDDL>();

            foreach(var cl in clients)
            {
                ClientDDL c = new ClientDDL() { ClientName = cl.CompanyName , ClientID = cl.ClientCompanyId };
                result.Add(c);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}