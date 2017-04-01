using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsNetworkModel
{
    public class Schedule
    {
        public int LeagueId { get; set; }
        public int PlayerId { get; set; }
        public DateTime GameTime { get; set; }
        public int GameLocation { get; set; }
        public string AddUserName { get; set; }
        public DateTime AddUpdateDt { get; set; }

    }
}
