using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 優惠總表coupon
    {
        public 優惠總表coupon()
        {
            優惠明細couponLists = new HashSet<優惠明細couponList>();
        }

        public int 優惠編號couponId { get; set; }
        public string 優惠名稱couponName { get; set; }
        public decimal 優惠折扣couponDiscount { get; set; }
        public string 優惠代碼couponCode { get; set; }
        public DateTime 優惠截止日期couponDueDate { get; set; }
        public int 優惠所需紅利bonusCost { get; set; }

        public virtual ICollection<優惠明細couponList> 優惠明細couponLists { get; set; }
    }
}
