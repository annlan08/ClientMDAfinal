using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影導演movieDirector
    {
        public int 電影導演編號mdId { get; set; }
        public int 電影編號movieId { get; set; }
        public int 導演編號directorId { get; set; }

        public virtual 導演總表director 導演編號director { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
