using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using PhenixModel.Ref;
using Kendo.Mvc.UI;
using System.Web.Script.Serialization;

namespace Simplex.Controllers
{
    public class CustomSoftwareController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.Message = "Leave us a message.";
            ClientContactInfo p = new ClientContactInfo();
            p.CompanyName = string.Empty;
            p.FirstName = string.Empty;
            p.LastName = string.Empty;
            p.Email = string.Empty;
            p.MobileNumber = string.Empty;
            p.OfficeNumber = string.Empty;
            p.Message = string.Empty;

            return View(p);
        }


        [HttpPost]
        public ActionResult ContactUs([DataSourceRequest]DataSourceRequest request, string jsonStringCompany)
        {

            int totalClientCount = 0;
            var json_serializer = new JavaScriptSerializer();
            ClientContactInfo c = json_serializer.Deserialize<ClientContactInfo>(jsonStringCompany);

            

            return Json(new { success = true, message = string.Format("Total client count:  {0}", totalClientCount) }, JsonRequestBehavior.AllowGet);
        }

    }



}