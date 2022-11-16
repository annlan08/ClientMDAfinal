using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.Controllers
{
    public class TheaterController : Controller
    {
        private readonly MDAContext _MDA;
        public TheaterController(MDAContext MDA)
        {
            _MDA = MDA;
            _MDA.電影圖片movieIimagesLists.ToList();
            _MDA.電影圖片總表movieImages.ToList();
        }
        public IActionResult hot()
        {
            List<CTheater> datas = null;
            datas = _MDA.電影movies.OrderByDescending(p => p.票房boxOffice).Select
            (p => new CTheater
            {
                movie = p,
                分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                .Select(c => c.圖片編號image.圖片image).ToList(),
                會員評分 = p.電影評論movieComments.Select(x => x.評分rate).ToList()
            }).ToList();
            return ViewComponent("現正熱映", datas);
        }
        public IActionResult 測試業面()
        {
            List<CTheater> datas = null;
            var q = _MDA.電影圖片movieIimagesLists.Select(i => i);
            datas = _MDA.電影movies.OrderByDescending(p => p.票房boxOffice).Select
            (p => new CTheater
            {
                movie = p,
                分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                .Select(c => c.圖片編號image.圖片image).ToList(),
                會員評分 = p.電影評論movieComments.Select(x => x.評分rate).ToList()


            }).ToList();
         
            return View(datas);
        } //完工
        public IActionResult now()
        {
            List<CTheater> datas = null;
            List<int> img = _MDA.電影圖片movieIimagesLists.Select(p => p.電影圖片編號miId).ToList();
            datas = _MDA.電影movies.OrderByDescending(p => p.上映日期releaseDate).Select
                      (p => new CTheater
                      {
                          movie = p,
                          分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                          MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList()

                      }).ToList();
            return ViewComponent("即將上映",datas);
        } //完工
        public IActionResult 快定頁面測試(int id)
        {
            HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, $"~/Theater/快定頁面測試/{id}");
            CTheater datas = null;
            datas = _MDA.電影movies.Where(p => p.電影編號movieId == id).Select
                (p => new CTheater
                {
                    movie = p,
                    分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                    MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList()

                }).FirstOrDefault();

            return View(datas);
           
        } //完工
        public IActionResult nowpage(int num)
        {
            int i = 12, j = 0;
           List<CTheater> datas = null;
            if (num == 1)
            {
                datas = _MDA.電影movies.OrderByDescending(p => p.上映日期releaseDate).Select
                (p => new CTheater
                {
                    movie = p,
                    分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                    MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList()

                }).ToList();
                return ViewComponent("即將上映", datas);
            }
            else if (num >= 2) {
                j = 12;
                i = i * num;
            }
                datas = _MDA.電影movies.OrderByDescending(p => p.上映日期releaseDate).Select
                (p => new CTheater
                {
                    movie = p,
                    分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                    MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList()

                }).Skip(i - j).Take(i).ToList();
                return ViewComponent("即將上映", datas);
        } //完工
        public IActionResult hotpage(int num)
        {
            int i = 12, j = 0;
            List<CTheater> datas = null;
            if (num == 1)
            {
                datas = _MDA.電影movies.OrderByDescending(p => p.票房boxOffice).Select
                (p => new CTheater
                {
                    movie = p,
                    分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                    MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList(),
                    會員評分 = p.電影評論movieComments.Select(x => x.評分rate).ToList()

                }).ToList();
                return ViewComponent("現正熱映", datas);
            }
            else if (num >= 2)
            {
                j = 12;
                i = i * num;
            }
            datas = _MDA.電影movies.OrderByDescending(p => p.票房boxOffice).Select
            (p => new CTheater
            {
                movie = p,
                分級級數ratingLevel = p.電影分級編號rating.分級級數ratingLevel,
                MPimg = _MDA.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == p.電影編號movieId)
                                  .Select(c => c.圖片編號image.圖片image).ToList(),
                會員評分 = p.電影評論movieComments.Select(x => x.評分rate).ToList()

            }).Skip(i - j).Take(i).ToList();
            return ViewComponent("現正熱映", datas);
        }
        public IActionResult page()
        {

            return ViewComponent("頁數");
        } //完工
        public IActionResult area(string keyword,int id ,int time)
        {
            List<CTheater> datas = null;
            
            string t1 = time.ToString();
            t1 = t1.Insert(4, "-");
            t1 = t1.Insert(7, "-");
            DateTime t2 = Convert.ToDateTime(t1).Date;
            int mcid = this._MDA.電影代碼movieCodes.Where(p => p.電影編號movieId == id).Select(x => x.電影代碼編號movieCodeId).FirstOrDefault();
            DateTime timer = _MDA.場次screenings.Where(p => p.放映日期playDate.Date == t2).Select(x => x.放映日期playDate).FirstOrDefault();
            if (String.IsNullOrEmpty(keyword))
            { return View(); }
            if (keyword == "北區")
            {
                datas = _MDA.電影院theaters.Where(p => (p.地址address.Contains("基隆") ||
      p.地址address.Contains("北市") || p.地址address.Contains("台東") ||
      p.地址address.Contains("花蓮"))).Select
    (i => new CTheater
    {
        theater = i,
        cinemas影廳種類 = _MDA.影廳cinemas.Where(c => c.電影院編號theaterId == i.電影院編號theaterId).Select(c => new CcinemaViewMode
        {
            CinemaID = c.影廳編號cinemaId,
            CinemaName = c.廳種名稱cinemaClsName,
            放映時間 = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.放映開始時間playTime).ToList(),
            場次ID = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.場次編號screeningId).ToList(),
            場次座位數量 = _MDA.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => new CSeatViewMode
            {
                座位數量 = s.出售座位狀態seatStatuses.Select(o => o.出售座位資訊seatSoldInfo).FirstOrDefault()

            }).ToList()
        }).ToList(),

    }).ToList();
            }
            else if (keyword == "桃竹苗")
            {
                datas = _MDA.電影院theaters.Where(p => p.地址address.Contains("桃") ||
                p.地址address.Contains("苗") || p.地址address.Contains("竹")
                ).Select
                (i => new CTheater
                {
                    theater = i,
                    cinemas影廳種類 = _MDA.影廳cinemas.Where(c => c.電影院編號theaterId == i.電影院編號theaterId).Select(c => new CcinemaViewMode
                    {
                        CinemaID = c.影廳編號cinemaId,
                        CinemaName = c.廳種名稱cinemaClsName,
                        放映時間 = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.放映開始時間playTime).ToList(),
                        場次ID = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.場次編號screeningId).ToList(),

                    }).ToList(),
                    //seat座位 =fn_計算座位數( _MDA.出售座位狀態seatStatuses.Where(c => c.場次編號screening.影廳編號cinema.電影院編號theaterId == i.電影院編號theaterId).Select(c => c.出售座位資訊seatSoldInfo).FirstOrDefault()),
                }).ToList();
            }
            else if (keyword == "中區")
            {
                datas = _MDA.電影院theaters.Where(p => p.地址address.Contains("台中") ||
                p.地址address.Contains("雲林")
                ).Select
                (i => new CTheater
                {
                    theater = i,
                    cinemas影廳種類 = _MDA.影廳cinemas.Where(c => c.電影院編號theaterId == i.電影院編號theaterId).Select(c => new CcinemaViewMode
                    {
                        CinemaID = c.影廳編號cinemaId,
                        CinemaName = c.廳種名稱cinemaClsName,
                        放映時間 = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.放映開始時間playTime).ToList(),
                        場次ID = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.場次編號screeningId).ToList(),
                    }).ToList(),
                    //seat座位 =  _MDA.出售座位狀態seatStatuses.Where(c => c.場次編號screening.影廳編號cinema.電影院編號theaterId == i.電影院編號theaterId).Select(c => c.出售座位資訊seatSoldInfo).FirstOrDefault(),

                }).ToList();
            }
            else if (keyword == "南區及離島")
            {
                datas = _MDA.電影院theaters.Where(p => p.地址address.Contains("高雄") ||
                p.地址address.Contains("嘉義") || p.地址address.Contains("台南")
                ).Select
                (i => new CTheater
                {
                    theater = i,
                    cinemas影廳種類 = _MDA.影廳cinemas.Where(c => c.電影院編號theaterId == i.電影院編號theaterId).Select(c => new CcinemaViewMode
                    {
                        CinemaID = c.影廳編號cinemaId,
                        CinemaName = c.廳種名稱cinemaClsName,
                        放映時間 = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.放映開始時間playTime).ToList(),
                        場次ID = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.場次編號screeningId).ToList(),

                    }).ToList(),
                    //seat座位 = _MDA.出售座位狀態seatStatuses.Where(c => c.場次編號screening.影廳編號cinema.電影院編號theaterId == i.電影院編號theaterId).Select(c => c.出售座位資訊seatSoldInfo).FirstOrDefault(),
                }).ToList();
            }
            else
            {
                datas = _MDA.電影院theaters.Select
            (i => new CTheater
            {
             theater = i,
             cinemas影廳種類 = _MDA.影廳cinemas.Where(c => c.電影院編號theaterId == i.電影院編號theaterId).Select(c => new CcinemaViewMode
             {
                 CinemaID = c.影廳編號cinemaId,
                 CinemaName = c.廳種名稱cinemaClsName,
                 放映時間 = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.放映開始時間playTime).ToList(),
                 場次ID = c.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => s.場次編號screeningId).ToList(),

             }).ToList(),
                                //seat座位 = _MDA.出售座位狀態seatStatuses.Where(c => c.場次編號screening.影廳編號cinema.電影院編號theaterId == i.電影院編號theaterId).Select(c => c.出售座位資訊seatSoldInfo).FirstOrDefault(),
                            }).ToList();
                        }

            return ViewComponent("時間地區",datas);
        } //完工

        public int fn_計算座位數(string seatInfo) { 
            string[] seatArr = seatInfo.Split('@');
            int count = 0;
            foreach (string item in seatArr) { 
                if (!(item.Contains("NA")) && !(item.Contains("saled")) && !(string.IsNullOrWhiteSpace(item)))
                    count++; 
            } 
            return count;
        }
        public IActionResult Seat(string seat) {
            int x =0;
            x=fn_計算座位數(seat);
        
          return ViewComponent("時間",x);
        }
    }
}


//var data = _MDA.出售座位狀態seatStatuses.Where(p => p.場次編號screening.影廳編號cinema.電影院編號theaterId == id).Select(p => p.出售座位資訊seatSoldInfo).ToList();
//var cinema123 = _MDA.影廳cinemas.ToList();
//var seatStatuses123 = _MDA.出售座位狀態seatStatuses.AsEnumerable();

//var datas1 = _MDA.電影院theaters.Where(p => (p.地址address.Contains("基隆") ||
//p.地址address.Contains("北市") || p.地址address.Contains("台東") ||
//p.地址address.Contains("花蓮")) && p.影廳cinemas.Where(o=>o.場次screenings.Where(a=>a.電影代碼movieCodeNavigation.電影編號movieId == id).Any() == true).Any()
//).Select
//    (i => i.影廳cinemas.FirstOrDefault().場次screenings.FirstOrDefault().出售座位狀態seatStatuses.FirstOrDefault().出售座位資訊seatSoldInfo).ToList();
//var aasd = fn_計算座位數(datas1[0]);