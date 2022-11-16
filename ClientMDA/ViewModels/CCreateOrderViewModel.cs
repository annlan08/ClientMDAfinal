using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CCreateOrderViewModel
    {
        public string email { get; set; }
        public int ScreenID { get; set; }
        public string SeatInfo { get; set; }
        public string TicketInfo { get; set; }
        public decimal fullPrice { get; set; }
    }
}
