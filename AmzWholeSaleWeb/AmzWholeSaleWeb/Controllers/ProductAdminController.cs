using AmzBL.Products;
using AmzBL.Sections;
using AmzModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Simplex.Tools.Cart;
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
        private SectionDataHandler sectionHandler;

        public ProductAdminController()
        {
            productHandler = new AmzProductHandler();
            //sectionHandler = new SectionDataHandler();
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

            //ViewData["SourceList"] = new string[] { "select...", "Furnituer", "Home", "Construction" }.Select(x =>
            //new
            //{
            //    Id = x,
            //    Status = x
            //});
            PopulateSections();

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


        private void PopulateSections()
        {
            if (sectionHandler == null)
                sectionHandler = new SectionDataHandler();
            var sections = sectionHandler.GetSections();
            ViewData["SectionList"] = sections;
            ViewData["defaultSection"] = sections.First();
        }

        public ActionResult ViewTransactions()
        {
            return View();
        }

        #endregion

        public ActionResult Cart_Read([DataSourceRequest] DataSourceRequest request)
        {

            CartHandler ch = new CartHandler();
            string sql = @"      select c.CartId, p.ProductName, ci.Quantity, ci.Price, ci.Quantity * ci.Price as Total, c.DateCreated, c.CheckedOut
                    from dbo.Cart c
                    inner join dbo.CartItems ci on c.CartId = ci.CartId
                    inner join amz.Products p on ci.ProductId = p.ProductID
                    order by DateCreated, CheckedOut
              ";
            var result = ch.GetCartView(sql);

            return Json(result.ToDataSourceResult(request));
        }



        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(productHandler.GetProducts(null,null,true).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, AmzProduct product)
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


            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, AmzProduct product)
        {
            productHandler.UpdateProduct(product);

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, AmzProduct product)
        {
            string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");
                    
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

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> files, int productID)
        {
            string uploadMessage = FILE_UPLOAD_SUCCESSFUL;
            bool fileUploadFailed = false;
            bool fileIsValid = true;
            try
            {
                var request = System.Web.HttpContext.Current.Request;

                if (productID == 0)
                {
                    uploadMessage = "Can't except product id 0. Add a product -> Save Changes -> Select the row -> Upload image";
                    fileIsValid = false;
                    fileUploadFailed = true;
                }

                var product = productHandler.GetProduct(productID);
                string destinationFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/products");
                string nowTicks = DateTime.Now.Ticks.ToString();


                if (files.ElementAtOrDefault(0) != null)
                {
                    fileIsValid = true;
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(0).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.OriginalImageId);
                        files.ElementAtOrDefault(0).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);



                        Bitmap bmOriginal = new Bitmap(sourceFile);
                        ImageHandler ih = new ImageHandler();


                        if (product != null)
                        {
                            ih.Save(bmOriginal, 100, 100, 100, string.Format(@"{0}\{1}", destinationFilePath, product.SmallImageId));
                            logger.InfoFormat("Resized input file and saved as {0}", product.SmallImageId);

                            ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}", destinationFilePath, product.MediumImageId));
                            logger.InfoFormat("Resized input file and saved as {0}", product.MediumImageId);

                            ih.Save(bmOriginal, 400, 400, 100, string.Format(@"{0}\{1}", destinationFilePath, product.LargeImageId));
                            logger.InfoFormat("Resized input file and saved as {0}", product.LargeImageId);
                        }


                        logger.InfoFormat("Resized input file and saved as {0}", destinationFilePath);
                    }
                }

                fileIsValid = true;
                if (files.ElementAtOrDefault(1) != null)
                {
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(1).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image 1 file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        if(string.IsNullOrEmpty(product.ImageIdOne))
                            product.ImageIdOne = string.Format("AMZ_Image_1_{0}.jpg", nowTicks);

                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdOne);
                        files.ElementAtOrDefault(1).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);
                    }
                }


                if (files.ElementAtOrDefault(2) != null)
                {
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(2).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image 2 file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        if (string.IsNullOrEmpty(product.ImageIdTwo))
                            product.ImageIdTwo = string.Format("AMZ_Image_2_{0}.jpg", nowTicks);

                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdTwo);
                        files.ElementAtOrDefault(2).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);
                    }
                }

                if (files.ElementAtOrDefault(3) != null)
                {
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(3).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image 3 file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        if (string.IsNullOrEmpty(product.ImageIdThree))
                            product.ImageIdThree = string.Format("AMZ_Image_3_{0}.jpg", nowTicks);

                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdThree);
                        files.ElementAtOrDefault(3).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);
                    }
                }

                if (files.ElementAtOrDefault(4) != null)
                {
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(4).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image 4 file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        if (string.IsNullOrEmpty(product.ImageIdFour))
                            product.ImageIdFour = string.Format("AMZ_Image_4_{0}.jpg", nowTicks);

                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFour);
                        files.ElementAtOrDefault(4).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);
                    }
                }

                if (files.ElementAtOrDefault(5) != null)
                {
                    FileInfo fi = new FileInfo(files.ElementAtOrDefault(5).FileName);
                    if (!fi.Extension.ToLower().Equals(".jpg"))
                    {
                        uploadMessage = "Please upload a valid [.jpg] image 5 file";
                        fileIsValid = false;
                    }

                    if (fileIsValid)
                    {
                        if (string.IsNullOrEmpty(product.ImageIdFive))
                            product.ImageIdFive = string.Format("AMZ_Image_5_{0}.jpg", nowTicks);

                        string sourceFile = string.Format(@"{0}\{1}", destinationFilePath, product.ImageIdFive);
                        files.ElementAtOrDefault(5).SaveAs(sourceFile);

                        logger.InfoFormat("Saved input file as {0}", sourceFile);
                    }
                }


                productHandler.UpdateProduct(product);
                logger.InfoFormat(FILE_UPLOAD_SUCCESSFUL);
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
                if (!fi.Extension.ToLower().Equals(".jpg"))
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
                        ,UnitsInStock = unitInStock
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