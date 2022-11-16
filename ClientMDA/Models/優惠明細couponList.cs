using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 優惠明細couponList
    {
        public 優惠明細couponList()
        {
            使用優惠明細usingCouponLists = new HashSet<使用優惠明細usingCouponList>();
        }

        public int 優惠明細編號couponListId { get; set; }
        public int 會員編號memberId { get; set; }
        public int 優惠編號couponId { get; set; }
        public bool 是否使用優惠oxCouponUsing { get; set; }
        public int 訂單編號orderId { get; set; }

        public virtual 優惠總表coupon 優惠編號coupon { get; set; }
        public virtual 會員member 會員編號member { get; set; }
        public virtual ICollection<使用優惠明細usingCouponList> 使用優惠明細usingCouponLists { get; set; }
    }
}
