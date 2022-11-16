using Microsoft.AspNetCore.Http;
using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMovieImagesListViewModel
    {
        private 電影圖片movieIimagesList _ImagesList;

        public 電影圖片movieIimagesList ImagesList
        {
            get { return _ImagesList; }
            set { _ImagesList = value; }
        }

        public CMovieImagesListViewModel()
        {
            _ImagesList = new 電影圖片movieIimagesList();  //每做一個就建一個
        }
        public int 電影圖片編號miId
        {
            get { return _ImagesList.電影圖片編號miId; }
            set { _ImagesList.電影圖片編號miId = value; }
        }
        public int 電影編號movieId
        {
            get { return _ImagesList.電影編號movieId; }
            set { _ImagesList.電影編號movieId = value; }
        }
        public int 圖片編號imageId
        {
            get { return _ImagesList.圖片編號imageId; }
            set { _ImagesList.圖片編號imageId = value; }
        }

        //public virtual 電影圖片總表movieImage 圖片編號image { get; set; }

        //public int 圖片編號imageId { get; set; }
        public string 圖片image { get; set; }
        public string 圖片雲端imageImdb { get; set; }
        public string 圖片類型imageType { get; set; }
        public string 電影名稱movieName { get; set; }
        public int? 屏蔽invisible { get; set; }

        //public virtual 電影movie 電影編號movie { get; set; }
        //public int 電影編號movieId { get; set; }
        public int? 系列編號seriesId { get; set; }
        public string 中文標題titleCht { get; set; }
        public string 英文標題titleEng { get; set; }
        public int 上映年份releaseYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? 上映日期releaseDate { get; set; }
        public int 片長runtime { get; set; }
        public int? 電影分級編號ratingId { get; set; }
        public decimal? 評分rate { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public double? 票房boxOffice { get; set; }
        public string 劇情大綱plot { get; set; }
        public string 預告片trailer { get; set; }

        //photo
        public IFormFile photo { get; set; }
        public string movieImage { get; set; }
        public string movieIMDB { get; set; }
}
}
