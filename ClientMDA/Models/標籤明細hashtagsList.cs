using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 標籤明細hashtagsList
    {
        public int 標籤明細編號chId { get; set; }
        public int 評論編號commentId { get; set; }
        public int 標籤編號hashtagId { get; set; }

        public virtual 標籤總表hashtag 標籤編號hashtag { get; set; }
        public virtual 電影評論movieComment 評論編號comment { get; set; }
    }
}
