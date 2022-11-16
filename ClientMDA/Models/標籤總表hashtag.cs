using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 標籤總表hashtag
    {
        public 標籤總表hashtag()
        {
            標籤明細hashtagsLists = new HashSet<標籤明細hashtagsList>();
        }

        public int 標籤編號hashtagId { get; set; }
        public string 標籤hashtag { get; set; }

        public virtual ICollection<標籤明細hashtagsList> 標籤明細hashtagsLists { get; set; }
    }
}
