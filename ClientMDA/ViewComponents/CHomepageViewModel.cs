
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CHomepageViewModel
    {
        //rate
        public int 會員編號Member_ID { get; set; }
        public int 電影編號Movie_ID { get; set; }
        public DateTime 發佈時間Comment_Time { get; set; }
        public decimal 評分Rate { get; set; }
        //list
        public int 片單總表編號MovieList_ID { get; set; }
        public int 會員編號Member_IDbook { get; set; }
        public int 電影編號Movie_IDbook { get; set; }
        //removelist
        public int 我的片單MyMovieList_ID { get; set; }
        //List<int> 圖片編號
        public List<int> 圖片編號 { get; set; }
        //comment
        public int 評論編號Comment_ID { get; set; }
    }

    
    
}
