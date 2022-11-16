using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CHomePageRankViewModel
    {
        public int 電影編號Movie_ID { get; set; }
        public string 電影Movie { get; set; }
        public string 電影英Movie_En { get; set; }
        public string 電影排名Movie_Rank { get; set; }
        public string 圖片雲端ImageIMDB { get; set; }
        public string 周末票房BoxOffice_Weekend { get; set; }
        public string 累積票房BoxOffice_Gross { get; set; }
        public string 周次Weeks { get; set; }
        public string 統計時間 { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public decimal? 評分Rate { get; set; }


        public int 會員編號Member_IDbook { get; set; }
        public int 電影編號Movie_IDbook { get; set; }
        public int 片單總表編號MovieList_ID { get; set; }

        public int 我的片單MyMovieList_ID{ get; set; }
        public int 會員編號Member_ID { get; set; }
    }
}
