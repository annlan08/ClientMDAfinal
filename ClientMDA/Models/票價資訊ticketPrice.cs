using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 票價資訊ticketPrice
    {
        public 票價資訊ticketPrice()
        {
            訂單明細orderDetails = new HashSet<訂單明細orderDetail>();
        }

        public int 票價明細ticketId { get; set; }
        public int 影城編號mainTheaterId { get; set; }
        public int 票種編號ticketTypeId { get; set; }
        public decimal 價格ticketPrice { get; set; }

        public virtual 影城mainTheater 影城編號mainTheater { get; set; }
        public virtual 票種ticketType 票種編號ticketType { get; set; }
        public virtual ICollection<訂單明細orderDetail> 訂單明細orderDetails { get; set; }
    }
}
