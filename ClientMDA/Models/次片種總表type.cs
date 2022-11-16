using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 次片種總表type
    {
        public 次片種總表type()
        {
            電影片種movieTypes = new HashSet<電影片種movieType>();
        }

        public int 片種編號totalTypeId { get; set; }
        public int 次片種編號typeId { get; set; }
        public string 次片種名稱typeName { get; set; }

        public virtual 片種總表totalType 片種編號totalType { get; set; }
        public virtual ICollection<電影片種movieType> 電影片種movieTypes { get; set; }
    }
}
