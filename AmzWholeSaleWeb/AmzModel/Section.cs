using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmzModel
{
    public class Section
    {
        public int SectionID { get; set; }
	    public string SectionName { get; set; }
        public string SectionDescription { get; set; }
	    public DateTime AddDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
