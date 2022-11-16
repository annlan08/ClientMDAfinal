using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 回覆樓數floor
    {
        public int 樓數編號floorId { get; set; }
        public int 評論編號commentId { get; set; }
        public int 會員編號memberId { get; set; }
        public string 回覆內容floors { get; set; }
        public DateTime 發佈時間floorTime { get; set; }
        public int? 被按讚次數thumbsUp { get; set; }
        public int? 被倒讚次數thumbsDown { get; set; }
        public int 屏蔽invisible { get; set; }

        public virtual 電影評論movieComment 評論編號comment { get; set; }
    }
}
