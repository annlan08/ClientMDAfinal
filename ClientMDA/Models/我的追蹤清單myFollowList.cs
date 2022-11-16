using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 我的追蹤清單myFollowList
    {
        public int 我的追蹤清單編號cfId { get; set; }
        public int 會員編號memberId { get; set; }
        public int 對象targetId { get; set; }
        public int 追讚倒編號actionTypeId { get; set; }
        public int 連接編號connectId { get; set; }
        public string 檢舉理由reportReason { get; set; }
        public int? 處理狀態status { get; set; }

        public virtual 對象target 對象target { get; set; }
        public virtual 會員member 會員編號member { get; set; }
        public virtual 追讚倒actionType 追讚倒編號actionType { get; set; }
    }
}
