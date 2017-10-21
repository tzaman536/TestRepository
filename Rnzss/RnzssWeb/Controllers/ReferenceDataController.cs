using SimplexInvoiceBL;
using SimplexInvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{

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

    public class State
    {
        public string StateId { get; set; }
        public string StateCode { get; set; }
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
            List<PickupDeliveryAddress> result = PickupDeliveryAddress.GetPickupAndDeliveryLocations().ToList();

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

        public ActionResult GetStates()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<string> states = SimplexInvoiceHelper.GetStates();

            List<State> result = new List<State>();

            foreach (var st in states)
            {
                State c = new State() { StateId = st, StateCode = st };
                result.Add(c);
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}