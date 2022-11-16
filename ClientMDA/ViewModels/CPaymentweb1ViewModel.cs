using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;

namespace ClientMDA.ViewModels
{
    public class CPaymentweb1ViewModel
    {
        public int count { get; set; }
        public int screenID { get; set; }
        public int theaterID { get; set; }
        public int MovieID { get; set; }
        public int MovieCode { get; set; }
        public string Ticketstring { get; set; }
        public string seatInfo { get; set; }
        public string Data { get; set; }
        public string Time { get; set; }

    }
}
