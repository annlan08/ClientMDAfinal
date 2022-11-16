using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 訂單明細orderDetail
    {
        public int 訂單編號orderId { get; set; }
        public int 訂單明細編號orderDetailId { get; set; }
        public int 票價明細ticketId { get; set; }
        public int 張數count { get; set; }

        public virtual 票價資訊ticketPrice 票價明細ticket { get; set; }
        public virtual 訂單總表order 訂單編號order { get; set; }
    }
}
