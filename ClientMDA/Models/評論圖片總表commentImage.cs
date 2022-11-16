using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 評論圖片總表commentImage
    {
        public 評論圖片總表commentImage()
        {
            評論圖片明細commentImagesLists = new HashSet<評論圖片明細commentImagesList>();
        }

        public int 評論圖庫編號commentImageId { get; set; }
        public string 圖片image { get; set; }
        public int 屏蔽invisible { get; set; }

        public virtual ICollection<評論圖片明細commentImagesList> 評論圖片明細commentImagesLists { get; set; }
    }
}
