using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CGetInfoForTicketActionViewModel
    {
        public int MovieID { get; set; }
        public int MovieCode { get; set; }
        public string seatInfo { get; set; }
        public int screenID { get; set; }
        public int theaterID { get; set; }
        public string theaterName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int ticketCount { get; set; }
    }
}
