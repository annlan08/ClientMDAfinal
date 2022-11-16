using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影院theater
    {
        public 電影院theater()
        {
            商品資料products = new HashSet<商品資料product>();
            影廳cinemas = new HashSet<影廳cinema>();
        }

        public int 電影院編號theaterId { get; set; }
        public string 電影院名稱theaterName { get; set; }
        public int 影城編號mainTheaterId { get; set; }
        public string 營業時間businessHours { get; set; }
        public string 電話phone { get; set; }
        public string 地址address { get; set; }
        public string 官方網站url { get; set; }
        public string 電影院照片image { get; set; }

        public virtual 影城mainTheater 影城編號mainTheater { get; set; }
        public virtual ICollection<商品資料product> 商品資料products { get; set; }
        public virtual ICollection<影廳cinema> 影廳cinemas { get; set; }
    }
}
