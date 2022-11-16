using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 系列電影movieSeries
    {
        public 系列電影movieSeries()
        {
            電影movies = new HashSet<電影movie>();
        }

        public int 系列編號seriesId { get; set; }
        public string 系列名稱seriesName { get; set; }

        public virtual ICollection<電影movie> 電影movies { get; set; }
    }
}
