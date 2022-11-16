using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影movie
    {
        public 電影movie()
        {
            我的片單myMovieLists = new HashSet<我的片單myMovieList>();
            電影主演casts = new HashSet<電影主演cast>();
            電影代碼movieCodes = new HashSet<電影代碼movieCode>();
            電影圖片movieIimagesLists = new HashSet<電影圖片movieIimagesList>();
            電影導演movieDirectors = new HashSet<電影導演movieDirector>();
            電影片種movieTypes = new HashSet<電影片種movieType>();
            電影產地movieOrigins = new HashSet<電影產地movieOrigin>();
            電影評論movieComments = new HashSet<電影評論movieComment>();
        }

        public int 電影編號movieId { get; set; }
        public int? 系列編號seriesId { get; set; }
        public string 中文標題titleCht { get; set; }
        public string 英文標題titleEng { get; set; }
        public int 上映年份releaseYear { get; set; }
        public DateTime? 上映日期releaseDate { get; set; }
        public int 片長runtime { get; set; }
        public int? 電影分級編號ratingId { get; set; }
        public decimal? 評分rate { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public double? 票房boxOffice { get; set; }
        public string 劇情大綱plot { get; set; }
        public string 預告片trailer { get; set; }

        public virtual 系列電影movieSeries 系列編號series { get; set; }
        public virtual 電影分級movieRating 電影分級編號rating { get; set; }
        public virtual ICollection<我的片單myMovieList> 我的片單myMovieLists { get; set; }
        public virtual ICollection<電影主演cast> 電影主演casts { get; set; }
        public virtual ICollection<電影代碼movieCode> 電影代碼movieCodes { get; set; }
        public virtual ICollection<電影圖片movieIimagesList> 電影圖片movieIimagesLists { get; set; }
        public virtual ICollection<電影導演movieDirector> 電影導演movieDirectors { get; set; }
        public virtual ICollection<電影片種movieType> 電影片種movieTypes { get; set; }
        public virtual ICollection<電影產地movieOrigin> 電影產地movieOrigins { get; set; }
        public virtual ICollection<電影評論movieComment> 電影評論movieComments { get; set; }
    }
}
