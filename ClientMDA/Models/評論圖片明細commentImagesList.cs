using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 評論圖片明細commentImagesList
    {
        public int 評論圖片編號ccId { get; set; }
        public int 評論圖庫編號commentImageId { get; set; }
        public int 評論編號commentId { get; set; }

        public virtual 評論圖片總表commentImage 評論圖庫編號commentImage { get; set; }
        public virtual 電影評論movieComment 評論編號comment { get; set; }
    }
}
