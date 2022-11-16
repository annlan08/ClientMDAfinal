using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;
using ClientMDA.ViewModel;

namespace ClientMDA.ViewModel.WenViewModel
{
    public class CShoppingProd
    {
        private 商品資料product _product;
        //private 電影院theater _theater;

        public 商品資料product product
        {
            get { return _product; }
            set { _product = value; }
        }
        //public 電影院theater theater
        //{
        //    get { return _theater; }
        //    set { _theater = value; }
        //}
        public CShoppingProd()
        {
            _product = new 商品資料product();
            //_theater = new 電影院theater();
        }

        public int 商品編號productId
        {
            get { return _product.商品編號productId; }
            set { _product.商品編號productId = value; }
        }

        public string 商品名稱productName
        {
            get { return _product.商品名稱productName; }
            set { _product.商品名稱productName = value; }
        }

        public decimal 商品價格productPrice
        {
            get { return _product.商品價格productPrice; }
            set { _product.商品價格productPrice = value; }
        }
        public int 電影院編號theaterId
        {
            get { return _product.電影院編號theaterId; }
            set { _product.電影院編號theaterId = value; }
        }

        public string 商品圖片路徑imagePath
        {
            get { return _product.商品圖片路徑imagePath; }
            set { _product.商品圖片路徑imagePath = value; }
        }

        public string 商品介紹introduce
        {
            get { return _product.商品介紹introduce; }
            set { _product.商品介紹introduce = value; }
        }

        public string 類別category
        {
            get { return _product.類別category; }
            set { _product.類別category = value; }
        }
        //public string 電影院名稱theaterName {
        //    get { return _theater.電影院名稱theaterName; }
        //    set { _theater.電影院名稱theaterName = value; }
        //}
       
        //public 電影院theater theater { get; set; }
        
        //public IFormFile photo { get; set; }
    }
}
