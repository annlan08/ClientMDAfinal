using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 購買商品明細receipt
    {
        public int 購買商品明細編號receiptId { get; set; }
        public int 訂單編號orderId { get; set; }
        public int 商品編號productId { get; set; }
        public int 商品數量qty { get; set; }

        public virtual 商品資料product 商品編號product { get; set; }
        public virtual 訂單總表order 訂單編號order { get; set; }
    }
}
