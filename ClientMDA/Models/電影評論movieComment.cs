using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影評論movieComment
    {
        public 電影評論movieComment()
        {
            回覆樓數floors = new HashSet<回覆樓數floor>();
            標籤明細hashtagsLists = new HashSet<標籤明細hashtagsList>();
            評論圖片明細commentImagesLists = new HashSet<評論圖片明細commentImagesList>();
        }

        public int 評論編號commentId { get; set; }
        public int 會員編號memberId { get; set; }
        public int 電影編號movieId { get; set; }
        public decimal? 評分rate { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public string 評論標題commentTitle { get; set; }
        public string 評論內容comments { get; set; }
        public DateTime 發佈時間commentTime { get; set; }
        public DateTime? 觀影日期viewingTime { get; set; }
        public string 觀影方式source { get; set; }
        public int? 公開等級編號publicId { get; set; }
        public bool? 是否開放討論串oxFloor { get; set; }
        public int 屏蔽invisible { get; set; }

        public virtual 公開等級編號publicId 公開等級編號public { get; set; }
        public virtual 會員member 會員編號member { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
        public virtual ICollection<回覆樓數floor> 回覆樓數floors { get; set; }
        public virtual ICollection<標籤明細hashtagsList> 標籤明細hashtagsLists { get; set; }
        public virtual ICollection<評論圖片明細commentImagesList> 評論圖片明細commentImagesLists { get; set; }
    }
}
