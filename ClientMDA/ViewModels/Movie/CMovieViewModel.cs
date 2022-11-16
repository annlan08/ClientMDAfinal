using Microsoft.AspNetCore.Http;
using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMovieViewModel
    {
        private 電影movie _movie;
        private 電影主演cast _mCast;
        private 電影導演movieDirector _mDirector;
        private 電影產地movieOrigin _mOrigin;
        private 電影片種movieType _mType;
        private 電影圖片movieIimagesList _mIimagesList;

        public 電影movie movie
        {
            get { return _movie; }
            set { _movie = value; }
        }
        public 電影主演cast mCast
        {
            get { return _mCast; }
            set { _mCast = value; }
        }
        public 電影導演movieDirector mDirector
        {
            get { return _mDirector; }
            set { _mDirector = value; }
        }
        public 電影產地movieOrigin mOrigin
        {
            get { return _mOrigin; }
            set { _mOrigin = value; }
        }
        public 電影片種movieType mType
        {
            get { return _mType; }
            set { _mType = value; }
        }
        public 電影圖片movieIimagesList mIimagesList
        {
            get { return _mIimagesList; }
            set { _mIimagesList = value; }
        }

        public CMovieViewModel()
        {
            _movie = new 電影movie();
            _mCast = new 電影主演cast();
            _mDirector = new 電影導演movieDirector();
            _mOrigin = new 電影產地movieOrigin();
            _mType = new 電影片種movieType();
            _mIimagesList = new 電影圖片movieIimagesList();
        }

        //---------------------------電影---------------------------//

        #region
        [DisplayName("電影編號")]
        public int 電影編號movieId
        {
            get { return _movie.電影編號movieId; }
            set { _movie.電影編號movieId = value; }
        }

        [DisplayName("系列編號")]
        public int? 系列編號seriesId
        {
            get { return _movie.系列編號seriesId; }
            set { _movie.系列編號seriesId = value; }
        }

        //系列電影movieSeries
        //public int 系列編號seriesId { get; set; }
        public string 系列名稱seriesName { get; set; }

        [DisplayName("中文標題")]
        public string 中文標題titleCht
        {
            get { return _movie.中文標題titleCht; }
            set { _movie.中文標題titleCht = value; }
        }

        [DisplayName("英文標題")]
        public string 英文標題titleEng
        {
            get { return _movie.英文標題titleEng; }
            set { _movie.英文標題titleEng = value; }
        }

        [DisplayName("上映年份")]
        public int 上映年份releaseYear
        {
            get { return _movie.上映年份releaseYear; }
            set { _movie.上映年份releaseYear = value; }
        }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("上映日期")]
        public DateTime? 上映日期releaseDate
        {
            get { return _movie.上映日期releaseDate; }
            set { _movie.上映日期releaseDate = value; }
        }

        [DisplayName("片長")]
        public int 片長runtime
        {
            get { return _movie.片長runtime; }
            set { _movie.片長runtime = value; }
        }

        [DisplayName("電影分級")]
        public int? 電影分級編號ratingId
        {
            get { return _movie.電影分級編號ratingId; }
            set { _movie.電影分級編號ratingId = value; }
        }
        
        //電影分級movieRating
        //public int? 分級編號ratingId { get; set; }
        public string 分級級數ratingLevel { get; set;}
        public byte[] 分級圖片ratingImage { get; set ;}

        [DisplayName("評分")]
        public decimal? 評分rate
        {
            get { return _movie.評分rate; }
            set { _movie.評分rate = value; }
        }

        [DisplayName("期待度")]
        public decimal? 期待度anticipation
        {
            get { return _movie.期待度anticipation; }
            set { _movie.期待度anticipation = value; }
        }

        [DisplayName("票房(億美元)")]
        public double? 票房boxOffice
        {
            get { return _movie.票房boxOffice; }
            set { _movie.票房boxOffice = value; }
        }

        [DisplayName("劇情大綱")]
        public string 劇情大綱plot
        {
            get { return _movie.劇情大綱plot; }
            set { _movie.劇情大綱plot = value; }
        }

        public string 預告片trailer
        {
            get { return _movie.預告片trailer; }
            set { _movie.預告片trailer = value; }
        }

        //public virtual 系列電影movieSeries 系列編號series { get; set; }
        //public virtual 電影分級movieRating 電影分級編號rating { get; set; }

        #endregion

        //---------------------------演員---------------------------//

        #region
        [DisplayName("主演編號")]
        public int 主演編號maId
        {
            get { return _mCast.主演編號maId; }
            set { _mCast.主演編號maId = value; }
        }

        [DisplayName("演員編號")]
        public int 演員編號actorId
        {
            get { return _mCast.演員編號actorId; }
            set { _mCast.演員編號actorId = value; }
        }

        //演員總表actor
        //public int 演員編號actorsId { get; set; }
        public string 演員中文名字nameCht { get; set; }
        public string 演員英文名字nameEng { get; set; }
        public string 演員照片image { get; set; }

        [DisplayName("角色名字")]
        public string 角色名字characterName
        {
            get { return _mCast.角色名字characterName; }
            set { _mCast.角色名字characterName = value; }
        }

        [DisplayName("角色說明")]
        public string 角色說明characterIllustrate
        {
            get { return _mCast.角色說明characterIllustrate; }
            set { _mCast.角色說明characterIllustrate = value; }
        }

        //public virtual 演員總表actor 演員編號actor { get; set; }
        #endregion

        //---------------------------導演---------------------------//

        #region
        [DisplayName("電影導演編號")]
        public int 電影導演編號mdId
        {
            get { return _mDirector.電影導演編號mdId; }
            set { _mDirector.電影導演編號mdId = value; }
        }

        [DisplayName("導演編號")]
        public int 導演編號directorId
        {
            get { return _mDirector.導演編號directorId; }
            set { _mDirector.導演編號directorId = value; }
        }

        //導演總表director
        //public int 導演編號directorId { get; set; }
        public string 導演中文名字nameCht { get; set; }
        public string 導演英文名字nameEng { get; set; }
        public string 導演照片image { get; set; }

        //public virtual 導演總表director 導演編號director { get; set; }
        #endregion

        //---------------------------電影產地---------------------------//

        #region
        [DisplayName("電影產地編號")]
        public int 電影產地編號mcId
        {
            get { return _mOrigin.電影產地編號mcId; }
            set { _mOrigin.電影產地編號mcId = value; }
        }

        [DisplayName("國家編號")]
        public string 國家編號countryId
        {
            get { return _mOrigin.國家編號countryId; }
            set { _mOrigin.國家編號countryId = value; }
        }

        //國家總表country
        //public string 國家編號countryId { get; set; }
        public string 國家名稱countryName { get; set; }
        public byte[] 國旗countryImage { get; set; }

        //public virtual 國家總表country 國家編號country { get; set; }
        #endregion

        //---------------------------電影片種---------------------------//

        #region
        [DisplayName("電影片種編號")]
        public int 電影片種編號mtId
        {
            get { return _mType.電影片種編號mtId; }
            set { _mType.電影片種編號mtId = value; }
        }

        [DisplayName("片種編號")]
        public int 片種編號typeId
        {
            get { return _mType.片種編號typeId; }
            set { _mType.片種編號typeId = value; }
        }
        //片種總表totalType
        //public int 片種編號totalTypeId { get; set; }
        public string 片種名稱totalTypeName { get; set; }

        //public virtual 片種總表totalType 片種編號type { get; set; }
        #endregion

        //---------------------------電影圖片---------------------------//

        #region
        [DisplayName("電影圖片編號")]
        public int 電影圖片編號miId
        {
            get { return _mIimagesList.電影圖片編號miId; }
            set { _mIimagesList.電影圖片編號miId = value; }
        }

        [DisplayName("圖片編號")]
        public int 圖片編號imageId
        {
            get { return _mIimagesList.圖片編號imageId; }
            set { _mIimagesList.圖片編號imageId = value; }
        }

        //電影圖片總表movieImage
        //public int 圖片編號imageId { get; set; }
        public string 圖片image { get; set; }
        public string 圖片雲端imageImdb { get; set; }
        public string 圖片類型imageType { get; set; }
        public string 電影名稱movieName { get; set; }
        public int? 屏蔽invisible { get; set; }

        //public virtual 電影圖片總表movieImage 圖片編號image { get; set; }
        #endregion


        public IFormFile mImgFrIFF { get; set; }
        public List<string> mImgFrList { get; set; }
        public List<string> mCountryName { get; set; }
        //public List<byte[]> mCountryImg { get; set; }
        public List<CCountryImageViewModel> mCountryList { get; set; }
        public List<CMovieDirectorViewModel> mDirectorList { get; set; }
        public List<CMovieActorViewModel> mActorList { get; set; }
        public List<CMovieTypeViewModel> mTypeList { get; set; }
        public List<CMovieImagesListViewModel> mPoster { get; set; } 
        public List<decimal?> mRateList { get; set; }
    }
}
