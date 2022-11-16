using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 出售座位狀態seatStatus
    {
        public int 出售座位編號seatId { get; set; }
        public int 場次編號screeningId { get; set; }
        public string 出售座位資訊seatSoldInfo { get; set; }

        public virtual 場次screening 場次編號screening { get; set; }
    }
}
