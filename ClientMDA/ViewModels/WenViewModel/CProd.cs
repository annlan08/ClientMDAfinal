using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;
using ClientMDA.ViewModel;

namespace ClientMDA.ViewModel.WenViewModel
{
    public class CProd
    {
        public int 商品編號productId { get; set; }
        public string 商品名稱productName { get; set; }
        public decimal 商品價格productPrice { get; set; }
        public int 電影院編號theaterId { get; set; }
        public string 商品圖片image { get; set; }
        public string 商品圖片路徑imagePath { get; set; }
        public string 商品介紹introduce { get; set; }
        public string 類別category { get; set; }
        public string 電影院名稱theaterName { get; set; }      
        public int inputdelete { get; set; }
        public int currentheaterID { get; set; }
        public IFormFile photo { get; set; }
        public 商品資料product product { get; set; }
        public 電影院theater theater { get; set; }
    }
}
