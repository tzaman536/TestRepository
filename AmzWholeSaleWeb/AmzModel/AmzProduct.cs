using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmzModel
{
    public class AmzProduct
    {

        public int ProductID { get; set; }
	    public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductLongDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public bool ImageUploadSuccessful { get; set; }
        public string SmallImageId { get; set; }
        public string MediumImageId { get; set; }
        public string LargeImageId { get; set; }
        public string OriginalImageId { get; set; }
        public string ImageIdOne { get; set; }
        public string ImageIdTwo { get; set; }
        public string ImageIdThree { get; set; }
        public string ImageIdFour { get; set; }
        public string ImageIdFive { get; set; }
        public int TotalItemsInCart { get; set; }
        public decimal CostOfItemsInCart { get; set; }
        public DateTime AddDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
