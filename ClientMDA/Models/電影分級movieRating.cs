using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影分級movieRating
    {
        public 電影分級movieRating()
        {
            電影movies = new HashSet<電影movie>();
        }

        public int 分級編號ratingId { get; set; }
        public string 分級級數ratingLevel { get; set; }
        public string 分級說明ratingIllustrate { get; set; }
        public byte[] 分級圖片ratingImage { get; set; }

        public virtual ICollection<電影movie> 電影movies { get; set; }
    }
}
