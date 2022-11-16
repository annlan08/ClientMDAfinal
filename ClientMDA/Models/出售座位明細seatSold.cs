using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 出售座位明細seatSold
    {
        public int 訂單編號orderId { get; set; }
        public int 出售明細編號soldId { get; set; }
        public int 場次編號screeningId { get; set; }
        public string 座位表編號seatId { get; set; }

        public virtual 場次screening 場次編號screening { get; set; }
        public virtual 訂單總表order 訂單編號order { get; set; }
    }
}
