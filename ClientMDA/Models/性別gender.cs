using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 性別gender
    {
        public 性別gender()
        {
            會員members = new HashSet<會員member>();
        }

        public int 性別gender1 { get; set; }
        public string 性別名稱genderName { get; set; }

        public virtual ICollection<會員member> 會員members { get; set; }
    }
}
