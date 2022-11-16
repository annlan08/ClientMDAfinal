using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 片種總表totalType
    {
        public 片種總表totalType()
        {
            電影片種movieTypes = new HashSet<電影片種movieType>();
        }

        public int 片種編號totalTypeId { get; set; }
        public string 片種名稱totalTypeName { get; set; }

        public virtual ICollection<電影片種movieType> 電影片種movieTypes { get; set; }
    }
}
