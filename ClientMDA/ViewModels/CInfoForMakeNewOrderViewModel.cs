using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CInfoForMakeNewOrderViewModel
    {
        public string TicketInfo { get; set; }
        public string SeatInfo { get; set; }
        public int ScreenID { get; set; }
        public string ProductInfo { get; set; }
        public List<CTicketItemViewModel> Alltciket { get; set; }
    }
}
