using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 我的片單myMovieList
    {
        public int 我的片單myMovieListId { get; set; }
        public int 片單總表編號movieListId { get; set; }
        public int 會員編號memberId { get; set; }
        public int 電影編號movieId { get; set; }

        public virtual 會員member 會員編號member { get; set; }
        public virtual 片單總表movieList 片單總表編號movieList { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
