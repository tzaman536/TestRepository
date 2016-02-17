using AmzBL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmzWholeSaleWeb.Controllers
{
    public class ProductImage
    {
        public string ImageId { get; set; }
    }

    public class ProductDetailController : Controller
    {
        private AmzProductHandler productHandler;

        public ProductDetailController()
        {
            productHandler = new AmzProductHandler();
        }

        // GET: ProductDetail
        public ActionResult Index(int productID)
        {
            var result = productHandler.GetProduct(productID);
            List<ProductImage> imageList = new List<ProductImage>();
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.OriginalImageId))
                {
                    var img = new ProductImage() { ImageId = result.OriginalImageId };
                    imageList.Add(img);
                }
                if (!string.IsNullOrEmpty(result.ImageIdOne))
                {
                    var img = new ProductImage() { ImageId = result.ImageIdOne };
                    imageList.Add(img);
                }
                if (!string.IsNullOrEmpty(result.ImageIdTwo))
                {
                    var img = new ProductImage() { ImageId = result.ImageIdTwo };
                    imageList.Add(img);
                }
                if (!string.IsNullOrEmpty(result.ImageIdThree))
                {
                    var img = new ProductImage() { ImageId = result.ImageIdThree };
                    imageList.Add(img);
                }
                if (!string.IsNullOrEmpty(result.ImageIdFour))
                {
                    var img = new ProductImage() { ImageId = result.ImageIdFour };
                    imageList.Add(img);
                }
                if (!string.IsNullOrEmpty(result.ImageIdFive))
                {
                    var img = new ProductImage() { ImageId = result.ImageIdFive };
                    imageList.Add(img);
                }

            }

            ViewBag.Product = result;
            return View(imageList);
        }
    }
}