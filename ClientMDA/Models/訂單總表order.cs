using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 訂單總表order
    {
        public 訂單總表order()
        {
            使用優惠明細usingCouponLists = new HashSet<使用優惠明細usingCouponList>();
            出售座位明細seatSolds = new HashSet<出售座位明細seatSold>();
            訂單明細orderDetails = new HashSet<訂單明細orderDetail>();
            購買商品明細receipts = new HashSet<購買商品明細receipt>();
        }

        public int 訂單編號orderId { get; set; }
        public int 會員編號memberId { get; set; }
        public DateTime 訂單時間orderTime { get; set; }
        public int 場次編號screeningId { get; set; }
        public int 訂單狀態編號orderStatusId { get; set; }

        public virtual 場次screening 場次編號screening { get; set; }
        public virtual 會員member 會員編號member { get; set; }
        public virtual 訂單狀態orderStatus 訂單狀態編號orderStatus { get; set; }
        public virtual ICollection<使用優惠明細usingCouponList> 使用優惠明細usingCouponLists { get; set; }
        public virtual ICollection<出售座位明細seatSold> 出售座位明細seatSolds { get; set; }
        public virtual ICollection<訂單明細orderDetail> 訂單明細orderDetails { get; set; }
        public virtual ICollection<購買商品明細receipt> 購買商品明細receipts { get; set; }
    }
}
