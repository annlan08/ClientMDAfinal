using System;
using System.Collections.Generic;

namespace ClientMDA.ViewModels
{
    public class COrderInfoViewModel
    {

        public int OrderId { get; set; }
        public string MovieName { get; set; }
        public string MovieVersion { get; set; }
        public string StartTime { get; set; }
        public string StartDate { get; set; }
        public string MoviePicture { get; set; }
        public string TheaterName { get; set; }
        public string TheaterAddress { get; set; }
        public List<string> TicketInfo { get; set; }
        public List<string> ProductInfo { get; set; }
        public string SelectSeatInfo { get; set; }
        public int fullprice { get; set; }


    }
}