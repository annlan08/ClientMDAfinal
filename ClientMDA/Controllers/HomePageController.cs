using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;
        private readonly MDAContext _MDA;

        public HomePageController(ILogger<HomePageController> logger, MDAContext MDA)
        {
            _logger = logger;
            _MDA = MDA;
        }

        public IActionResult Index()
        {
            return View();
        }

        //問題區
        public IActionResult QandA()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var a = db.一般資訊generaInformations;
            var b = db.會員問題memQas;
            var c = db.評分問題rateQas;
            var d = db.評論問題commentQas;
            var e = db.加入片單addlistQas;
            var f = db.訂票問題orderticketQas;
            var g = db.取票問題taketicketQas;
            var h = db.退票問題refundQas;
            var i = db.優惠券couponQas;
            var j = db.購物問題shopQas;

            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                一般資訊GeneraInformation = a.ToList(),
                會員問題memQa = b.ToList(),
                評分問題rateQa = c.ToList(),
                評論問題commentQa = d.ToList(),
                加入片單addlistQa = e.ToList(),
                訂票問題orderticketQa = f.ToList(),
                取票問題taketicketQa = g.ToList(),
                退票問題refundQa = h.ToList(),
                優惠券couponQa = i.ToList(),
                購物問題shopQa = j.ToList(),



            }).ToList();
            return View(datas);
        }

        public IActionResult GeneraInfor()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var a = db.一般資訊generaInformations;
            datas = _MDA.問題總表questions.Select(p => new CQANDAViewModel
            {
                一般資訊GeneraInformation = a.ToList(),
            }).ToList();

            return ViewComponent("一般資訊", datas);
        }

        public IActionResult memQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var b = db.會員問題memQas;
            datas = _MDA.問題總表questions.Select(p => new CQANDAViewModel
            {
                會員問題memQa = b.ToList(),
            }).ToList();
            return ViewComponent("會員問題", datas);
        }

        public IActionResult rateQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var c = db.評分問題rateQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                評分問題rateQa = c.ToList(),
            }).ToList();
            return ViewComponent("評分問題", datas);
        }

        public IActionResult commentQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var d = db.評論問題commentQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                評論問題commentQa = d.ToList(),
            }).ToList();
            return ViewComponent("評論問題", datas);
        }

        public IActionResult addlistQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var e = db.加入片單addlistQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                加入片單addlistQa = e.ToList(),
            }).ToList();
            return ViewComponent("加入片單", datas);
        }

        public IActionResult orderticketQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var f = db.訂票問題orderticketQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                訂票問題orderticketQa = f.ToList(),
            }).ToList();
            return ViewComponent("訂票問題", datas);
        }

        public IActionResult taketicketQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var g = db.取票問題taketicketQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                取票問題taketicketQa = g.ToList(),
            }).ToList();
            return ViewComponent("取票問題", datas);
        }

        public IActionResult refundQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var h = db.退票問題refundQas;

            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                退票問題refundQa = h.ToList(),
            }).ToList();
            return ViewComponent("退票問題", datas);
        }

        public IActionResult couponQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var i = db.優惠券couponQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                優惠券couponQa = i.ToList(),
            }).ToList();
            return ViewComponent("優惠券", datas);
        }

        public IActionResult shopQa()
        {
            MDAContext db = new MDAContext();
            List<CQANDAViewModel> datas = null;
            var j = db.購物問題shopQas;
            datas = _MDA.一般資訊generaInformations.Select(p => new CQANDAViewModel
            {
                購物問題shopQa = j.ToList(),
            }).ToList();
            return ViewComponent("購物問題", datas);
        }


        [HttpPost]
        public IActionResult Index(CHomepageViewModel p)
        {
            if (p.會員編號Member_ID != 0)
            {
                電影評論movieComment m = new 電影評論movieComment();
                m.會員編號memberId = p.會員編號Member_ID;
                m.電影編號movieId = p.電影編號Movie_ID;
                m.評分rate = p.評分Rate;
                m.發佈時間commentTime = p.發佈時間Comment_Time = DateTime.Now;
                _MDA.電影評論movieComments.Add(m);
                _MDA.SaveChanges();
            }
            else if (p.電影編號Movie_IDbook != 0)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.會員編號memberId = p.會員編號Member_IDbook;
                l.片單總表編號movieListId = p.片單總表編號MovieList_ID;
                l.電影編號movieId = p.電影編號Movie_IDbook;
                _MDA.我的片單myMovieLists.Add(l);
                _MDA.SaveChanges();
            }
            else if (p.我的片單MyMovieList_ID != 0)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.我的片單myMovieListId = p.我的片單MyMovieList_ID;
                _MDA.我的片單myMovieLists.Remove(l);
                _MDA.SaveChanges();
            }
            return View();
        }

        public IActionResult 排行()
        {
            var s = (from a in _MDA.電影圖片movieIimagesLists
                     join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
                     where a.圖片編號image.電影名稱movieName == b.電影movie && a.圖片編號image.圖片類型imageType == "海報"
                     orderby b.排行編號rankId ascending
                     select new CHomePageRankViewModel
                     {
                         圖片雲端ImageIMDB = a.圖片編號image.圖片雲端imageImdb,
                         電影編號Movie_ID = a.電影編號movieId,
                         電影Movie = b.電影movie,
                         電影英Movie_En = a.電影編號movie.英文標題titleEng,
                         電影排名Movie_Rank = b.電影排名movieRank,
                         周末票房BoxOffice_Weekend = b.周末票房boxOfficeWeekend,
                         累積票房BoxOffice_Gross = b.累積票房boxOfficeGross,
                         周次Weeks = b.周次weeks,
                         統計時間 = b.統計時間,
                         評分Rate = a.電影編號movie.評分rate,

                     }).ToList();
            return View(s);
        }

        //public IActionResult 新片排行()
        //{
        //    var s = (from a in _MDA.電影圖片movieIimagesLists
        //             join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
        //             where a.圖片編號image.電影名稱movieName == b.電影movie
        //             orderby b.排行編號rankId ascending
        //             select new CHomePageRankViewModel
        //             {
        //                 圖片雲端ImageIMDB = a.圖片編號image.圖片雲端imageImdb,
        //                 電影編號Movie_ID = a.電影編號movieId,
        //                 電影Movie = b.電影movie,
        //                 電影英Movie_En = a.電影編號movie.英文標題titleEng,
        //                 電影排名Movie_Rank = b.電影排名movieRank

        //             }).ToList();

        //    return ViewComponent("新片排行", s);
        //}

        [HttpPost]
        public IActionResult 排行(CHomePageRankViewModel p)
        {
            if (p.會員編號Member_ID != 0)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.會員編號memberId = p.電影編號Movie_IDbook;
                l.片單總表編號movieListId = p.片單總表編號MovieList_ID;
                l.電影編號movieId = p.電影編號Movie_IDbook;
                _MDA.我的片單myMovieLists.Add(l);
                _MDA.SaveChanges();
            }
            else if (p.我的片單MyMovieList_ID != 0)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.我的片單myMovieListId = p.我的片單MyMovieList_ID;
                _MDA.我的片單myMovieLists.Remove(l);
                _MDA.SaveChanges();
            }
            return View();
        }

        public IActionResult 本周()
        {
            var d = (from a in _MDA.電影movies
                     join b in _MDA.電影圖片總表movieImages on a.中文標題titleCht equals b.電影名稱movieName
                     where a.上映日期releaseDate >= DateTime.Now.AddDays(-7) && a.上映日期releaseDate <= DateTime.Now.AddDays(7) &&b.圖片類型imageType=="海報"
                     orderby a.上映日期releaseDate
                     select new CHomePageNewViewModel
                     {
                         電影編號Movie_ID = a.電影編號movieId,
                         中文標題Title_Cht = a.中文標題titleCht,
                         電影英Movie_En = a.英文標題titleEng,
                         期待度anticipation = a.期待度anticipation,
                         片長Runtime = a.片長runtime,
                         上映日期Release_Date = a.上映日期releaseDate,
                         圖片雲端ImageIMDB = b.圖片雲端imageImdb
                     }).ToList();
            return View(d);
        }

        //public IActionResult 本周新片()
        //{
        //    var s = (from a in _MDA.電影圖片movieIimagesLists
        //             join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
        //             where a.圖片編號image.電影名稱movieName == b.電影movie
        //             orderby b.排行編號rankId ascending
        //             select new CHomePageRankViewModel
        //             {
        //                 圖片雲端ImageIMDB = a.圖片編號image.圖片雲端imageImdb,
        //                 電影編號Movie_ID = a.電影編號movieId,
        //                 電影Movie = b.電影movie,
        //                 電影英Movie_En = a.電影編號movie.英文標題titleEng,
        //                 電影排名Movie_Rank = b.電影排名movieRank

        //             }).ToList();

        //    return ViewComponent("本周新片", s);
        //}

        public IActionResult 期待()
        {
            var d = (from a in _MDA.電影movies
                     join b in _MDA.電影圖片總表movieImages on a.中文標題titleCht equals b.電影名稱movieName
                     where (a.上映日期releaseDate >= DateTime.Now.AddDays(30) && a.期待度anticipation != null &&b.圖片類型imageType=="海報")

                     orderby a.期待度anticipation descending

                     select new CHomePageExpectViewModel
                     {
                         電影編號Movie_ID = a.電影編號movieId,
                         中文標題Title_Cht = a.中文標題titleCht,
                         電影英Movie_En = a.英文標題titleEng,
                         期待度anticipation = a.期待度anticipation,
                         上映日期Release_Date = a.上映日期releaseDate,
                         圖片雲端ImageIMDB = b.圖片雲端imageImdb
                     }).ToList();
            return View(d);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult test3()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult test()
        {
            return View();
        }

    }
}
