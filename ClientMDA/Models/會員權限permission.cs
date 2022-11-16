using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 會員權限permission
    {
        public 會員權限permission()
        {
            會員members = new HashSet<會員member>();
        }

        public int 會員權限permission1 { get; set; }
        public string 權限名稱permissionName { get; set; }

        public virtual ICollection<會員member> 會員members { get; set; }
    }
}
