using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 演員總表actor
    {
        public 演員總表actor()
        {
            電影主演casts = new HashSet<電影主演cast>();
        }

        public int 演員編號actorsId { get; set; }
        public string 演員中文名字nameCht { get; set; }
        public string 演員英文名字nameEng { get; set; }
        public string 演員照片image { get; set; }

        public virtual ICollection<電影主演cast> 電影主演casts { get; set; }
    }
}
