using ClientMDA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace ClientMDA.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly MDAContext _MDA;

        Random rd = new Random();

        public ApiController(ILogger<ApiController> logger, MDAContext MDA)
        {
            _logger = logger;
            _MDA = MDA;
            _MDA.電影圖片movieIimagesLists.ToList();
            _MDA.電影圖片總表movieImages.ToList();
            _MDA.電影movies.ToList();
            _MDA.我的片單myMovieLists.ToList();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getmemID()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(m => m.會員編號memberId);
                if (q != null)
                {
                    return Json(q);
                }
            }
            return Json(0);
        }

        public IActionResult getmemlist()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var q2 = _MDA.片單總表movieLists.Where(m => m.會員編號memberId == q && m.片單總表名稱listName == "我的片單(預設)").Select(p => p.片單總表編號movieListId);
                return Json(q2);
            }
            return Json(0);
        }

        //尚未設定資料
        public IActionResult showbannernum()
        {
            var q = from a in _MDA.電影圖片movieIimagesLists
                    join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
                    where a.圖片編號image.電影名稱movieName == b.電影movie
                    orderby b.排行編號rankId ascending
                    select a.電影編號movie.電影編號movieId;

            return Json(q);
        }

        public IActionResult checklogin()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (user == null)
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "~/HomePage/Index");
                return Redirect("~/Member/Login");
            }
            return View();
        }

        public IActionResult checklogin2()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (user == null)
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "~/HomePage/排行");
                return Redirect("~/Member/Login");
            }
            return View();
        }

        public IActionResult checklogin3()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (user == null)
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "~/HomePage/本周");
                return Redirect("~/Member/Login");
            }
            return View();
        }

        public IActionResult checklogin4()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (user == null)
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "~/HomePage/期待");
                return Redirect("~/Member/Login");
            }
            return View();
        }

        public IActionResult showrankposter()
        {
            var q = (from a in _MDA.電影圖片movieIimagesLists
                     join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
                     where a.圖片編號image.電影名稱movieName == b.電影movie && a.圖片編號image.圖片類型imageType == "海報"
                     orderby b.排行編號rankId ascending
                     select a.圖片編號image.圖片雲端imageImdb).ToList();
            //var q1 = (from a in _MDA.電影圖片總表movieImages
            //         join b in _MDA.電影排行movieRanks on a.電影名稱movieName equals b.電影movie
            //         where a.電影名稱movieName == b.電影movie
            //         orderby b.電影排名movieRank
            //         select a.圖片雲端imageImdb).ToList();

            string o = "0";
            //q1.RemoveAll();
            for (int i = 0; i < q.Count(); i++)
            {
                if (q[i] == "0")
                {
                    q.Remove(q[i]);
                }
            }

            return Json(q);
        }

        public IActionResult showranknum()
        {
            var q = from a in _MDA.電影圖片movieIimagesLists
                    join b in _MDA.電影排行movieRanks on a.圖片編號image.電影名稱movieName equals b.電影movie
                    where a.圖片編號image.電影名稱movieName == b.電影movie
                    orderby b.排行編號rankId ascending
                    select a.電影編號movie.電影編號movieId;
            return Json(q);
        }

        public IActionResult ranknumlist()
        {
            List<int> list = new List<int>();
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var c = _MDA.片單總表movieLists.Where(p => p.片單總表名稱listName == "我的片單(預設)" && p.會員編號memberId == q).Select(o => o.片單總表編號movieListId).FirstOrDefault(); ;
                var q3 = _MDA.我的片單myMovieLists.AsEnumerable().Where(m => m.會員編號memberId == q && m.片單總表編號movieListId == c).OrderBy(i => i.電影編號movieId).Select(m => m.我的片單myMovieListId);

                return Json(q3);
            }
            return Json(0);
        }

        public IActionResult showrankmovie()
        {
            var q1 = from a in _MDA.電影排行movieRanks
                     where a.電影排名movieRank != null
                     select a.電影movie;
            return Json(q1);
        }

        public IActionResult shownewmovie()
        {
            var q2 = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7))).OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(v => v.電影編號movie.中文標題titleCht);
            return Json(q2);
        }

        public IActionResult shownewposter()
        {
            var q = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7)) && m.圖片編號image.圖片類型imageType == "海報").OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(u => u.圖片編號image.圖片雲端imageImdb);
            return Json(q);
        }

        public IActionResult shownewnum()
        {
            var q2 = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7))).OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(v => v.電影編號movie.電影編號movieId);
            return Json(q2);
        }

        public IActionResult showrecommendposter()
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<int> listNumbers = new List<int>();
            int number;
            for (int i = 0; i < 20; i++)
            {
                do
                {
                    //資料庫要新增很多東西才可以跳更多推薦
                    number = rd.Next(1, 100);
                } while (listNumbers.Contains(number));
                listNumbers.Add(number);
            }
            for (int i = 0; i < listNumbers.Count(); i++)
            {
                var q3 = _MDA.電影圖片movieIimagesLists.AsEnumerable().Where(p => p.電影編號movieId == listNumbers[i]).Select(o => o.圖片編號image.圖片雲端imageImdb).FirstOrDefault();
                list.Add(q3);
            }
            for (int i = 0; i < listNumbers.Count(); i++)
            {
                var q4 = _MDA.電影圖片movieIimagesLists.AsEnumerable().Where(o => o.電影編號movieId == listNumbers[i]).Select(p => p.電影編號movie.電影編號movieId).FirstOrDefault().ToString();
                list.Add(q4);
            }
            return Json(list);
        }

        public IActionResult showrecnum()
        {
            var q2 = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7))).OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(v => v.電影編號movie.電影編號movieId);
            return Json(q2);
        }

        //抓片單電影編號
        public IActionResult bookmarkget()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                string x = "我的片單(預設)";
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var q2 = _MDA.我的片單myMovieLists.Where(m => m.會員編號memberId == q && m.片單總表編號movieList.片單總表名稱listName == x).Select(m => m.電影編號movieId).Distinct();
                return Json(q2);
            }
            return Json(0);
        }

        public IActionResult showlistmovie()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                string x = "我的片單(預設)";
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var f = _MDA.我的片單myMovieLists.Where(i => i.會員編號memberId == q && i.片單總表編號movieList.片單總表名稱listName == x).Distinct().OrderBy(e => e.我的片單myMovieListId).Select(w => w.電影編號movie.中文標題titleCht);
                return Json(f);
            }
            return Json(0);
        }

        public IActionResult showlistmovienem()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                string x = "我的片單(預設)";
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var f = _MDA.我的片單myMovieLists.Where(i => i.會員編號memberId == q && i.片單總表編號movieList.片單總表名稱listName == x).Distinct().OrderBy(e => e.我的片單myMovieListId).Select(w => w.電影編號movieId);

                return Json(f);
            }
            return Json(0);
        }

        public IActionResult showlistposter()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                List<string> list = new List<string>();
                string x = "我的片單(預設)";
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var f = _MDA.我的片單myMovieLists.Where(i => i.會員編號memberId == q && i.片單總表編號movieList.片單總表名稱listName == x).Distinct().OrderBy(e => e.我的片單myMovieListId).Select(w => w.電影編號movieId).ToArray();
                for (var i = 0; i < f.Count(); i++)
                {
                    var q3 = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movieId == f[i] && m.圖片編號image.電影圖片movieIimagesLists != null && m.圖片編號image.圖片類型imageType == "海報").Select(p => p.圖片編號image.圖片雲端imageImdb).FirstOrDefault();
                    list.Add(q3);
                }
                return Json(list);
            }
            return Json(0);
        }

        public IActionResult getlistnumber()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                List<string> list = new List<string>();
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var q2 = _MDA.我的片單myMovieLists.Where(m => m.會員編號memberId == q).Select(m => m.電影編號movieId).ToArray();
                for (var i = 0; i < q2.Count(); i++)
                {
                    var q3 = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movieId == q2[i]).FirstOrDefault().圖片編號image.圖片雲端imageImdb;
                    list.Add(q3);
                }
                return Json(list);
            }
            return Json(0);
        }

        public IActionResult getliststar()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                List<string> list = new List<string>();
                string x = "我的片單(預設)";
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var f = _MDA.我的片單myMovieLists.Where(i => i.會員編號memberId == q && i.片單總表編號movieList.片單總表名稱listName == x).Distinct().OrderBy(e => e.我的片單myMovieListId).Select(w => w.電影編號movie.評分rate);
                return Json(f);
            }
            return Json(0);
        }

        public IActionResult getrankex()
        {
            var d = from a in _MDA.電影movies
                    join b in _MDA.電影排行movieRanks on a.中文標題titleCht equals b.電影movie
                    orderby b.排行編號rankId ascending
                    select a.期待度anticipation;
            //var reate = this._MDA.電影排行movieRanks.Select(m=>m.電影movie).ToList();
            //var movie = this._MDA.電影movies.ToList();
            //List<decimal> list = new List<decimal>();
            //List<string> namelist = new List<string>();
            //foreach(var item in movie)
            //{
            //    if (reate.Contains(item.中文標題titleCht))
            //    {
            //        namelist.Add(item.中文標題titleCht);
            //        list.Add((decimal)item.期待度anticipation);
            //    }
            //}

            var s = d.ToList();
            return Json(d);
        }

        public IActionResult getrankac()
        {
            var d = from a in _MDA.電影movies
                    join b in _MDA.電影排行movieRanks on a.中文標題titleCht equals b.電影movie

                    orderby b.排行編號rankId ascending
                    select a.評分rate;
            return Json(d);
        }

        public IActionResult getnewex()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7))).OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(v => v.電影編號movie.期待度anticipation);
            return Json(d);
        }

        public IActionResult getnewac()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(-7)) && m.電影編號movie.上映日期releaseDate <= (DateTime.Now.AddDays(7))).OrderBy(d => d.電影編號movie.上映日期releaseDate).Select(v => v.電影編號movie.評分rate);
            return Json(d);
        }

        public IActionResult showexrankmovie()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(30))).OrderByDescending(d => d.電影編號movie.期待度anticipation).Select(v => v.電影編號movie.中文標題titleCht);
            return Json(d);
        }

        public IActionResult showexrankposter()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(30)) && m.圖片編號image.圖片類型imageType == "海報").OrderByDescending(d => d.電影編號movie.期待度anticipation).Select(v => v.圖片編號image.圖片雲端imageImdb);
            return Json(d);
        }

        public IActionResult showexrankmovienuber()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(30))).OrderByDescending(d => d.電影編號movie.期待度anticipation).Select(v => v.電影編號movie.電影編號movieId);
            return Json(d);
        }

        public IActionResult getreeex()
        {
            var d = _MDA.電影圖片movieIimagesLists.Where(m => m.電影編號movie.上映日期releaseDate >= (DateTime.Now.AddDays(30))).OrderByDescending(d => d.電影編號movie.期待度anticipation).Select(v => v.電影編號movie.期待度anticipation);
            return Json(d);
        }

        //加入評分資料
        public IActionResult RateSubmit(int Member_ID, int Movie_ID, int Rate)
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                電影評論movieComment m = new 電影評論movieComment();
                m.會員編號memberId = Member_ID;
                m.電影編號movieId = Movie_ID;
                m.評分rate = Rate;
                m.發佈時間commentTime = DateTime.Now;
                _MDA.電影評論movieComments.Add(m);
                _MDA.SaveChanges();

                //要改用評論的評分 目前資料太少無法用平均
                var q = _MDA.電影movies.Where(p => p.電影編號movieId == Movie_ID).FirstOrDefault();
                decimal y = decimal.Round(((Convert.ToDecimal(q.評分rate) + Convert.ToDecimal(Rate)) / 2), 1, MidpointRounding.ToEven);
                q.評分rate = y;
                _MDA.SaveChanges();
                return Content("1", "text/plain");
            }
            return Content("0", "text/plain");
        }

        //找評論ID
        public IActionResult getComment()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var d = _MDA.電影評論movieComments.Where(o => o.會員編號memberId == q).OrderBy(p => p.評論編號commentId).Select(p => p.評論編號commentId);

                return Json(d);
            }
            return Json(0);
        }
        //找評論電影編號
        public IActionResult getCommentMovie()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var d = _MDA.電影評論movieComments.Where(o => o.會員編號memberId == q).OrderBy(p => p.評論編號commentId).Select(p => p.電影編號movieId);

                return Json(d);
            }
            return Json(0);
        }
        //找評論電影編號
        public IActionResult getCommenRank()
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.會員members.Where(m => m.會員電話cellphone == a).Select(p => p.會員編號memberId).FirstOrDefault();
                var d = _MDA.電影評論movieComments.Where(o => o.會員編號memberId == q).OrderBy(p => p.評論編號commentId).Select(p => p.評分rate);

                return Json(d);
            }
            return Json(0);
        }

        //刪除評分資料
        public IActionResult RateDelete(int Comment_ID, int Rate, int Movie_ID)
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.電影評論movieComments.FirstOrDefault(t => t.評論編號commentId == Comment_ID);
                _MDA.電影評論movieComments.Remove(q);
                _MDA.SaveChanges();
                var q2 = _MDA.電影movies.Where(p => p.電影編號movieId == Movie_ID).FirstOrDefault();
                decimal y = decimal.Round(((Convert.ToDecimal(q2.評分rate) * 2) - Rate), 1, MidpointRounding.ToEven);
                q2.評分rate = y;
                _MDA.SaveChanges();
                return Content("1", "text/plain");
            }
            return Content("0", "text/plain");
        }

        //編輯評分資料
        public IActionResult RateEdit(int Comment_ID, int Rate, int Movie_ID)
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                var q = _MDA.電影評論movieComments.FirstOrDefault(t => t.評論編號commentId == Comment_ID);
                var q2 = _MDA.電影movies.Where(p => p.電影編號movieId == Movie_ID).FirstOrDefault();
                decimal y = decimal.Round((((Convert.ToDecimal(q2.評分rate) * 2) - Convert.ToDecimal(q.評分rate) + Rate) / 2), 1, MidpointRounding.ToEven); ;
                q.評分rate = Rate;
                _MDA.電影評論movieComments.Update(q);
                _MDA.SaveChanges();
                q2.評分rate = y;
                _MDA.SaveChanges();
                return Content("1", "text/plain");
            }
            return Content("0", "text/plain");
        }

        public IActionResult bookSubmit(int memberid, int movieid, int movielistid)
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.會員編號memberId = memberid;
                l.片單總表編號movieListId = movielistid;
                l.電影編號movieId = movieid;
                _MDA.我的片單myMovieLists.Add(l);
                _MDA.SaveChanges();
                return Content("1", "text/plain");
            }
            return Content("0", "text/plain");
        }

        //不能用
        public IActionResult bookdelete(int listid)
        {
            string a = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            if (a != null)
            {
                我的片單myMovieList l = new 我的片單myMovieList();
                l.我的片單myMovieListId = listid;
                _MDA.我的片單myMovieLists.Remove(l);
                _MDA.SaveChanges();
                return Content("1", "text/plain");
            }
            return Content("0", "text/plain");
        }
    }
}
