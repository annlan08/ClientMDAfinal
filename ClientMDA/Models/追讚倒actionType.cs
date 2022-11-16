using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 追讚倒actionType
    {
        public 追讚倒actionType()
        {
            我的追蹤清單myFollowLists = new HashSet<我的追蹤清單myFollowList>();
        }

        public int 追讚倒編號actionTypeId { get; set; }
        public string 追讚倒名稱actionTypeName { get; set; }

        public virtual ICollection<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }
    }
}
