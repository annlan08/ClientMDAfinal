using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 商品資料product
    {
        public 商品資料product()
        {
            購買商品明細receipts = new HashSet<購買商品明細receipt>();
        }

        public int 商品編號productId { get; set; }
        public string 商品名稱productName { get; set; }
        public decimal 商品價格productPrice { get; set; }
        public int 電影院編號theaterId { get; set; }
        public string 商品圖片image { get; set; }
        public string 商品圖片路徑imagePath { get; set; }
        public string 商品介紹introduce { get; set; }
        public string 類別category { get; set; }

        public virtual 電影院theater 電影院編號theater { get; set; }
        public virtual ICollection<購買商品明細receipt> 購買商品明細receipts { get; set; }
    }
}
