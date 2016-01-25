using AmzBL.Products;
using AmzModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AmzWholeSaleWeb.Controllers
{

    public class ProductAdminController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ProductAdminController));

        private AmzProductHandler productHandler;

        public ProductAdminController()
        {
            productHandler = new AmzProductHandler();
        }

        // GET: ProductAdmin
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            else
            {
                ViewBag.Message = "Manage your product by uploading product image and typing in product descritpion below.";
            }
            return View();
        }


        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(productHandler.GetProducts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<AmzProduct> products)
        {
            var results = new List<AmzProduct>();

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    var addedProduct = productHandler.AddProduct(product);
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
                    productHandler.UpdateProduct(product);
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

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int productID)
        {
            var request = System.Web.HttpContext.Current.Request;

            string uploadMessage = "File upload successful";

            FileInfo fi = new FileInfo(file.FileName);
            if(!fi.Extension.Equals(".jpg"))
            {
                uploadMessage = "Please upload a valid [.jpg] image file";
            }

            return RedirectToAction("Index", new RouteValueDictionary(
                                                new { controller = "ProductAdmin", action = "Index", message =  uploadMessage}));
        }



    }
}