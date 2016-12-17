using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexInvoiceModel
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueDescription { get; set; }
        public string LeagueType { get; set; }
        public string LeagueLevel { get; set; }
        public string AddUserName { get; set; }
        public DateTime AddUpdateDt { get; set; }
    }
}
