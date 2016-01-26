using AmzBL.Products;
using AmzModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Simplex.Tools.File;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public ActionResult Index(string message, bool errorFund=false)
        {
            if (string.IsNullOrEmpty(message))
                ViewBag.Message = "Manage your product by uploading product image and typing in product descritpion below.";
            else
                ViewBag.Message = message;

            ViewBag.ErrorFound = errorFund;
            
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
            string uploadMessage = "File upload successful";
            bool fileUploadSuccessful = true;
            bool fileIsValid = true;
            try
            {
                var request = System.Web.HttpContext.Current.Request;

                FileInfo fi = new FileInfo(file.FileName);
                if (!fi.Extension.Equals(".jpg"))
                {
                    uploadMessage = "Please upload a valid [.jpg] image file";
                }

                if(productID == 0)
                {
                    uploadMessage = "Can't except product id 0. Please select a valid data row";
                    fileIsValid = false;
                }

                if (fileIsValid)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                    DirectoryInfo di = new DirectoryInfo(path);
                    if(!di.Exists)
                    {
                        di.Create();
                    }

                    string sourceFile = string.Format(@"{0}\{1}_{2}", path,DateTime.Now.Ticks, file.FileName);
                    file.SaveAs(sourceFile);
                    
                    logger.InfoFormat("Saved input file as {0}",sourceFile);
                    string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content") + string.Format(@"\products\{0}.jpg",productID);
                    Bitmap bmOriginal = new Bitmap(sourceFile);
                    ImageHandler ih = new ImageHandler();
                    ih.Save(bmOriginal, 100, 100, 100, destinationFilePath);
                     
                    logger.InfoFormat("Resized input file and saved as {0}",destinationFilePath);
                    logger.InfoFormat("File upload successful");
                    
                    try
                    {
                        FileInfo fiFileToDelete = new FileInfo(sourceFile);
                        if (fiFileToDelete.Exists)
                            fiFileToDelete.Delete();
                        logger.InfoFormat("Removed file {0}",sourceFile);

                    }
                    catch (Exception ex1)
                    {
                        logger.Error(ex1);   
                        
                    }

                }

            }
            catch(Exception ex)
            {
                logger.Error(ex);
                uploadMessage = string.Format("File upload failed: Error: {0}", ex.Message);
                fileUploadSuccessful = false;
            }


            return RedirectToAction("Index", new RouteValueDictionary(
                                                new { controller = "ProductAdmin", action = "Index", message =  uploadMessage, status = fileUploadSuccessful }));
        }



    }
}