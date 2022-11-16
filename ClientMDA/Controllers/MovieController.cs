using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMDA.Controllers
{
    public class MovieController : Controller
    {
        private readonly MDAContext _MDAcontext;

        public MovieController(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
            _MDAcontext.電影圖片movieIimagesLists.ToList();
            _MDAcontext.電影圖片總表movieImages.ToList();
        }

        public ActionResult Rank(CKeywordViewModel model,int? box ,int? year)
        {
            box ??= 2;year ??= 1;//??= >> if null
            電影movie K = new();
            List<CMovieViewModel> datas = null;
            var mPoster = _MDAcontext.電影圖片movieIimagesLists.Select(i => i);
            if (string.IsNullOrEmpty(model.txtKeyword))
            {
                if (box == 1)//依票房
                {
                    if (year == 1)//全部
                    {
                        datas = _MDAcontext.電影movies.OrderByDescending(m => m.票房boxOffice).Select
                                (m => new CMovieViewModel
                                {
                                    movie = m,
                                    mRateList = m.電影評論movieComments.Select(r => r.評分rate).ToList(),
                                    mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId)
                                    .Select(c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                                    mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                                    (i => new CMovieImagesListViewModel
                                    {
                                        movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                                        movieImage = i.圖片編號image.圖片image,
                                    }).ToList(),
                                }).Take(50).ToList();
                    }
                    else//新片
                    {
                        datas = _MDAcontext.電影movies.OrderByDescending(m => m.票房boxOffice).Where(i => (DateTime.Now.Year - i.上映年份releaseYear) <= 1).Select
                                (m => new CMovieViewModel
                                {
                                    movie = m,
                                    mRateList = m.電影評論movieComments.Select(r => r.評分rate).ToList(),
                                    mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId)
                                    .Select(c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                                    mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                                    (i => new CMovieImagesListViewModel
                                    {
                                        movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                                        movieImage = i.圖片編號image.圖片image,
                                    }).ToList(),
                                }).Take(50).ToList();
                    }
                }
                else//依評分
                {
                    if (year == 1)//全部
                    {
                        datas = _MDAcontext.電影movies.OrderByDescending(m => m.評分rate).Select
                                (m => new CMovieViewModel
                                {
                                    movie = m,
                                    mRateList = m.電影評論movieComments.Select(r => r.評分rate).ToList(),
                                    mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId)
                                    .Select(c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                                    mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                                    (i => new CMovieImagesListViewModel
                                    {
                                        movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                                        movieImage = i.圖片編號image.圖片image,
                                    }).ToList(),
                                }).Take(50).ToList();
                    }
                    else//新片
                    {
                        datas = _MDAcontext.電影movies.OrderByDescending(m => m.評分rate).Where(i => (DateTime.Now.Year - i.上映年份releaseYear) <= 1).Select
                                (m => new CMovieViewModel
                                {
                                    movie = m,
                                    mRateList = m.電影評論movieComments.Select(r => r.評分rate).ToList(),
                                    mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId)
                                    .Select(c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                                    mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                                    (i => new CMovieImagesListViewModel
                                    {
                                        movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                                        movieImage = i.圖片編號image.圖片image,
                                    }).ToList(),
                                }).Take(50).ToList();
                    }
                }
            }
            else
            {
                datas = _MDAcontext.電影movies.Where(m => m.中文標題titleCht.Contains(model.txtKeyword) ||
                                                           m.英文標題titleEng.Contains(model.txtKeyword)).Select
                         (m => new CMovieViewModel
                         {
                             movie = m,
                             mRateList = m.電影評論movieComments.Select(r => r.評分rate).ToList(),
                             mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId)
                             .Select(c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                             mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                                    (i => new CMovieImagesListViewModel
                                    {
                                        movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                                        movieImage = i.圖片編號image.圖片image,
                                    }).ToList(),
                         }).ToList();
            }
            return View(datas);
        }

        public IActionResult 電影介紹(int? id) //電影資訊/電影評論
        {
            CMovieViewModel datas = null;
            var q = _MDAcontext.電影圖片movieIimagesLists.Select(i => i);
            datas = _MDAcontext.電影movies.Where(m => m.電影編號movieId == id).Select
            (m => new CMovieViewModel
            {
                movie = m,
                分級級數ratingLevel = m.電影分級編號rating.分級級數ratingLevel,
                分級圖片ratingImage = m.電影分級編號rating.分級圖片ratingImage,
                系列名稱seriesName = m.系列編號series.系列名稱seriesName,
                mRateList = m.電影評論movieComments.Select(i => i.評分rate).ToList(),
                mPoster = m.電影圖片movieIimagesLists.Select(
                    p => new CMovieImagesListViewModel                  
                    {
                        ImagesList = p,
                        movieImage = p.圖片編號image.圖片image,
                    }
                    ).ToList(),
                    //導演List
                    mDirectorList = _MDAcontext.電影導演movieDirectors.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                (c => new CMovieDirectorViewModel
                {
                    directorChtName = c.導演編號director.導演中文名字nameCht,
                    directorEngName = c.導演編號director.導演英文名字nameEng,
                    directorImg = c.導演編號director.導演照片image,
                }
                ).ToList(),
                    //演員List
                    mActorList = _MDAcontext.電影主演casts.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                (c => new CMovieActorViewModel
                {
                    ActorChtName = c.演員編號actor.演員中文名字nameCht,
                    ActorEngName = c.演員編號actor.演員英文名字nameEng,
                    ActorImg = c.演員編號actor.演員照片image,
                    CharacterName = c.角色名字characterName,
                    CharacterTxt = c.角色說明characterIllustrate,
                }
                ).ToList(),
                    //國家List
                    mCountryList = _MDAcontext.電影產地movieOrigins.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                (c => new CCountryImageViewModel
                {
                    countryName = c.國家編號country.國家名稱countryName,
                    countryImage = c.國家編號country.國旗countryImage,
                }
                ).ToList(),
                    //國家名稱
                    //mCountryName = _MDAcontext.電影產地movieOrigins.Where(i => i.電影編號movieId == m.電影編號movieId)
                    //.Select(c => c.國家編號country.國家名稱countryName).ToList(),
                    mImgFrList = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                (c => c.圖片編號image.圖片雲端imageImdb).ToList(),
                    //片種List
                    mTypeList = _MDAcontext.電影片種movieTypes.Where(i => i.電影編號movieId == m.電影編號movieId).Select
                (c => new CMovieTypeViewModel
                {
                    mType = c.片種編號type.片種名稱totalTypeName,
                }
                ).ToList(),
            }).FirstOrDefault();
            return View(datas);
        }

        [HttpPost]
        public IActionResult 電影介紹(CCommentViewModel cVM) //回傳電影簡易評論
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);

            電影評論movieComment c = new 電影評論movieComment()
            {
                電影編號movieId = cVM.電影編號movieId,
                會員編號memberId = mem.會員編號memberId,
                評分rate = cVM.評分rate,
                評論標題commentTitle = cVM.評論標題commentTitle,
                評論內容comments = cVM.評論內容comments,
                發佈時間commentTime = DateTime.Now,
                公開等級編號publicId = 0,
                是否開放討論串oxFloor = true,
                屏蔽invisible = 0,
            };
            _MDAcontext.電影評論movieComments.Add(c);
            _MDAcontext.SaveChanges();
            return RedirectToAction("電影介紹", "Movie", new { id = cVM.電影編號movieId });
        }

        public IActionResult 電影劇照牆(int? id)
        {
            CMovieViewModel datas = null;
            datas = _MDAcontext.電影movies.Where(m => m.電影編號movieId == id).Select
            (m => new CMovieViewModel
            {
                movie = m,
            }).FirstOrDefault();
            return View(datas);
        }

        public IActionResult checkLogin(string page, int? id) //登入後跳轉的頁面
        {
            HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, $"~/Movie/{page}/{id}");
            return Redirect("~/Member/Login");
        }

        public FileResult ShowPhoto(int id) //電影分級img
        {
            電影分級movieRating rating = _MDAcontext.電影分級movieRatings.Find(id);
            byte[] context = rating.分級圖片ratingImage;
            return File(context, "image/jpeg");
        } 
    }
}
