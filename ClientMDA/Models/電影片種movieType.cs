using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影片種movieType
    {
        public int 電影片種編號mtId { get; set; }
        public int 電影編號movieId { get; set; }
        public int 片種編號typeId { get; set; }

        public virtual 片種總表totalType 片種編號type { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
