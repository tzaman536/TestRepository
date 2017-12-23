using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RnzssWeb.Models
{
    public class RfqCountSummary
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string RfqStatus { get; set; }
        public double StatusCount { get; set; }



        private double _totalSent;
        public double TotalSent
        {
            get
            {
                return _totalSent;
            }
            set
            {
                _totalSent = value;
            }
        }

        private double _totalOpen;
        public double TotalOpen
        {
            get
            {
                return _totalOpen;
            }
            set
            {
                _totalOpen = value;

            }
        }

        private double _totalReadyToBid;
        public double TotalReadyToBid
        {
            get
            {
                return _totalReadyToBid;
            }
            set
            {
                _totalReadyToBid = value;

            }
        }

        private double _totalBid;
        public double TotalBid
        {
            get
            {
                return _totalBid;
            }
            set
            {
                _totalBid = value;

            }
        }

        private double _totalAwarded;
        public double TotalAwarded
        {
            get
            {
                return _totalAwarded;
            }
            set
            {
                _totalAwarded = value;

            }
        }


        private void RecalculatePercent()
        {
            double totalCount = _totalSent
                    + _totalOpen
                    + _totalReadyToBid
                    + _totalBid
                    + _totalAwarded;

            if (totalCount != 0)
            {
                _totalSent = Math.Round((_totalSent / totalCount) * 100, 2);
                _totalOpen = Math.Round((_totalOpen / totalCount) * 100, 2);
                _totalReadyToBid = Math.Round((_totalReadyToBid / totalCount) * 100, 2);
                _totalBid = Math.Round((_totalBid / totalCount) * 100, 2);
                _totalAwarded = Math.Round((_totalAwarded / totalCount) * 100, 2);
            }
        }

        public void LoadSummary()
        {
            var rfqSummaryList = GetRfqSummaryList();

            foreach (var item in rfqSummaryList)
            {
                switch (item.RfqStatus)
                {
                    case "Open":
                        TotalOpen = item.StatusCount;
                        break;
                    case "Awarded":
                        TotalAwarded = item.StatusCount;
                        break;
                    case "Bid":
                        TotalBid = item.StatusCount;
                        break;
                    case "ReadyToBid":
                        TotalReadyToBid = item.StatusCount;
                        break;
                    case "Sent":
                        TotalSent = item.StatusCount;
                        break;
                    default:
                        break;
                }
            }

            RecalculatePercent();
        }

        public static IEnumerable<RfqCountSummary> GetRfqSummaryList()
        {
            using (IDbConnection connection = CommonMethods.OpenConnection())
            {
                try
                {
                    return connection.Query<RfqCountSummary>(string.Format(@"
                                                        select RfqStatus,count(*) StatusCount
                                                        from rnz.RequestForQuote
                                                        Group by RfqStatus

                                                        "), commandTimeout: 0).ToList();
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }

            }

            return null;

        }
    }



    public class RfqCountByMonth
    {
        public double January { get; set; }
        public double February { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }


        public void Load()
        {
            Random rnd = new Random();

            January = rnd.Next(300);
            February = rnd.Next(300);
            March = rnd.Next(300);
            April = rnd.Next(300);
            May = rnd.Next(300);
            June = rnd.Next(300);
            July = rnd.Next(300);
            August = rnd.Next(300);
            September = rnd.Next(300);
            October = rnd.Next(300);
            November = rnd.Next(300);
            December = rnd.Next(300);

        }
    }

}
