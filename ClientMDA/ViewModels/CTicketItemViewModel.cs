using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CTicketItemViewModel
    {
        public int TicketID { get; set; }
        public string TicketName { get; set; }
        public decimal TicketPrice { get; set; }
        public int TicketCount { get; set; }
    }
}
