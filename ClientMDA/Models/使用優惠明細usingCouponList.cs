using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 使用優惠明細usingCouponList
    {
        public int 使用優惠明細編號usingCouponListId { get; set; }
        public int 優惠明細編號couponListId { get; set; }
        public int 訂單編號orderId { get; set; }

        public virtual 優惠明細couponList 優惠明細編號couponList { get; set; }
        public virtual 訂單總表order 訂單編號order { get; set; }
    }
}
