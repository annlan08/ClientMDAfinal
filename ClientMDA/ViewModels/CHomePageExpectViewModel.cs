using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CHomePageExpectViewModel
    {
        public int 電影編號Movie_ID { get; set; }
        public string 中文標題Title_Cht { get; set; }
        public string 電影英Movie_En { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public string 圖片雲端ImageIMDB { get; set; }        
        public DateTime? 上映日期Release_Date { get; set; }

        public int 會員編號Member_IDbook { get; set; }
        public int 電影編號Movie_IDbook { get; set; }
        public int 片單總表編號MovieList_ID { get; set; }

        public int 我的片單MyMovieList_ID { get; set; }
        public int 會員編號Member_ID { get; set; }
    }
}
