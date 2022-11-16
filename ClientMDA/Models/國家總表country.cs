using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 國家總表country
    {
        public 國家總表country()
        {
            電影產地movieOrigins = new HashSet<電影產地movieOrigin>();
        }

        public string 國家編號countryId { get; set; }
        public string 國家名稱countryName { get; set; }
        public string 國旗countryImage { get; set; }

        public virtual ICollection<電影產地movieOrigin> 電影產地movieOrigins { get; set; }
    }
}
