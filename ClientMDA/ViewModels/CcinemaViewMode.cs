using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CcinemaViewMode
    {
        public int CinemaID{ get; set; }
        public string CinemaName { get; set; }
        public List<string> 放映時間 { get; set; }
        public List<int> 場次ID { get; set; }
        public List<DateTime> 場次放映日期 { get; set; }

        public List<string> photo { get; set; }
        public List<string> moviename { get; set; }
        public string 電影圖片 { get; set; }
        public List<CSeatViewMode> 場次座位數量 { get; set; }
    }
}
