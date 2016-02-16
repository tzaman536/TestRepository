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

        #region Views
        // GET: ProductAdmin

        public ActionResult Index(string message, bool errorFund=false)
        {

            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if(!User.IsInRole("AmzAdmin"))
            {
                return RedirectToAction("Index", "AmzHome");
            }

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

        public ActionResult ManageProduct(string message, bool errorFund = false)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }


            if (string.IsNullOrEmpty(message))
            {
                ViewBag.Message = "Manage existing products.";
            }
            else
            {
                if (message.Equals(FILE_UPLOAD_SUCCESSFUL))
                    ViewBag.ErrorFound = false;
                else
                    ViewBag.ErrorFound = true;


                ViewBag.Message = message;
            }

            return View();
        }

        #endregion


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
                string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");
                foreach (var product in products)
                {
                    
                    try
                    {
                        string fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.OriginalImageId);
                        FileInfo fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try
                            {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.SmallImageId);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.MediumImageId);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.LargeImageId);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdOne);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }

                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdTwo);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdThree);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFour);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }
                        fileToDelete = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFive);
                        fi = new FileInfo(fileToDelete);
                        if (fi.Exists)
                        {
                            try {
                                fi.Delete();
                            }
                            catch { }
                        }



                    }
                    catch (Exception ex)
                    {
                        logger.Fatal(ex);
                    }


                    productHandler.DeleteProduct(product);
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
                    //string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                    //DirectoryInfo di = new DirectoryInfo(path);
                    //if(!di.Exists)
                    //{
                    //    di.Create();
                    //}

                    var product = productHandler.GetProduct(productID);
                    string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");
                    string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.OriginalImageId);
                    file.SaveAs(sourceFile);
                    
                    logger.InfoFormat("Saved input file as {0}",sourceFile);



                    Bitmap bmOriginal = new Bitmap(sourceFile);
                    ImageHandler ih = new ImageHandler();


                    if(product != null)
                    {
                        ih.Save(bmOriginal, 100, 100, 100, string.Format(@"{0}\{1}", destinationFilePath, product.SmallImageId));
                        logger.InfoFormat("Resized input file and saved as {0}", product.SmallImageId);

                        ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}", destinationFilePath, product.MediumImageId));
                        logger.InfoFormat("Resized input file and saved as {0}", product.MediumImageId);

                        ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}", destinationFilePath, product.LargeImageId));
                        logger.InfoFormat("Resized input file and saved as {0}", product.LargeImageId);
                    }


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


            return RedirectToAction("ManageProduct", new RouteValueDictionary(
                                                new { controller = "ProductAdmin", action = "ManageProduct", message =  uploadMessage, status = fileUploadFailed }));
        }



        [HttpPost]
        public ActionResult UploadProduct(IEnumerable<HttpPostedFileBase> files, string productName, string productDescription, string productLongDescription, decimal unitPrice, int unitInStock)
        {
            string uploadMessage = PRODUCT_UPLOAD_SUCCESSFUL;
            bool fileUploadFailed = false;
            bool fileIsValid = true;
            try
            {
                var request = System.Web.HttpContext.Current.Request;

                FileInfo fi = new FileInfo(files.ElementAtOrDefault(0).FileName);
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
                    
                    //string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                    //DirectoryInfo di = new DirectoryInfo(path);
                    //if (!di.Exists)
                    //{
                    //    di.Create();
                    //}

                    string nowTicks = DateTime.Now.Ticks.ToString();
                    string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");

                    string originalImageId = string.Format("AMZ_Original_{0}.jpg",nowTicks);
                    string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);

                    files.ElementAtOrDefault(0).SaveAs(sourceFile);
                    logger.InfoFormat("Saved input file as {0}", sourceFile);
                    product.OriginalImageId = originalImageId;


                    Bitmap bmOriginal = new Bitmap(sourceFile);
                    ImageHandler ih = new ImageHandler();

                    

                    product.SmallImageId =  string.Format(@"AMZ_Small_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 100, 100, 100, string.Format(@"{0}\{1}",destinationFilePath,product.SmallImageId));
                    logger.InfoFormat("Resized input file and saved as {0}", product.SmallImageId);

                    product.MediumImageId = string.Format(@"AMZ_Medium_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}",destinationFilePath, product.MediumImageId));
                    logger.InfoFormat("Resized input file and saved as {0}", product.MediumImageId);

                    product.LargeImageId = string.Format(@"AMZ_Large_{0}.jpg", nowTicks);
                    ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}",destinationFilePath, product.LargeImageId));
                    logger.InfoFormat("Resized input file and saved as {0}",product.LargeImageId);


                    if(files.ElementAtOrDefault(1) != null )
                    {
                        originalImageId = string.Format("AMZ_Image_1_{0}.jpg", nowTicks);
                        sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);
                        product.ImageIdOne = originalImageId;
                        files.ElementAtOrDefault(1).SaveAs(sourceFile);
                        logger.InfoFormat("Saved image one file as {0}", sourceFile);
                    }

                    if (files.ElementAtOrDefault(2) != null)
                    {
                        originalImageId = string.Format("AMZ_Image_2_{0}.jpg", nowTicks);
                        sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);
                        product.ImageIdTwo = originalImageId;
                        files.ElementAtOrDefault(2).SaveAs(sourceFile);
                        logger.InfoFormat("Saved image two file as {0}", sourceFile);
                    }

                    if (files.ElementAtOrDefault(3) != null)
                    {
                        originalImageId = string.Format("AMZ_Image_3_{0}.jpg", nowTicks);
                        sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);
                        product.ImageIdThree = originalImageId;
                        files.ElementAtOrDefault(3).SaveAs(sourceFile);
                        logger.InfoFormat("Saved image three file as {0}", sourceFile);
                    }

                    if (files.ElementAtOrDefault(4) != null)
                    {
                        originalImageId = string.Format("AMZ_Image_4_{0}.jpg", nowTicks);
                        sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);
                        product.ImageIdFour = originalImageId;
                        files.ElementAtOrDefault(4).SaveAs(sourceFile);
                        logger.InfoFormat("Saved image four file as {0}", sourceFile);
                    }

                    if (files.ElementAtOrDefault(5) != null)
                    {
                        originalImageId = string.Format("AMZ_Image_5_{0}.jpg", nowTicks);
                        sourceFile = string.Format(@"{0}\{1}", destinationFilePath, originalImageId);
                        product.ImageIdFive = originalImageId;
                        files.ElementAtOrDefault(5).SaveAs(sourceFile);
                        logger.InfoFormat("Saved image five file as {0}", sourceFile);
                    }





                    product.ImageUploadSuccessful = true;

                    productHandler.AddProduct(product);

                    logger.InfoFormat(PRODUCT_UPLOAD_SUCCESSFUL);

                    //try
                    //{
                    //    FileInfo fiFileToDelete = new FileInfo(sourceFile);
                    //    if (fiFileToDelete.Exists)
                    //        fiFileToDelete.Delete();
                    //    logger.InfoFormat("Removed file {0}", sourceFile);

                    //}
                    //catch (Exception ex1)
                    //{
                    //    logger.Error(ex1);

                    //}
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