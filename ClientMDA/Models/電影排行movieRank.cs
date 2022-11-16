using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影排行movieRank
    {
        public int 排行編號rankId { get; set; }
        public string 電影movie { get; set; }
        public string 電影英movieEn { get; set; }
        public string 電影排名movieRank { get; set; }
        public string 周末票房boxOfficeWeekend { get; set; }
        public string 累積票房boxOfficeGross { get; set; }
        public string 周次weeks { get; set; }
        public string 統計時間 { get; set; }
    }
}
