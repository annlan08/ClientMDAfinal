using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 公開等級編號publicId
    {
        public 公開等級編號publicId()
        {
            電影評論movieComments = new HashSet<電影評論movieComment>();
        }

        public int 公開等級編號publicId1 { get; set; }
        public string 公開等級public { get; set; }

        public virtual ICollection<電影評論movieComment> 電影評論movieComments { get; set; }
    }
}
