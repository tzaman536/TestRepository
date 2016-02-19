using AmzBL.Sections;
using AmzModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmzWholeSaleWeb.Controllers
{
    public class ReferenceDataController : Controller
    {
        private SectionDataHandler sectionDataHandler;


        public ReferenceDataController()
        {
            sectionDataHandler = new SectionDataHandler();
        }
        // GET: ReferenceData
        public ActionResult Index()
        {
            ViewBag.Message = "Reference data management page.";
            return View();
        }


        public ActionResult Section_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(sectionDataHandler.GetSections().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Section_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Section> sections)
        {
            var results = new List<Section>();

            if (sections != null && ModelState.IsValid)
            {
                foreach (var section in sections)
                {
                    var addedSection = sectionDataHandler.AddSection(section);
                    if (addedSection != null)
                    {
                        ViewBag.Message = "Record saved";
                        ViewBag.ErrorFound = false;

                        section.SectionID = addedSection.SectionID;
                    }
                    else
                    {
                        ViewBag.Message = "Failed to save record";
                        ViewBag.ErrorFound = true;

                    }

                    results.Add(section);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Section_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Section> sections)
        {
            if (sections != null && ModelState.IsValid)
            {
                foreach (var section in sections)
                {
                    sectionDataHandler.UpdateSection(section);
                }
            }
            ViewBag.Message = "Update successful.";
            return Json(sections.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Section_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Section> sections)
        {
            if (sections.Any())
            {
                foreach (var section in sections)
                {
                    sectionDataHandler.DeleteSection(section);
                }
            }

            return Json(sections.ToDataSourceResult(request, ModelState));
        }

    }
}