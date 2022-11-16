using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影產地movieOrigin
    {
        public int 電影產地編號mcId { get; set; }
        public int 電影編號movieId { get; set; }
        public string 國家編號countryId { get; set; }

        public virtual 國家總表country 國家編號country { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
