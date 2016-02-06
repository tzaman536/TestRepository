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
        public string FILE_UPLOAD_SUCCESSFUL = "File upload successful";
        public string PRODUCT_UPLOAD_SUCCESSFUL = "Product upload successful";
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
            {
                ViewBag.Message = "Upload new products.";
            }
            else
            {
                if(message.Equals(FILE_UPLOAD_SUCCESSFUL))
                    ViewBag.ErrorFound = false;
                else
                    ViewBag.ErrorFound = true;


                ViewBag.Message = message;
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
                    {
                        ViewBag.Message = "Record saved";
                        ViewBag.ErrorFound = false;

                        product.ProductID = addedProduct.ProductID;
                    }
                    else
                    {
                        ViewBag.Message = "Failed to save record";
                        ViewBag.ErrorFound = true;

                    }

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
            string uploadMessage = FILE_UPLOAD_SUCCESSFUL;
            bool fileUploadFailed = false;
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
                    uploadMessage = "Can't except product id 0. Add a product -> Save Changes -> Select the row -> Upload image";
                    fileIsValid = false;
                    fileUploadFailed = true;
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
                    logger.InfoFormat(FILE_UPLOAD_SUCCESSFUL);
                    
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
                fileUploadFailed = true;
            }


            return RedirectToAction("Index", new RouteValueDictionary(
                                                new { controller = "ProductAdmin", action = "Index", message =  uploadMessage, status = fileUploadFailed }));
        }



        [HttpPost]
        public ActionResult UploadProduct(HttpPostedFileBase file, string productName, string productDescription, string productLongDescription, decimal unitPrice)
        {
            string uploadMessage = PRODUCT_UPLOAD_SUCCESSFUL;
            bool fileUploadFailed = false;
            bool fileIsValid = true;
            try
            {
                var request = System.Web.HttpContext.Current.Request;

                FileInfo fi = new FileInfo(file.FileName);
                if (!fi.Extension.Equals(".jpg"))
                {
                    uploadMessage = "Please upload a valid [.jpg] image file";
                }

                
                if (fileIsValid)
                {
                    AmzProduct product = new AmzProduct()
                    {
                        ProductName = productName
                        ,ProductDescription = productDescription
                        ,ProductLongDescription = productLongDescription
                        ,UnitPrice = unitPrice
                    };
                    
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                    DirectoryInfo di = new DirectoryInfo(path);
                    if (!di.Exists)
                    {
                        di.Create();
                    }

                    string nowTicks = DateTime.Now.Ticks.ToString();

                    string sourceFile = string.Format(@"{0}\{1}_{2}",path, nowTicks, fi.Name);
                    file.SaveAs(sourceFile);
                    logger.InfoFormat("Saved input file as {0}", sourceFile);


                    Bitmap bmOriginal = new Bitmap(sourceFile);
                    ImageHandler ih = new ImageHandler();

                    product.SmallImageId = System.Web.HttpContext.Current.Server.MapPath("~/Content") + string.Format(@"\products\AMZ_Small_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 100, 100, 100, product.SmallImageId);
                    logger.InfoFormat("Resized input file and saved as {0}", product.SmallImageId);

                    product.MediumImageId = System.Web.HttpContext.Current.Server.MapPath("~/Content") + string.Format(@"\products\AMZ_Medium_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 400, 400, 100, product.MediumImageId);
                    logger.InfoFormat("Resized input file and saved as {0}", product.MediumImageId);

                    product.LargeImageId = System.Web.HttpContext.Current.Server.MapPath("~/Content") + string.Format(@"\products\AMZ_Large_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 400, 400, 100, product.LargeImageId);
                    logger.InfoFormat("Resized input file and saved as {0}",product.LargeImageId);

                    product.ImageUploadSuccessful = true;

                    productHandler.AddProduct(product);

                    logger.InfoFormat(PRODUCT_UPLOAD_SUCCESSFUL);

                    try
                    {
                        FileInfo fiFileToDelete = new FileInfo(sourceFile);
                        if (fiFileToDelete.Exists)
                            fiFileToDelete.Delete();
                        logger.InfoFormat("Removed file {0}", sourceFile);

                    }
                    catch (Exception ex1)
                    {
                        logger.Error(ex1);

                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                uploadMessage = string.Format("Product upload failed: Error: {0}", ex.Message);
                fileUploadFailed = true;
            }


            return RedirectToAction("Index", new RouteValueDictionary(
                                                new { controller = "ProductAdmin", action = "Index", message = uploadMessage, status = fileUploadFailed }));
        }



    }
}