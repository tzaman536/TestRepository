using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZFactorModel.Ref;
using Kendo.Mvc.UI;
using System.Web.Script.Serialization;

namespace EZFactor1.Controllers
{

    

    public class RegisterCompanyController : Controller
    {
        //
        // GET: /RegisterCompany/

        public ActionResult Index()
        {
            Company p = new Company();
            p.CompanyName = string.Empty;
            p.EIN = string.Empty;
            p.FirstName = string.Empty;
            p.LastName = string.Empty;
            p.AddressLine1 = string.Empty;
            p.AddressLine2 = string.Empty;
            p.City = string.Empty;
            p.ContactName = string.Empty;
            p.Country = string.Empty;
            p.Email = string.Empty;
            p.FaxNumber = string.Empty;
            p.MobileNumber = string.Empty;
            p.PhoneNumber = string.Empty;
            p.State = string.Empty;
            p.ZipCode = string.Empty;
            p.Gender = "Male";
            p.Country = "United States";
            return View(p);
        }


        [HttpPost]
        public ActionResult SaveCompanyInfo([DataSourceRequest]DataSourceRequest request, string jsonStringCompany)
        {

            var json_serializer = new JavaScriptSerializer();
            Company c = json_serializer.Deserialize<Company>(jsonStringCompany);
            

     
            return Json(new { success = true, message = string.Format("First name is {0}",c.CompanyName) }, JsonRequestBehavior.AllowGet);
        }
    }
}
