using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 對象target
    {
        public 對象target()
        {
            我的追蹤清單myFollowLists = new HashSet<我的追蹤清單myFollowList>();
        }

        public int 對象targetId { get; set; }
        public string 對象名稱targetName { get; set; }

        public virtual ICollection<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }
    }
}
