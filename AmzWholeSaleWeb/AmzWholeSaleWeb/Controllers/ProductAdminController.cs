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
    public class ProductAdminController : Controller
    {

        // GET: ProductAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(AmzBL.Products.AmzProductHandler.GetProducts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<AmzProduct> products)
        {
            var results = new List<AmzProduct>();

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    var addedProduct = AmzBL.Products.AmzProductHandler.AddProduct(product);
                    if (addedProduct != null)
                        product.ProductID = addedProduct.ProductID;

                    results.Add(product);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<AmzProduct> products)
        {
            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //productService.Update(product);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<AmzProduct> products)
        {
            if (products.Any())
            {
                foreach (var product in products)
                {
                    //productService.Destroy(product);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

    }
}