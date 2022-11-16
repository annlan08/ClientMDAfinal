using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 片單總表movieList
    {
        public 片單總表movieList()
        {
            我的片單myMovieLists = new HashSet<我的片單myMovieList>();
        }

        public int 片單總表編號movieListId { get; set; }
        public string 片單總表名稱listName { get; set; }
        public int 會員編號memberId { get; set; }

        public virtual ICollection<我的片單myMovieList> 我的片單myMovieLists { get; set; }
    }
}
