using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影圖片movieIimagesList
    {
        public int 電影圖片編號miId { get; set; }
        public int 電影編號movieId { get; set; }
        public int 圖片編號imageId { get; set; }

        public virtual 電影圖片總表movieImage 圖片編號image { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
