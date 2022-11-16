using ClientMDA.Models;
using ClientMDA.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace ClientMDA.Controllers
{
    public class TicketingController : Controller
    {
        #region 建構子
        private MDAContext _dbContext;
        private readonly IWebHostEnvironment _host;

        public TicketingController(MDAContext dbContext, IWebHostEnvironment host)
        {
            _dbContext = dbContext;
            _host = host;
            _dbContext.場次screenings.ToList();
            _dbContext.電影movies.ToList();
            _dbContext.電影代碼movieCodes.ToList();
            _dbContext.電影院theaters.ToList();
            _dbContext.影廳cinemas.ToList();
            _dbContext.出售座位狀態seatStatuses.ToList();
            _dbContext.影城mainTheaters.ToList();
            _dbContext.票價資訊ticketPrices.ToList();
            _dbContext.票種ticketTypes.ToList();
            _dbContext.電影分級movieRatings.ToList();
            _dbContext.訂單總表orders.ToList();
            _dbContext.電影語言movieLanguages.ToList();
            _dbContext.電影主演casts.ToList();
            _dbContext.導演總表directors.ToList();
            _dbContext.電影導演movieDirectors.ToList();
            _dbContext.演員總表actors.ToList();
            _dbContext.會員members.ToList();
            _dbContext.電影圖片movieIimagesLists.ToList();
            _dbContext.電影圖片總表movieImages.ToList();
            _dbContext.優惠明細couponLists.ToList();
            _dbContext.優惠總表coupons.ToList();

        }
        #endregion

        #region 普通Action
        public IActionResult MovieInfoIndex()
        {
            return View();

        }

        public IActionResult SelectMovie()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (string.IsNullOrEmpty(user))
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "~/Ticketing/SelectMovie");
                return Redirect("~/Member/Login");
            }
            return View();
        }

        public IActionResult TheaterPartialView()
        {
            List<電影院theater> theaters = this._dbContext.電影院theaters.ToList();
            return PartialView($"~/Views/Ticketing/_TheaterPartialView.cshtml", theaters);
        }

        public IActionResult MoviePartialView()
        {
            List<電影代碼movieCode> movies = this._dbContext.訂單總表orders.AsEnumerable().GroupBy(o => o.場次編號screening.電影代碼movieCodeNavigation)
                      .OrderByDescending(o => o.Select(t => t.訂單明細orderDetails.Select(d => d.張數count).Sum()).Sum())
                      .Select(o => o.Key).ToList();
            foreach (var item in this._dbContext.電影代碼movieCodes)
            {
                if (!movies.Contains(item))
                    movies.Add(item);
            }
            string picurls = "";
            for (int i = 0; i < 9; i++)
            {
                int movieid = this._dbContext.電影代碼movieCodes.Where(m => m.電影編號movieId == movies[i].電影編號movieId).Select(m => m.電影編號movieId).FirstOrDefault();
                picurls += this._dbContext.電影圖片movieIimagesLists.Where(p => p.電影編號movieId == movieid).Select(p => p.圖片編號image.圖片image).FirstOrDefault() + '#';
            }
            ViewBag.str = picurls;
            return PartialView($"~/Views/Ticketing/_MoviePartialView.cshtml", movies);
        }

        public IActionResult MovieInfoIndex2(int id)
        {
            HttpContext.Session.SetInt32(CDictionary.SK_選擇的電影Code, id);
            List<電影院theater> theater = this._dbContext.場次screenings.Where(s => s.電影代碼movieCode == id && s.放映日期playDate > DateTime.Now.AddDays(1)).Select(s => s.影廳編號cinema.電影院編號theater).Distinct().ToList();
            return View(theater);
        }

        public IActionResult SelectByMovie(int theaterID)
        {
            HttpContext.Session.SetInt32(CDictionary.SK_選擇的電影院ID, theaterID);
            int MovieID = (int)HttpContext.Session.GetInt32(CDictionary.SK_選擇的電影Code);
            CSelectByMovieViewModel view = new CSelectByMovieViewModel()
            {
                movie = this._dbContext.電影代碼movieCodes.FirstOrDefault(c => c.電影代碼編號movieCodeId == MovieID),
                theater = this._dbContext.電影院theaters.FirstOrDefault(t => t.電影院編號theaterId == theaterID),
                Delectors = this._dbContext.電影代碼movieCodes.FirstOrDefault(c => c.電影代碼編號movieCodeId == MovieID).電影編號movie.電影導演movieDirectors.Select(d => d.導演編號director.導演中文名字nameCht).ToList(),
                Actors = this._dbContext.電影代碼movieCodes.FirstOrDefault(c => c.電影代碼編號movieCodeId == MovieID).電影編號movie.電影主演casts.Select(a => a.演員編號actor.演員中文名字nameCht).ToList(),
            };
            return View(view);
        }

        public IActionResult SelectByTheater(int theaterID)
        {
            HttpContext.Session.SetInt32(CDictionary.SK_選擇的電影院ID, theaterID);

            電影院theater theater = this._dbContext.電影院theaters.FirstOrDefault(t => t.電影院編號theaterId == theaterID);
            List<場次screening> AllScreening = this._dbContext.場次screenings
                                                   .Where(s => s.影廳編號cinema.電影院編號theaterId == theaterID)
                                                   .ToList();

            CSelectByTheaterViewModel view = new CSelectByTheaterViewModel()
            {
                theaterName = theater.電影院名稱theaterName,
                address = theater.地址address,
                phone = theater.電話phone,
                theaterId = theaterID,
                Allscreen = AllScreening
            };
            return View(view);
        }

        public IActionResult SeatMap(CScreenIDAndCountViewModel info)
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (string.IsNullOrEmpty(user))
            {
                return Redirect("~/Member/Login");
            }


            出售座位狀態seatStatus seatStaus = this._dbContext.出售座位狀態seatStatuses.Where(ss => ss.場次編號screeningId == info.ScreenID).FirstOrDefault();
            場次screening screening = this._dbContext.場次screenings.Where(s => s.場次編號screeningId == info.ScreenID).FirstOrDefault();
            CSeatMaoViewModels seatview = new CSeatMaoViewModels(seatStaus);
            seatview.seatCount選擇座位數量 = info.Count;
            seatview.MovieName電影名稱 = screening.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht;
            seatview.MovieID電影編號 = screening.電影代碼movieCodeNavigation.電影編號movieId;
            seatview.MovieCode電影代碼 = screening.電影代碼movieCode;
            seatview.TheaterName影城名稱 = screening.影廳編號cinema.電影院編號theater.電影院名稱theaterName;
            seatview.TheaterID影城編號 = screening.影廳編號cinema.電影院編號theaterId;
            seatview.Date日期 = screening.放映日期playDate.ToString("MMMM dd");
            seatview.fileVersion電影版本 = screening.影廳編號cinema.廳種名稱cinemaClsName;
            seatview.StartTime開始時間 = screening.放映開始時間playTime;

            return View(seatview);
        }

        public IActionResult SelectTicket(CGetInfoForTicketActionViewModel Info)
        {
            //判斷時間
            string screentime = this._dbContext.場次screenings.Where(s => s.場次編號screeningId == Info.screenID).FirstOrDefault().放映開始時間playTime;

            string[] strarr = screentime.Split(':');
            int starthour = Convert.ToInt32(strarr[0]);

            影城mainTheater currentMainTheater = this._dbContext.電影院theaters.Where(t => t.電影院編號theaterId == Info.theaterID).Select(t => t.影城編號mainTheater).FirstOrDefault();
            CSelectTicketViewModel ticketview = new CSelectTicketViewModel(Info);
            List<CTicketInfoViewModelcs> ALLticketInfo = new List<CTicketInfoViewModelcs>();
            var q = this._dbContext.票價資訊ticketPrices.Where(tp => tp.影城編號mainTheaterId == currentMainTheater.影城編號mainTheaterId);
            foreach (var item in q)
            {
                CTicketInfoViewModelcs ticketInfoitem = new CTicketInfoViewModelcs();
                ticketInfoitem.TicketID票價明細 = item.票價明細ticketId;
                ticketInfoitem.TicketName票種名稱 = item.票種編號ticketType.票種名稱ticketTypeName;
                ticketInfoitem.TicketPrice票價 = item.價格ticketPrice;
                if (starthour < 9 && starthour > 5)
                {
                    if (ticketInfoitem.TicketName票種名稱.Contains('早'))
                        ALLticketInfo.Add(ticketInfoitem);
                }
                else if (starthour > 22 || starthour < 5)
                {
                    if (ticketInfoitem.TicketName票種名稱.Contains('夜') || ticketInfoitem.TicketName票種名稱.Contains('晚'))
                        ALLticketInfo.Add(ticketInfoitem);
                }
                else
                {
                    if (!ticketInfoitem.TicketName票種名稱.Contains('夜') && !ticketInfoitem.TicketName票種名稱.Contains('晚') && !ticketInfoitem.TicketName票種名稱.Contains('早'))
                        ALLticketInfo.Add(ticketInfoitem);
                }

            }
            ticketview.ALLticketInfo = ALLticketInfo;
            return View(ticketview);
        }

        public IActionResult PaymentWeb(CPaymentweb1ViewModel payment1) // 回傳字串 id:count#id:count#  id=票價明細 count=數量
        {
            HttpContext.Session.SetInt32(CDictionary.SK_當前手續費, payment1.count * 20);
            CPaymentAndMovieInfoViewModel payview = new CPaymentAndMovieInfoViewModel(payment1);
            payview.theaterName電影院名稱 = this._dbContext.電影院theaters.FirstOrDefault(t => t.電影院編號theaterId == payment1.theaterID).電影院名稱theaterName;
            payview.Movieimage電影照片 = this._dbContext.電影圖片movieIimagesLists.Where(m => m.電影編號movieId == payment1.MovieID).Select(p => p.圖片編號image.圖片image).FirstOrDefault();
            payview.MovieName電影名稱 = this._dbContext.電影movies.Where(m => m.電影編號movieId == payment1.MovieID).Select(m => m.中文標題titleCht).FirstOrDefault();
            payview.MovieVersion電影版本 = this._dbContext.電影代碼movieCodes.Where(cd => cd.電影代碼編號movieCodeId == payment1.MovieCode).Select(l => l.語言編號language.語言名稱languageName).FirstOrDefault();
            payview.MovieInfo電影介紹 = this._dbContext.電影movies.Where(m => m.電影編號movieId == payment1.MovieID).Select(m => m.劇情大綱plot).FirstOrDefault();
            payview.Alltciket = fn_票種字串轉換List(payment1.Ticketstring);

            string address = this._dbContext.電影院theaters.FirstOrDefault(t => t.電影院編號theaterId == payment1.theaterID).地址address;
            ViewBag.addressInfo = fn_地址轉經緯度(address);

            return View(payview);
        }

        public IActionResult PaymentWeb2(CInfoForMakeNewOrderViewModel infoview)
        {
            ViewBag.fee = 0;
            int? fee = HttpContext.Session.GetInt32(CDictionary.SK_當前手續費);
            if (fee != null)
                ViewBag.fee = (int)fee;
            infoview.Alltciket = fn_票種字串轉換List(infoview.TicketInfo);
            HttpContext.Session.SetString(CDictionary.SK_加購的商品, infoview.ProductInfo);
            return View(infoview);
        }

        public IActionResult PaymentWeb3(CCreateOrderViewModel order)
        {

            string SessionStr = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member member = JsonSerializer.Deserialize<會員member>(SessionStr);

            fn_建立新訂單總表(order.ScreenID, member.會員編號memberId);
            fn_修改出售座位狀態(order.ScreenID, order.SeatInfo);
            int NewOrderID = this._dbContext.訂單總表orders.AsEnumerable().LastOrDefault().訂單編號orderId;
            fn_建立新訂單明細(NewOrderID, order.TicketInfo);
            fn_建立新出售座位明細(NewOrderID, order.ScreenID, order.SeatInfo);
            string MemberName = this._dbContext.會員members.Where(m => m.會員編號memberId == member.會員編號memberId).Select(n => (n.姓氏lName + n.名字fName)).FirstOrDefault();
            fn_寄送郵件("ilovemdaofficialok@gmail.com", MemberName, order.fullPrice);
            fn_會員購票送紅利(member.會員編號memberId, (int)order.fullPrice);
            fn_新增購買商品明細(NewOrderID);
            HttpContext.Session.SetString(CDictionary.SK_ORDER_INFO, "");

            string jsoncoupon = HttpContext.Session.GetString(CDictionary.SK_使用的優惠券);
            if (!string.IsNullOrEmpty(jsoncoupon))
            {
                優惠總表coupon coupon = JsonSerializer.Deserialize<優惠總表coupon>(jsoncoupon);
                優惠明細couponList couponlist = this._dbContext.優惠明細couponLists
                                                  .Where(c => c.會員編號memberId == member.會員編號memberId && c.優惠編號couponId == coupon.優惠編號couponId && c.是否使用優惠oxCouponUsing == false)
                                                  .FirstOrDefault();
                couponlist.是否使用優惠oxCouponUsing = true;
                使用優惠明細usingCouponList usingcoupon = new 使用優惠明細usingCouponList()
                {
                    優惠明細編號couponListId = couponlist.優惠明細編號couponListId,
                    訂單編號orderId=NewOrderID,
                };
                this._dbContext.使用優惠明細usingCouponLists.Add(usingcoupon);
                this._dbContext.SaveChanges();
                HttpContext.Session.SetString(CDictionary.SK_使用的優惠券, "");
            }
            return View();
        }

        public IActionResult PaymentWebO()
        {
            CCreateOrderViewModel order = new CCreateOrderViewModel();
            string json = HttpContext.Session.GetString(CDictionary.SK_ORDER_INFO);
            order = JsonSerializer.Deserialize<CCreateOrderViewModel>(json);
            return RedirectToAction("PaymentWeb3", order);
        }

        public IActionResult TicketError()
        {
            return View();
        }

        #endregion

        #region 歐付寶
        public IActionResult OPayment(CCreateOrderViewModel order)
        {
            string jsonprd = HttpContext.Session.GetString(CDictionary.SK_加購的商品);

            string[] product = jsonprd.Split('#');

            List<int> productcount = new List<int>()
             {
                   Convert.ToInt32(product[0]),
                   Convert.ToInt32(product[1]),
                   Convert.ToInt32(product[2]),
                   Convert.ToInt32(product[3]),
             };

            List<int> productprice = new List<int>()
            {
                    120,200,80,100
            };

            List<string> productname = new List<string>()
            {
                 "超值套餐","豪華套餐","爆米花","造型飲料杯"
            };


            string jsonstr = JsonSerializer.Serialize(order);
            HttpContext.Session.SetString(CDictionary.SK_ORDER_INFO, jsonstr);

            #region 金流支付
            string tradeNo = Guid.NewGuid().ToString();
            tradeNo = tradeNo.Substring(tradeNo.Length - 12, 12);
            ViewBag.tradeNo = tradeNo;
            string timenow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ViewBag.timenow = timenow;
            string moviename = this._dbContext.場次screenings.Where(s => s.場次編號screeningId == order.ScreenID).Select(s => s.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht).FirstOrDefault();
            List<CTicketItemViewModel> ticketlist = fn_票種字串轉換List(order.TicketInfo);
            int total = Convert.ToInt32(order.fullPrice);
            string ItemName = "";
            string filename = "";
            string ticketname = "";
            decimal ticketprice = 0;
            foreach (var item in ticketlist)
            {
                filename = this._dbContext.場次screenings.Where(s => s.場次編號screeningId == order.ScreenID).Select(s => s.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht).FirstOrDefault();
                ticketname = this._dbContext.票價資訊ticketPrices.Where(t => t.票價明細ticketId == item.TicketID).FirstOrDefault().票種編號ticketType.票種名稱ticketTypeName;
                ticketprice = this._dbContext.票價資訊ticketPrices.Where(t => t.票價明細ticketId == item.TicketID).FirstOrDefault().價格ticketPrice;
                ItemName += $"{filename}-{ticketname}({ticketprice}元)X{item.TicketCount}#";
            }
            ItemName = ItemName.Substring(0, ItemName.Length - 1);
            for (int i = 0; i < productcount.Count(); i++)
            {
                if (productcount[i] > 0)
                {
                    ItemName += $"#{productname[i]}({productprice[i]}元)X{productcount[i]}";
                }
            }
            ViewBag.Total = total;
            ViewBag.ItemName = ItemName;
            NameValueCollection parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["HashKey"] = "5294y06JbISpM5x9";
            parameters["ChoosePayment"] = "Credit";
            parameters["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}/Ticketing/PaymentWebO";    //完成後跳回去的頁面
            parameters["CreditInstallment"] = "";
            parameters["EncryptType"] = "1";
            parameters["InstallmentAmount"] = "";
            parameters["ItemName"] = ItemName;
            parameters["MerchantID"] = "2000132";
            parameters["MerchantTradeDate"] = timenow;
            parameters["MerchantTradeNo"] = tradeNo;
            parameters["PaymentType"] = "aio";
            parameters["Redeem"] = "";
            parameters["ReturnURL"] = "https://developers.opay.tw/AioMock/MerchantReturnUrl";
            parameters["StoreID"] = "";
            parameters["TotalAmount"] = total.ToString();
            parameters["TradeDesc"] = "建立信用卡測試訂單";
            parameters["HashIV"] = "v77hoKGq4kWxNNIS";

            ViewBag.ClientBackURL = $"{Request.Scheme}://{Request.Host}/Ticketing/PaymentWebO";

            string checkMacValue = parameters.ToString();

            checkMacValue = checkMacValue.Replace("=", "%3d").Replace("&", "%26");

            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(checkMacValue.ToLower()));
            checkMacValue = Convert.ToHexString(byteArray).ToUpper();
            ViewBag.checkMacValue = checkMacValue;
            #endregion
            return View();
        }

        [NonAction]
        public string Get_SHA256_Hash(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray).ToUpper();
        }
        #endregion

        #region ViewComponent區

        public IActionResult ComponentSelectMovie(string data)
        {
            HttpContext.Session.SetString(CDictionary.SK_選擇的放映日期, data);
            DateTime CurrentDate = fn_字串轉日期格式(data);
            return ViewComponent("SelectMovie", CurrentDate);
        }

        public IActionResult ComponentSelectScreen(int Code)
        {
            HttpContext.Session.SetInt32(CDictionary.SK_選擇的電影Code, Code);
            string data = HttpContext.Session.GetString(CDictionary.SK_選擇的放映日期);
            DateTime CurrentDate = fn_字串轉日期格式(data);
            return ViewComponent("SelectScreen", CurrentDate);
        }

        public IActionResult ComponentSelectScreen2(string data)
        {
            HttpContext.Session.SetString(CDictionary.SK_選擇的放映日期, data);
            DateTime CurrentDate = fn_字串轉日期格式(data);
            return ViewComponent("SelectScreen", CurrentDate);
        }

        public IActionResult ComponentMovieInfo(int Code)
        {
            int id = this._dbContext.電影代碼movieCodes
                         .Where(m => m.電影代碼編號movieCodeId == Code)
                         .FirstOrDefault().電影編號movieId;
            return ViewComponent("MovieInfo", id);
        }


        #endregion

        #region Ajax區

        public IActionResult Coupon(string code)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member member = JsonSerializer.Deserialize<會員member>(json);
            優惠總表coupon coupon = this._dbContext.優惠明細couponLists
                                      .Where(c => c.優惠編號coupon.優惠代碼couponCode == code && c.會員編號memberId == member.會員編號memberId && c.是否使用優惠oxCouponUsing == false)
                                      .Select(c => c.優惠編號coupon).FirstOrDefault();
            if (coupon != null)
            {
                優惠總表coupon coupon2 = new 優惠總表coupon()
                {
                    優惠代碼couponCode = coupon.優惠代碼couponCode,
                    優惠名稱couponName = coupon.優惠名稱couponName,
                    優惠編號couponId = coupon.優惠編號couponId,
                    優惠折扣couponDiscount = coupon.優惠折扣couponDiscount,
                };
                string json_coupon = JsonSerializer.Serialize(coupon2);
                HttpContext.Session.SetString(CDictionary.SK_使用的優惠券, json_coupon);
                return Json(coupon2);
            }
            return Json('F');
        }

        public IActionResult ShowCoupon()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member member = JsonSerializer.Deserialize<會員member>(json);
            List<優惠明細couponList> coupons = this._dbContext.優惠明細couponLists
                           .Where(c => c.會員編號memberId == member.會員編號memberId && c.是否使用優惠oxCouponUsing == false)
                           .ToList();
            return ViewComponent("ShowCoupon", coupons);
        }

        #endregion

        #region 驗證信

        public IActionResult sendmail(string email)
        {
            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();

            Random ran = new Random();
            int rannum = ran.Next(9999) + 1966728;
            HttpContext.Session.SetInt32(CDictionary.SK_購票驗證碼, rannum);
            builder.HtmlBody = $"<p>你好，你的驗證碼為{rannum}</p>" +
                              $"<div style='border: 2px solid black;text-align: center;'></div>" +
                              $"<p>當前時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";

            message.From.Add(new MailboxAddress("MDA訂票官網", "annlan08@outlook.com"));
            message.To.Add(new MailboxAddress("親愛的顧客", email));
            message.Subject = "MDA訂票網付款資訊";
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("annlan08@outlook.com", "ssbb19970513");
                client.Send(message);
                client.Disconnect(true);
            }
            return Json("K");
        }

        public IActionResult checkcode(int code) //==> 1成功 0失敗
        {
            int? Code = HttpContext.Session.GetInt32(CDictionary.SK_購票驗證碼);

            if (Code != null)
            {
                if ((int)Code == code)
                {
                    return Json("1");
                }
            }

            return Json("0");
        }

        #endregion

        #region 內建方法區
        [NonAction]
        public void fn_會員購票送紅利(int memberID, int fullprice)
        {
            會員member member = this._dbContext.會員members.Where(m => m.會員編號memberId == memberID).FirstOrDefault();
            int bouns = fullprice / 10;
            member.紅利點數bonus += bouns;
            this._dbContext.SaveChanges();
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member memberSession = JsonSerializer.Deserialize<會員member>(json);
            memberSession.紅利點數bonus = member.紅利點數bonus;
            json = JsonSerializer.Serialize<會員member>(memberSession);
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
        }

        [NonAction]
        public List<CTicketItemViewModel> fn_票種字串轉換List(string ticketString) //==>將  (回傳字串 id:count#id:count#  id=票價明細 count=數量) 轉成 一個 List<CTicketItemViewModel>
        {
            List<CTicketItemViewModel> tciketList = new List<CTicketItemViewModel>();
            string[] ticketArr = ticketString.Split('#');
            for (int i = 0; i < (ticketArr.Length - 1); i++)
            {
                string[] itemArr = ticketArr[i].Split(':');
                int id = Convert.ToInt32(itemArr[0]);
                int count = Convert.ToInt32(itemArr[1]);
                CTicketItemViewModel Tickitem = new CTicketItemViewModel()
                {
                    TicketID = id,
                    TicketCount = count,
                    TicketName = this._dbContext.票價資訊ticketPrices.Where(t => t.票價明細ticketId == id).Select(t => t.票種編號ticketType.票種名稱ticketTypeName).FirstOrDefault(),
                    TicketPrice = this._dbContext.票價資訊ticketPrices.Where(t => t.票價明細ticketId == id).Select(t => t.價格ticketPrice).FirstOrDefault(),
                };
                tciketList.Add(Tickitem);
            }
            return tciketList;
        }

        [NonAction]
        public void fn_建立新訂單總表(int ScreenID, int MemberID)
        {
            訂單總表order order = new 訂單總表order()
            {
                場次編號screeningId = ScreenID,
                會員編號memberId = MemberID,
                訂單時間orderTime = DateTime.Now,
                訂單狀態編號orderStatusId = 2,
            };
            this._dbContext.訂單總表orders.Add(order);
            this._dbContext.SaveChanges();
        }

        [NonAction]
        public void fn_建立新訂單明細(int orderID, string TicketInfo)
        {
            string[] TickerArr = TicketInfo.Split('#');

            for (int i = 0; i < (TickerArr.Count() - 1); i++)
            {
                if (string.IsNullOrWhiteSpace(TickerArr[i]))
                    break;
                string[] InfoArr = TickerArr[i].Split(':');
                訂單明細orderDetail orderDetail = new 訂單明細orderDetail()
                {
                    訂單編號orderId = orderID,
                    票價明細ticketId = Convert.ToInt32(InfoArr[0]),
                    張數count = Convert.ToInt32(InfoArr[1]),
                };
                this._dbContext.訂單明細orderDetails.Add(orderDetail);
            }

            this._dbContext.SaveChanges();
        }

        [NonAction]
        public void fn_建立新出售座位明細(int orderID, int screenID, string seatInfo)
        {
            string[] seatArr = seatInfo.Split('#');
            for (int i = 0; i < seatArr.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(seatArr[i]))
                    break;
                出售座位明細seatSold seatSold = new 出售座位明細seatSold()
                {
                    訂單編號orderId = orderID,
                    場次編號screeningId = screenID,
                    座位表編號seatId = seatArr[i],
                };

                this._dbContext.出售座位明細seatSolds.Add(seatSold);
            }
            this._dbContext.SaveChanges();
        }

        [NonAction]
        public void fn_修改出售座位狀態(int screenID, string seatInfo)
        {
            string[] selectSeatArr = seatInfo.Split('#');
            出售座位狀態seatStatus seatStatus = this._dbContext.出售座位狀態seatStatuses.Where(seat => seat.場次編號screeningId == screenID).FirstOrDefault();
            string[] seatArr = seatStatus.出售座位資訊seatSoldInfo.Split('@');
            for (int i = 0; i < seatArr.Count(); i++)
            {
                foreach (string selectitem in selectSeatArr)
                {
                    if (seatArr[i].Trim() == selectitem.Trim())
                    {
                        seatArr[i] += "saled";
                    }
                }
            }
            seatStatus.出售座位資訊seatSoldInfo = "";
            for (int i = 0; i < seatArr.Count(); i++)
            {
                seatStatus.出售座位資訊seatSoldInfo += seatArr[i] + "@";
            }
            this._dbContext.SaveChanges();
        }

        [NonAction]
        public void fn_寄送郵件(string email, string name, decimal fullPrice)
        {
            訂單總表order order = this._dbContext.訂單總表orders.OrderBy(o => o.訂單編號orderId).LastOrDefault();
            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();
            var image = builder.LinkedResources.Add($"{_host.WebRootPath}\\images\\Ticketing\\qrcode.jpg");
            string _price = fullPrice.ToString("0.00");
            int _id = order.訂單編號orderId;
            string[] _date = order.場次編號screening.放映日期playDate.Date.ToString().Split(' ');
            string _time = order.場次編號screening.放映開始時間playTime;
            string _theater = order.場次編號screening.影廳編號cinema.電影院編號theater.電影院名稱theaterName;
            string _movie = order.場次編號screening.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht;

            builder.HtmlBody = $"<div><h2 style='color:red'>MDA訂票網付款成功通知</h2></div>" +
                               $"<div>" +
                               $"<p>親愛的{name}你好 ，已收到你的付款(總價錢價錢:{_price}NT$)</p>" +
                               $"<div style='border: 1px solid black; text-align:center; width: 25vw'>" +
                               $"<h3 style='color: blue'>訂單內容</h3>" +
                               $"<h5>訂單編號:{_id}</h5>" +
                               $"<p>電影院名稱:{_theater}</p>" +
                               $"<p>電影名稱:{_movie}</p>" +
                               $"<p>放映日期:{_date[0]}</p>" +
                               $"<p>放映時間:{_time}</p>" +
                               $"<p>放映影廳:白金廳</p>" +
                               $"</div>" +
                               $"<p>當前時間:{DateTime.Now}</p>" +
                               $"<p>如對此訂單有任何問題，歡迎與我們聯絡!</p>" +
                               $"<p>感謝您對於本網站的支持</p>" +
                               $"<p>歡迎您再度光臨</p>" +
                               $"</div>";

            message.From.Add(new MailboxAddress("MDA訂票官網", "annlan08@outlook.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = "MDA訂票網付款資訊";
            message.Body = builder.ToMessageBody();


            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("annlan08@outlook.com", "ssbb19970513");
                client.Send(message);
                client.Disconnect(true);
            }

        }

        [NonAction]
        public int fn_計算座位數(string seatInfo)
        {
            string[] seatArr = seatInfo.Split('@');
            int count = 0;
            foreach (string item in seatArr)
            {
                if (!(item.Contains("NA")) && !(item.Contains("saled")) && !(string.IsNullOrWhiteSpace(item)) && !(item.Contains("||")) && !(string.IsNullOrEmpty(item)))
                    count++;
            }
            return count;
        }

        [NonAction]
        public DateTime fn_字串轉日期格式(string DateString)
        {
            string[] dateArr = DateString.Split('/');
            string[] dayArr = dateArr[2].Split(' ');
            DateTime Date = new DateTime(Convert.ToInt32(dateArr[0]), Convert.ToInt32(dateArr[1]), Convert.ToInt32(dayArr[0]));
            return Date;

        }

        [NonAction]
        public void fn_新增購買商品明細(int orderID)
        {
            string jsonprd = HttpContext.Session.GetString(CDictionary.SK_加購的商品);
            string[] product = jsonprd.Split('#');
            List<int> productcount = new List<int>()
             {
                   Convert.ToInt32(product[0]),
                   Convert.ToInt32(product[1]),
                   Convert.ToInt32(product[2]),
                   Convert.ToInt32(product[3]),
             };
            List<int> productid = new List<int>()
             {
                  13,14,15,16
             };
            for (int i = 0; i < productcount.Count(); i++)
            {
                if (productcount[i] != 0)
                {
                    購買商品明細receipt receipt = new 購買商品明細receipt()
                    {
                        商品編號productId = productid[i],
                        訂單編號orderId = orderID,
                        商品數量qty = productcount[i],
                    };
                    this._dbContext.購買商品明細receipts.Add(receipt);
                }
            }
            this._dbContext.SaveChanges();
        }


        #endregion

        #region 地圖

        public string fn_地址轉經緯度(string address)
        {
            string _url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key=AIzaSyAiIO_bYPUlozPgpWf3KXtdgZI_xmHLhDg";
            string url = String.Format(_url);
            string result = String.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            using (var response = request.GetResponse())
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                return result;
            }
        }

        #endregion

        #region 自動產生資料
        private Random ran = new Random();

        private List<string> seatInfolist = new List<string>()
        {
            "NA@NA@NA@NA@1:4@1:5@1:6@NA@1:7@1:8@NA@NA@NA@NA@NA@1:14@1:15@NA@1:16@1:17@1:18@NA@NA@NA@NA@||@NA@NA@NA@NA@2:4@2:5@2:6@NA@2:7@2:8@NA@NA@NA@NA@NA@2:14@2:15@NA@2:16@2:17@2:18@NA@NA@NA@NA@||@NA@NA@NA@NA@3:4@3:5@3:6@NA@3:7@3:8@NA@NA@NA@NA@NA@3:14@3:15@NA@3:16@3:17@3:18@NA@NA@NA@NA@||@NA@NA@NA@NA@4:4@4:5@4:6@NA@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@NA@4:16@4:17@4:18@NA@NA@NA@NA@||@NA@NA@NA@NA@5:4@5:5@5:6@NA@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@NA@5:16@5:17@5:18@NA@NA@NA@NA@||@NA@NA@NA@NA@6:4@6:5@6:6@NA@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@NA@6:16@6:17@6:18@NA@NA@NA@NA@||@NA@NA@NA@NA@7:4@7:5@7:6@NA@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@NA@7:16@7:17@7:18@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@NA@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@NA@8:16@8:17@8:18@NA@8:19@8:20@8:21@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@NA@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@NA@9:16@9:17@9:18@NA@9:19@9:20@9:21@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@NA@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@10:15@NA@10:16@10:17@10:18@NA@10:19@10:20@10:21@||@11:1@11:2@11:3@NA@11:4@11:5@11:6@NA@11:7@11:8@11:9@11:10@11:11@11:12@11:13@11:14@11:15@NA@11:16@11:17@11:18@NA@11:19@11:20@11:21@||@12:1@12:2@12:3@NA@12:4@12:5@12:6@NA@12:7@12:8@12:9@12:10@12:11@12:12@12:13@12:14@12:15@NA@12:16@12:17@12:18@NA@12:19@12:20@12:21@||@13:1@13:2@13:3@NA@13:4@13:5@13:6@NA@13:7@13:8@13:9@13:10@13:11@13:12@13:13@13:14@13:15@NA@13:16@13:17@13:18@NA@13:19@13:20@13:21@||@14:1@14:2@14:3@NA@14:4@14:5@14:6@NA@14:7@14:8@NA@NA@14:11@14:12@14:13@14:14@14:15@NA@14:16@14:17@14:18@NA@14:19@14:20@14:21@||@15:1@15:2@15:3@NA@15:4@15:5@15:6@NA@15:7@15:8@NA@NA@15:11@15:12@15:13@15:14@15:15@NA@15:16@15:17@15:18@NA@15:19@15:20@15:21@||@16:1@16:2@16:3@NA@16:4@16:5@16:6@NA@16:7@16:8@16:9@16:10@16:11@16:12@16:13@16:14@16:15@NA@16:16@16:17@16:18@NA@16:19@16:20@16:21@||@17:1@17:2@17:3@NA@17:4@17:5@17:6@NA@17:7@17:8@17:9@17:10@17:11@17:12@17:13@17:14@17:15@NA@17:16@17:17@17:18@NA@17:19@17:20@17:21@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@18:1@18:2@18:3@NA@18:4@18:5@18:6@NA@18:7@18:8@18:9@18:10@18:11@18:12@18:13@18:14@18:15@NA@18:16@18:17@18:18@NA@18:19@18:20@18:21@||@19:1@19:2@19:3@NA@19:4@19:5@19:6@NA@19:7@19:8@19:9@19:10@19:11@19:12@19:13@19:14@19:15@NA@19:16@19:17@19:18@NA@19:19@19:20@19:21@||@NA@NA@NA@NA@20:4@20:5@20:6@NA@20:7@20:8@20:9@20:10@20:11@20:12@20:13@20:14@20:15@NA@20:16@20:17@20:18@NA@NA@NA@NA@||@NA@NA@NA@NA@21:4@21:5@21:6@NA@21:7@21:8@21:9@21:10@21:11@21:12@21:13@21:14@21:15@NA@21:16@21:17@21:18@NA@NA@NA@NA@||@NA@NA@NA@NA@22:4@22:5@22:6@NA@22:7@22:8@NA@NA@NA@NA@NA@22:14@22:15@NA@22:16@22:17@22:18@NA@NA@NA@NA@||@NA@NA@NA@NA@23:4@23:5@23:6@NA@23:7@23:8@NA@NA@NA@NA@NA@23:14@23:15@NA@23:16@23:17@23:18@NA@NA@NA@NA@||@",
            "NA@NA@NA@NA@1:5@NA@NA@1:8@1:9@1:10@1:11@1:12@1:13@1:14@1:15@NA@NA@NA@||@NA@NA@NA@NA@2:5@NA@NA@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@NA@NA@NA@||@NA@NA@NA@NA@3:5@NA@NA@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@NA@NA@NA@||@4:1@NA@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@NA@NA@NA@4:14@4:15@NA@NA@NA@||@NA@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@NA@NA@NA@5:14@5:15@NA@NA@NA@||@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@NA@NA@6:14@6:15@6:16@6:17@6:18@||@7:1@7:2@7:3@7:4@NA@NA@NA@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@7:16@7:17@7:18@||@8:1@8:2@8:3@8:4@NA@NA@NA@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@NA@8:17@8:18@||@9:1@9:2@9:3@9:4@NA@NA@NA@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@NA@9:17@9:18@||@10:1@10:2@10:3@10:4@10:5@10:6@10:7@10:8@10:9@10:10@NA@NA@NA@10:14@10:15@10:16@10:17@10:18@||@11:1@11:2@11:3@11:4@11:5@11:6@11:7@11:8@11:9@11:10@NA@NA@NA@11:14@11:15@11:16@11:17@11:18@||@12:1@NA@NA@NA@12:5@12:6@12:7@12:8@12:9@12:10@NA@NA@NA@12:14@12:15@12:16@12:17@12:18@||@13:1@NA@NA@NA@13:5@13:6@13:7@13:8@13:9@13:10@13:11@13:12@13:13@13:14@13:15@13:16@13:17@13:18@||@14:1@14:2@14:3@14:4@14:5@14:6@14:7@14:8@14:9@14:10@14:11@14:12@14:13@14:14@14:15@14:16@14:17@14:18@||@15:1@15:2@15:3@15:4@15:5@15:6@15:7@15:8@15:9@15:10@15:11@15:12@15:13@15:14@15:15@15:16@15:17@15:18@||@16:1@16:2@16:3@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@16:17@16:18@||@17:1@17:2@17:3@17:4@17:5@17:6@17:7@17:8@17:9@17:10@17:11@17:12@17:13@17:14@17:15@17:16@17:17@17:18@||@18:1@NA@NA@18:4@18:5@18:6@NA@18:8@18:9@18:10@NA@18:12@18:13@18:14@18:15@18:16@18:17@18:18@||@",
            "NA@NA@NA@NA@NA@1:6@1:7@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@2:5@2:6@2:7@2:8@NA@NA@NA@NA@||@NA@NA@NA@3:4@3:5@3:6@3:7@3:8@3:9@NA@NA@NA@||@NA@NA@4:3@4:4@NA@4:6@4:7@NA@4:9@4:10@NA@NA@||@NA@5:2@5:3@5:4@NA@5:6@5:7@NA@5:9@5:10@5:11@NA@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@||@7:1@7:2@7:3@NA@7:5@7:6@7:7@7:8@NA@7:10@7:11@7:12@||@NA@8:2@8:3@8:4@NA@NA@NA@NA@8:9@8:10@8:11@NA@||@NA@NA@9:3@9:4@9:5@9:6@9:7@9:8@9:9@9:10@NA@NA@||@NA@NA@NA@10:4@10:5@10:6@10:7@10:8@10:9@NA@NA@NA@||@NA@NA@NA@NA@11:5@11:6@11:7@11:8@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@12:6@12:7@NA@NA@NA@NA@NA@||@",
            "NA@NA@NA@NA@NA@NA@NA@1:8@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@2:7@2:8@2:9@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@3:6@3:7@3:8@3:9@3:10@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@4:5@4:6@NA@4:8@NA@4:10@4:11@NA@NA@NA@NA@||@NA@NA@NA@5:4@5:5@NA@5:7@5:8@5:9@NA@5:11@5:12@NA@NA@NA@||@NA@NA@6:3@6:4@NA@6:6@6:7@6:8@6:9@6:10@NA@6:12@6:13@NA@NA@||@NA@7:2@7:3@7:4@7:5@NA@NA@7:8@NA@NA@7:11@7:12@7:13@7:14@NA@||@NA@8:2@8:3@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@||@NA@NA@9:3@9:4@9:5@9:6@9:7@NA@9:9@9:10@9:11@9:12@9:13@9:14@NA@||@NA@NA@NA@10:4@10:5@NA@10:7@10:8@10:9@NA@10:11@10:12@10:13@NA@NA@||@NA@NA@NA@NA@11:5@11:6@NA@NA@NA@11:10@11:11@11:12@NA@NA@NA@||@NA@NA@NA@NA@12:5@12:6@12:7@NA@12:9@12:10@12:11@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@13:6@13:7@13:8@13:9@13:10@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@14:7@14:8@14:9@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@15:8@NA@NA@NA@NA@NA@15:14@15:15@||@",
            "1:1@NA@1:3@NA@1:5@NA@1:7@NA@||@NA@2:2@NA@2:4@NA@2:6@NA@2:8@||@3:1@NA@3:3@NA@3:5@NA@3:7@NA@||@NA@4:2@NA@4:4@NA@4:6@NA@4:8@||@5:1@NA@5:3@NA@5:5@NA@5:7@NA@||@NA@6:2@NA@6:4@NA@6:6@NA@6:8@||@7:1@NA@7:3@NA@7:5@NA@7:7@NA@||@NA@8:2@NA@8:4@NA@8:6@NA@8:8@||@",
            "1:1@1:2@NA@1:3@1:4@NA@NA@NA@1:8@1:9@NA@1:10@1:11@||@2:1@2:2@NA@2:3@2:4@NA@NA@NA@2:8@2:9@NA@2:10@2:11@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@3:8@3:9@NA@3:10@3:11@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@4:8@4:9@NA@4:10@4:11@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@NA@5:10@5:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@NA@6:10@6:11@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@7:10@7:11@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@8:8@8:9@NA@8:10@8:11@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@9:8@9:9@NA@9:10@9:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@10:9@NA@10:10@10:11@||@11:1@11:2@NA@11:3@11:4@11:5@11:6@11:7@11:8@11:9@NA@11:10@11:11@||@",
            "1:1@1:2@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@1:15@1:16@NA@1:17@1:18@1:19@1:20@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@2:16@NA@2:17@2:18@2:19@2:20@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@3:16@NA@3:17@3:18@3:19@3:20@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@4:16@NA@4:17@4:18@4:19@4:20@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@5:16@NA@5:17@5:18@5:19@5:20@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@6:16@NA@6:17@6:18@6:19@6:20@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@7:16@NA@7:17@7:18@7:19@7:20@||@8:1@8:2@NA@NA@NA@NA@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@8:16@NA@8:17@8:18@8:19@8:20@||@NA@NA@9:3love@NA@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@NA@NA@9:15love@NA@NA@NA@9:18love@9:19@9:20@||@",
            "1:1@1:2@NA@1:3@1:4@1:5@1:6@1:7@1:8@1:9@1:10@NA@1:11@1:12@||@2:1@2:2@NA@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@2:11@2:12@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@NA@3:11@3:12@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@NA@4:11@4:12@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@NA@5:11@5:12@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@6:11@6:12@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@NA@7:11@7:12@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@8:8@8:9@8:10@NA@8:11@8:12@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@9:8@9:9@9:10@NA@9:11@9:12@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@10:9@10:10@NA@10:11@10:12@||@11:1@11:2@NA@11:3@11:4@11:5@11:6@11:7@11:8@11:9@11:10@NA@11:11@11:12@||@12:1@12:2@NA@12:3@12:4@12:5@12:6@12:7@12:8@12:9@12:10@NA@12:11@12:12@||@13:1@13:2love@NA@NA@NA@13:5love@13:6@13:7@13:8@13:9@NA@NA@13:11love@13:12@||@",
            "1:1@1:2@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@NA@1:15@1:16@1:17@1:18@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@NA@2:15@2:16@2:17@2:18@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@NA@3:15@3:16@3:17@3:18@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@NA@4:15@4:16@4:17@4:18@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@NA@5:15@5:16@5:17@5:18@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@NA@6:15@6:16@6:17@6:18@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@NA@7:15@7:16@7:17@7:18@||@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@NA@8:15@8:16@8:17@8:18@||@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@NA@9:15@9:16@9:17@9:18@||@10:1@10:2@10:3@10:4@NA@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@NA@10:15@10:16@10:17@10:18@||@11:1@11:2@11:3@11:4@NA@11:5@11:6@11:7@11:8@11:9@11:10@11:11@11:12@11:13@11:14@NA@11:15@11:16@11:17@11:18@||@12:1@12:2@12:3@12:4@NA@12:5@12:6@12:7@12:8@12:9@12:10@12:11@12:12@12:13@12:14@NA@12:15@12:16@12:17@12:18@||@13:1@13:2@13:3love@NA@NA@NA@13:6love@13:7@13:8@13:9@13:10@13:11@13:12@13:13@13:14@NA@NA@13:16love@13:17@13:18@||@",
            "NA@NA@NA@NA@NA@1:5love@NA@NA@NA@NA@NA@NA@1:12love@NA@NA@NA@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@2:13@2:14@2:15@2:16@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@NA@3:13@3:14@3:15@3:16@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@NA@4:13@4:14@4:15@4:16@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@5:13@5:14@5:15@5:16@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@NA@6:13@6:14@6:15@6:16@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@NA@7:13@7:14@7:15@7:16@||@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@NA@8:13@8:14@8:15@8:16@||@9:1@9:2@9:3@NA@NA@NA@NA@NA@NA@NA@9:10@9:11@9:12@NA@9:13@9:14@9:15@NA@||@",
            "1:1@1:2@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9love@NA@1:11love@NA@1:13love@1:14@1:15@1:16@NA@1:17@1:18@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@2:16@NA@2:17@2:18@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@3:16@NA@3:17@3:18@NA@NA@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@4:16@NA@4:17@4:18@NA@NA@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@5:16@NA@5:17@5:18@NA@NA@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@6:16@NA@6:17@6:18@NA@NA@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@7:16@NA@7:17@7:18@NA@NA@||@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@8:16@NA@8:17@8:18@NA@NA@||@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@9:16@NA@9:17@9:18@9:19@9:20@||@10:1@10:2@10:3@10:4@NA@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@10:15@10:16@NA@10:17@10:18@10:19@10:20@||@11:1@11:2@11:3@11:4@NA@11:5@11:6@11:7@11:8@11:9@11:10@11:11@11:12@11:13@11:14@11:15@11:16@NA@11:17@11:18@11:19@11:20@||@12:1@12:2@12:3@12:4@NA@12:5@12:6@12:7@12:8@12:9@12:10@12:11@12:12@12:13@12:14@12:15@12:16@NA@12:17@12:18@12:19@12:20@||@13:1@13:2@13:3@13:4@NA@13:5@13:6@13:7@13:8@13:9@13:10@13:11@13:12@13:13@13:14@13:15@13:16@NA@13:17@13:18@13:19@13:20@||@",
            "NA@NA@NA@1:3@1:4@1:5@1:6@1:7love@NA@1:9love@NA@1:11@1:12@1:13@1:14@NA@1:15@1:16@||@NA@NA@NA@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@NA@2:15@2:16@||@NA@NA@NA@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@NA@3:15@3:16@||@NA@NA@NA@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@NA@4:15@4:16@||@NA@NA@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@NA@5:15@5:16@||@NA@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@NA@6:15@6:16@||@NA@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@NA@7:15@7:16@||@NA@NA@NA@8:3@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@NA@8:15@8:16@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@NA@9:15@9:16@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@NA@10:15@10:16@||@",
            "NA@NA@NA@NA@NA@NA@NA@1:7@1:8@1:9@1:10@1:11love@NA@1:13love@NA@1:15love@1:16@1:17@1:18@NA@NA@NA@NA@NA@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@NA@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@2:16@2:17@2:18@NA@2:19@2:20@2:21@2:22@2:23@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@NA@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@3:16@3:17@3:18@NA@3:19@3:20@3:21@3:22@3:23@3:24@||@4:1@4:2@4:3@4:4@4:5@4:6@NA@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@4:16@4:17@4:18@NA@4:19@4:20@4:21@4:22@4:23@4:24@||@5:1@5:2@5:3@5:4@5:5@5:6@NA@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@5:16@5:17@5:18@NA@5:19@5:20@5:21@5:22@5:23@5:24@||@6:1@6:2@6:3@6:4@6:5@6:6@NA@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@6:16@6:17@6:18@NA@6:19@6:20@6:21@6:22@6:23@6:24@||@7:1@7:2@7:3@7:4@7:5@7:6@NA@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@7:16@7:17@7:18@NA@7:19@7:20@7:21@7:22@7:23@7:24@||@8:1@8:2@8:3@8:4@8:5@8:6@NA@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@8:16@8:17@8:18@NA@8:19@8:20@8:21@8:22@8:23@8:24@||@9:1@9:2@9:3@9:4@9:5@9:6@NA@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@9:16@9:17@9:18@NA@9:19@9:20@9:21@9:22@9:23@9:24@||@",
            "1:1@1:2@1:3@1:4@1:5love@NA@1:7love@NA@1:9@1:10@1:11@1:12@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@||@5:1@5:2@5:3@5:4@5:5@5:6@NA@5:8@5:9@5:10@5:11@5:12@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@||@",
            "NA@1:2love@NA@1:4love@NA@1:6love@1:7@1:8@1:9@1:10@NA@NA@1:11@1:12@1:13@1:14@1:15@1:16@1:17@1:18@1:19@1:20@1:21@1:22@NA@NA@1:23@1:24@1:25@1:26@1:27@1:28@NA@1:30love@NA@1:32love@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@NA@2:11@2:12@2:13@2:14@2:15@2:16@2:17@2:18@2:19@2:20@2:21@2:22@NA@NA@2:23@2:24@2:25@2:26@2:27@2:28@2:29@2:30@2:31@2:32@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@NA@NA@3:11@3:12@3:13@3:14@3:15@3:16@3:17@3:18@3:19@3:20@3:21@3:22@NA@NA@3:23@3:24@3:25@3:26@3:27@3:28@3:29@3:30@3:31@3:32@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@NA@NA@4:11@4:12@4:13@4:14@4:15@4:16@4:17@4:18@4:19@4:20@4:21@4:22@NA@NA@4:23@4:24@4:25@4:26@4:27@4:28@4:29@4:30@4:31@4:32@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@NA@NA@5:11@5:12@5:13@5:14@5:15@5:16@5:17@5:18@5:19@5:20@5:21@5:22@NA@NA@5:23@5:24@5:25@5:26@5:27@5:28@5:29@5:30@5:31@5:32@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@NA@6:11@6:12@6:13@6:14@6:15@6:16@6:17@6:18@6:19@6:20@6:21@6:22@NA@NA@6:23@6:24@6:25@6:26@6:27@6:28@6:29@6:30@6:31@6:32@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@NA@NA@7:11@7:12@7:13@7:14@7:15@7:16@7:17@7:18@7:19@7:20@7:21@7:22@NA@NA@7:23@7:24@7:25@7:26@7:27@7:28@7:29@7:30@7:31@7:32@||@8:1@8:2@8:3@8:4@8:5@8:6@8:7@8:8@8:9@8:10@NA@NA@8:11@8:12@8:13@8:14@8:15@8:16@8:17@8:18@8:19@8:20@8:21@8:22@NA@NA@8:23@8:24@8:25@8:26@8:27@8:28@8:29@8:30@8:31@8:32@||@9:1@9:2@9:3@9:4@9:5@9:6@9:7@9:8@9:9@9:10@NA@NA@9:11@9:12@9:13@9:14@9:15@9:16@9:17@9:18@9:19@9:20@9:21@9:22@NA@NA@9:23@9:24@9:25@9:26@9:27@9:28@9:29@9:30@9:31@9:32@||@10:1@10:2@10:3@10:4@10:5@10:6@10:7@10:8@10:9@10:10@NA@NA@10:11@10:12@10:13@10:14@10:15@10:16@10:17@10:18@10:19@10:20@10:21@10:22@NA@NA@10:23@10:24@10:25@10:26@10:27@10:28@10:29@10:30@10:31@10:32@||@11:1@11:2@11:3@11:4@11:5@11:6@11:7@11:8@11:9@11:10@NA@NA@11:11@11:12@11:13@11:14@11:15@11:16@11:17@11:18@11:19@11:20@11:21@11:22@NA@NA@11:23@11:24@11:25@11:26@11:27@11:28@11:29@11:30@11:31@11:32@||@12:1@12:2@12:3@12:4@12:5@12:6@12:7@12:8@12:9@12:10@NA@NA@12:11@12:12@12:13@12:14@12:15@12:16@12:17@12:18@12:19@12:20@12:21@12:22@NA@NA@12:23@12:24@12:25@12:26@12:27@12:28@12:29@12:30@12:31@12:32@||@NA@NA@NA@13:4@13:5@13:6@13:7@13:8@13:9@13:10@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@13:23@13:24@13:25@13:26@13:27@13:28@13:29@13:30@13:31@13:32@||@NA@NA@NA@NA@14:5@14:6@14:7@14:8@14:9@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@14:24@14:25@14:26@14:27@14:28@14:29@14:30@14:31@NA@||@",
            "NA@NA@NA@NA@NA@NA@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@1:15@1:16@1:17@1:18@1:19@NA@NA@NA@NA@||@2:1@2:2@2:3@2:4@2:5@NA@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@3:5@NA@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13love@NA@3:15love@NA@3:17love@NA@NA@NA@NA@NA@NA@||@4:1@4:2@4:3@4:4@4:5@NA@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@4:16@4:17@NA@4:19@4:20@NA@NA@NA@||@5:1@5:2@5:3@5:4@5:5@NA@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@5:16@5:17@NA@5:19@5:20@NA@NA@NA@||@6:1@6:2@6:3@6:4@6:5@NA@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@6:16@6:17@NA@6:19@6:20@NA@NA@NA@||@7:1@7:2@7:3@7:4@7:5@NA@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@7:16@7:17@NA@7:19@7:20@NA@NA@NA@||@8:1@8:2@8:3@8:4@8:5@NA@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@8:16@8:17@NA@8:19@8:20@NA@NA@NA@||@9:1@9:2@9:3@9:4@9:5@NA@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@9:16@9:17@NA@9:19@9:20@9:21@9:22@9:23@||@10:1@10:2@10:3@10:4@10:5@NA@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@10:15@10:16@10:17@NA@10:19@10:20@10:21@10:22@10:23@||@11:1@11:2@11:3@11:4@11:5@NA@11:6@11:7@11:8@11:9@11:10@11:11@11:12@11:13@11:14@11:15@11:16@11:17@NA@11:19@11:20@11:21@11:22@11:23@||@12:1@12:2@12:3@12:4@12:5@NA@12:6@12:7@12:8@12:9@12:10@12:11@12:12@12:13@12:14@12:15@12:16@12:17@NA@12:19@12:20@12:21@12:22@12:23@||@",
            "NA@NA@NA@1:4@NA@NA@NA@||@NA@NA@2:3@2:4@2:5@NA@NA@||@NA@3:2@3:3@3:4@3:5@3:6@NA@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@NA@NA@5:3@5:4@5:5@NA@NA@||@NA@NA@6:3@6:4@6:5@NA@NA@||@NA@NA@7:3@7:4@7:5@NA@NA@||@",
            "NA@1:2@1:3@NA@NA@NA@NA@NA@1:9@1:10@NA@||@2:1@2:2@2:3@2:4@NA@NA@NA@2:8@2:9@2:10@2:11@||@3:1@3:2@3:3@3:4@3:5@NA@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@NA@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@NA@||@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@NA@NA@||@NA@NA@NA@7:4@7:5@7:6@7:7@7:8@NA@NA@NA@||@NA@NA@NA@NA@8:5@8:6@8:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@9:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "1:1@1:2@1:3@NA@1:4@1:5@1:6@1:7@1:8@NA@1:9@1:10@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@2:7@2:8@NA@2:9@2:10@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@NA@NA@3:5@3:6@3:7@NA@NA@3:9@3:10@||@NA@NA@4:3@NA@4:4@4:5@4:6@4:7@4:8@NA@4:9@4:10@||@NA@NA@5:3@NA@5:4@5:5@5:6@5:7@5:8@NA@5:9@5:10@||@6:1@6:2@6:3@NA@NA@6:5@6:6@6:7@NA@NA@6:9@6:10@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@7:7@7:8@NA@7:9@7:10@||@8:1@8:2@8:3@NA@8:4@NA@NA@8:7@8:8@NA@8:9@8:10@||@9:1@9:2@9:3@NA@9:4@NA@NA@9:7@9:8@NA@NA@NA@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@10:7@10:8@NA@NA@NA@||@",
            "NA@NA@NA@NA@1:4@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@NA@NA@NA@NA@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@NA@2:15@2:16@2:17@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@NA@3:15@3:16@3:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@NA@4:15@4:16@4:17@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@NA@5:15@5:16@5:17@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@NA@6:15@6:16@6:17@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@NA@7:15@7:16@7:17@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@NA@8:15@8:16@8:17@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@NA@9:15@9:16@9:17@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@NA@10:15@10:16@10:17@||@NA@11:2@11:3@NA@11:4@11:5@11:6@11:7@11:8@11:9@11:10@11:11@11:12@11:13@11:14@NA@11:15@11:16@NA@||@NA@NA@NA@NA@NA@12:5@12:6@12:7@12:8@12:9@12:10@12:11@12:12@12:13@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@13:5@13:6@13:7@13:8@13:9@13:10@13:11@13:12@13:13@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@14:5@14:6@14:7@14:8@14:9@14:10@14:11@14:12@14:13@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@15:6@15:7@15:8@15:9@15:10@15:11@15:12@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@16:7@16:8@16:9@16:10@16:11@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@1:2@1:3@NA@1:5@1:6@NA@1:8@NA@1:10@NA@1:12@NA@||@2:1@2:2@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@NA@3:8@3:9@3:10@NA@3:12@3:13@||@NA@4:2@4:3@4:4@NA@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@||@5:1@5:2@NA@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@||@6:1@6:2@6:3@6:4@6:5@6:6@NA@6:8@6:9@NA@6:11@NA@6:13@||@7:1@NA@7:3@NA@7:5@7:6@7:7@7:8@NA@7:10@7:11@7:12@7:13@||@8:1@8:2@8:3@8:4@8:5@8:6@NA@8:8@8:9@8:10@8:11@8:12@8:13@||@NA@9:2@9:3@9:4@NA@9:6@9:7@9:8@NA@9:10@9:11@NA@9:13@||@10:1@10:2@NA@10:4@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@||@11:1@11:2@11:3@11:4@11:5@11:6@11:7@11:8@11:9@11:10@NA@11:12@11:13@||@12:1@NA@12:3@12:4@NA@12:6@NA@12:8@12:9@12:10@12:11@12:12@12:13@||@NA@13:2@13:3@13:4@13:5@13:6@13:7@13:8@13:9@NA@13:11@13:12@NA@||@",
            "1:1@1:2@1:3@1:4@1:5@||@2:1@2:2@2:3@2:4@2:5@||@3:1@3:2@3:3@3:4@3:5@||@4:1@4:2@4:3@4:4@4:5@||@5:1@5:2@5:3@5:4@5:5@||@",
            "NA@1:2@NA@||@2:1@2:2@2:3@||@NA@3:2@NA@||@",
            "1:1@NA@1:3@NA@1:5@NA@1:7@||@NA@2:2@NA@2:4@NA@2:6@NA@||@3:1@NA@3:3@NA@3:5@NA@3:7@||@NA@4:2@NA@4:4@NA@4:6@NA@||@5:1@NA@5:3@NA@5:5@NA@5:7@||@NA@6:2@NA@6:4@NA@6:6@NA@||@7:1@NA@7:3@NA@7:5@NA@7:7@||@",
            "1:1@1:2@NA@1:3@1:4@1:5@NA@1:6@1:7@||@2:1@2:2@NA@2:3@2:4@2:5@NA@2:6@2:7@||@3:1@3:2@NA@3:3@3:4@3:5@NA@3:6@3:7@||@4:1@4:2@NA@4:3@4:4@4:5@NA@4:6@4:7@||@5:1@5:2@NA@5:3@5:4@5:5@NA@5:6@5:7@||@6:1@6:2@NA@6:3@6:4@6:5@NA@6:6@6:7@||@7:1@7:2@NA@7:3@7:4@7:5@NA@7:6@7:7@||@8:1@8:2@NA@8:3@8:4@8:5@NA@8:6@8:7@||@9:1@9:2@NA@9:3@9:4@9:5@NA@9:6@9:7@||@",
            "NA@1:2@1:3@NA@||@2:1@2:2@2:3@2:4@||@3:1@3:2@3:3@3:4@||@NA@4:2@4:3@NA@||@",
            "NA@NA@NA@1:4@1:5@NA@NA@NA@||@NA@NA@NA@2:4@2:5@NA@NA@NA@||@3:1@NA@NA@3:4@3:5@NA@NA@3:8@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@||@NA@NA@6:3@6:4@6:5@6:6@NA@NA@||@NA@NA@7:3@7:4@7:5@7:6@NA@NA@||@8:1@8:2@8:3@8:4@8:5@8:6@8:7@8:8@||@",
            "1:1@1:2@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@NA@1:13@1:14@1:15@1:16@1:17@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@2:13@2:14@2:15@2:16@2:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@NA@3:13@3:14@3:15@3:16@3:17@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@NA@4:13@4:14@4:15@4:16@4:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@5:13@5:14@5:15@5:16@5:17@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@NA@6:13@6:14@6:15@6:16@6:17@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@NA@7:13@7:14@7:15@7:16@7:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@NA@8:13@8:14@8:15@8:16@8:17@||@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@NA@9:13@9:14@9:15@9:16@9:17@||@10:1@10:2@10:3@10:4@NA@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@NA@10:13@10:14@10:15@10:16@10:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@11:1@11:2@11:3@11:4@NA@11:5@11:6@11:7@11:8@11:9@11:10@11:11@11:12@NA@11:13@11:14@11:15@11:16@11:17@||@12:1@12:2@12:3@12:4@NA@12:5@12:6@12:7@12:8@12:9@12:10@12:11@12:12@NA@12:13@12:14@12:15@12:16@12:17@||@13:1@13:2@13:3@13:4@NA@13:5@13:6@13:7@13:8@13:9@13:10@13:11@13:12@NA@13:13@13:14@13:15@13:16@13:17@||@14:1@14:2@14:3@14:4@NA@14:5@14:6@14:7@14:8@14:9@14:10@14:11@14:12@NA@14:13@14:14@14:15@14:16@14:17@||@15:1@15:2@15:3@15:4@NA@15:5@15:6@15:7@15:8@15:9@15:10@15:11@15:12@NA@15:13@15:14@15:15@15:16@15:17@||@",
            "NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@1:1@1:2@1:3@NA@NA@1:6@1:7@1:8@1:9@NA@||@2:1@2:2@2:3@NA@NA@2:6@2:7@2:8@2:9@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@||@",
            "1:1@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@||@NA@3:2@3:3@3:4@3:5@3:6@3:7@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@6:2@6:3@NA@NA@6:6@6:7@||@7:1@7:2@7:3@NA@NA@7:6@7:7@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "1:1@1:2@1:3@NA@1:4@1:5@1:6@NA@1:7@1:8@1:9@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@NA@2:7@2:8@2:9@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@NA@3:7@3:8@3:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@NA@4:7@4:8@4:9@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@NA@5:7@5:8@5:9@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@NA@6:7@6:8@6:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@NA@7:7@7:8@7:9@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@NA@8:7@8:8@8:9@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@NA@9:7@9:8@9:9@||@",
            "1:1@1:2@1:3@NA@1:4@1:5@1:6@NA@1:7@1:8@1:9@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@NA@2:7@2:8@2:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@NA@3:7@3:8@3:9@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@NA@4:7@4:8@4:9@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@NA@5:7@5:8@5:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@NA@6:7@6:8@6:9@||@7:1@7:2@7:3@NA@7:4@NA@7:6@NA@7:7@7:8@7:9@||@8:1@8:2@8:3@NA@8:4@NA@8:6@NA@8:7@8:8@8:9@||@9:1@9:2@9:3@NA@9:4@NA@9:6@NA@9:7@9:8@9:9@||@",
            "1:1@NA@1:3@1:4@NA@1:6@||@2:1@2:2@2:3@2:4@2:5@2:6@||@3:1@3:2@3:3@3:4@3:5@3:6@||@4:1@NA@4:3@4:4@NA@4:6@||@5:1@NA@5:3@5:4@NA@5:6@||@6:1@6:2@6:3@6:4@6:5@6:6@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@NA@NA@NA@1:5@NA@NA@NA@NA@||@2:1@2:2@NA@NA@2:5@NA@NA@2:8@2:9@||@3:1@3:2@NA@NA@3:5@NA@NA@3:8@3:9@||@4:1@4:2@NA@NA@4:5@NA@NA@4:8@4:9@||@5:1@5:2@NA@5:4@5:5@5:6@NA@5:8@5:9@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@NA@7:4@7:5@7:6@NA@7:8@7:9@||@8:1@8:2@NA@NA@NA@NA@NA@8:8@8:9@||@9:1@9:2@NA@NA@NA@NA@NA@9:8@9:9@||@",
            "1:1@1:2@NA@1:3@1:4@NA@NA@1:7@1:8@NA@1:9@1:10@1:11@||@2:1@2:2@NA@2:3@2:4@NA@NA@2:7@2:8@NA@2:9@2:10@2:11@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@3:8@NA@3:9@3:10@3:11@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@4:8@NA@4:9@4:10@4:11@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@5:8@NA@5:9@5:10@5:11@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@6:8@NA@6:9@6:10@6:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@7:8@NA@7:9@7:10@7:11@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@8:8@NA@8:9@8:10@8:11@||@NA@NA@NA@9:3@9:4@9:5@9:6@9:7@9:8@NA@9:9@9:10@9:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@NA@10:9@10:10@10:11@||@11:1@11:2@NA@11:3@11:4@11:5@11:6@11:7@11:8@NA@11:9@11:10@11:11@||@",
            "1:1@1:2@1:3@1:4@||@NA@2:2@2:3@NA@||@3:1@3:2@3:3@3:4@||@4:1@NA@NA@4:4@||@5:1@5:2@5:3@5:4@||@NA@6:2@6:3@NA@||@NA@7:2@7:3@NA@||@8:1@8:2@8:3@8:4@||@9:1@NA@NA@9:4@||@10:1@10:2@10:3@10:4@||@NA@11:2@11:3@NA@||@",
            "1:1@1:2@NA@1:4@1:5@||@2:1@NA@2:3@NA@2:5@||@3:1@NA@3:3@NA@3:5@||@4:1@4:2@NA@4:4@4:5@||@5:1@5:2@NA@5:4@5:5@||@6:1@6:2@NA@6:4@6:5@||@7:1@NA@7:3@NA@7:5@||@8:1@NA@8:3@NA@8:5@||@9:1@9:2@NA@9:4@9:5@||@10:1@10:2@NA@10:4@10:5@||@NA@11:2@11:3@11:4@NA@||@NA@12:2@NA@12:4@NA@||@NA@13:2@13:3@13:4@NA@||@14:1@14:2@14:3@14:4@14:5@||@15:1@NA@NA@NA@15:5@||@16:1@NA@NA@NA@16:5@||@17:1@NA@NA@NA@17:5@||@18:1@18:2@18:3@18:4@18:5@||@",
            "1:1@1:2@1:3@1:4@1:5@1:6@1:7@||@2:1@2:2@2:3@NA@2:5@2:6@2:7@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@||@4:1@NA@4:3@NA@4:5@NA@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@6:2@6:3@NA@6:5@6:6@6:7@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@||@",
            "1:1@1:2@1:3@1:4@||@2:1@NA@NA@2:4@||@3:1@NA@NA@3:4@||@4:1@4:2@4:3@4:4@||@",
            "1:1@1:2@NA@1:3@1:4@NA@NA@1:7@1:8@NA@1:9@1:10@1:11@||@2:1@2:2@NA@2:3@2:4@NA@NA@2:7@2:8@NA@2:9@2:10@2:11@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@3:8@NA@3:9@3:10@3:11@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@4:8@NA@4:9@4:10@4:11@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@5:8@NA@5:9@5:10@5:11@||@6:1@6:2@NA@6:3@6:4@NA@NA@6:7@6:8@NA@6:9@6:10@6:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@NA@NA@7:9@7:10@7:11@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@NA@NA@8:9@8:10@8:11@||@NA@NA@NA@9:3@9:4@9:5@9:6@9:7@NA@NA@9:9@9:10@9:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@NA@10:9@10:10@10:11@||@11:1@11:2@NA@11:3@11:4@11:5@11:6@11:7@11:8@NA@11:9@11:10@11:11@||@",
            "NA@NA@NA@NA@NA@1:6@1:7@1:8@1:9@1:10@1:11@NA@1:12@1:13@NA@||@NA@NA@NA@NA@NA@2:6@2:7@2:8@2:9@2:10@2:11@NA@2:12@2:13@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@NA@3:12@3:13@NA@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@NA@4:12@4:13@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@NA@5:12@5:13@5:14@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@NA@6:12@6:13@6:14@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@NA@7:12@7:13@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@NA@8:12@8:13@NA@||@NA@NA@NA@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@NA@9:12@9:13@NA@||@",
            "1:1@1:2@1:3@1:4@1:5@1:6@1:7@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@||@3:1@NA@NA@3:4@NA@NA@3:7@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@NA@NA@NA@NA@NA@6:7@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@||@",
            "NA@NA@NA@NA@NA@NA@1:6@1:7@1:8@1:9@NA@1:10@1:11@1:12@1:13@1:14@||@NA@NA@2:3@2:4@2:5@NA@2:6@2:7@2:8@2:9@NA@2:10@2:11@2:12@2:13@2:14@||@3:1@3:2@3:3@3:4@3:5@NA@3:6@3:7@3:8@3:9@NA@3:10@3:11@3:12@3:13@3:14@||@4:1@4:2@4:3@4:4@4:5@NA@4:6@4:7@4:8@4:9@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@5:4@5:5@NA@5:6@5:7@5:8@5:9@NA@5:10@5:11@5:12@5:13@5:14@||@6:1@6:2@6:3@6:4@6:5@NA@6:6@6:7@6:8@6:9@NA@6:10@6:11@6:12@6:13@6:14@||@NA@NA@7:3@7:4@7:5@NA@7:6@7:7@7:8@7:9@NA@7:10@7:11@7:12@7:13@7:14@||@NA@NA@8:3@8:4@8:5@NA@8:6@8:7@8:8@8:9@NA@8:10@8:11@8:12@8:13@8:14@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@NA@1:13@1:14@1:15@1:16@||@NA@NA@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@2:13@2:14@2:15@2:16@||@NA@NA@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@NA@3:13@3:14@3:15@3:16@||@NA@NA@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@NA@4:13@4:14@4:15@4:16@||@NA@NA@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@5:13@5:14@5:15@5:16@||@NA@NA@6:3@6:4@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@6:13@6:14@6:15@6:16@||@NA@NA@7:3@7:4@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@7:13@7:14@7:15@7:16@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@NA@8:13@8:14@8:15@8:16@||@NA@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@NA@9:13@9:14@9:15@9:16@||@NA@10:2@10:3@10:4@NA@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@NA@10:13@10:14@NA@NA@||@",
            "NA@NA@NA@NA@NA@1:6@1:7@1:8@1:9@1:10@1:11@NA@1:12@1:13@NA@||@NA@NA@NA@NA@NA@2:6@2:7@2:8@2:9@2:10@2:11@NA@2:12@2:13@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@NA@3:12@3:13@NA@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@NA@4:12@4:13@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@NA@5:12@5:13@5:14@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@NA@6:12@6:13@6:14@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@NA@7:12@7:13@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@NA@8:12@8:13@NA@||@NA@NA@NA@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@NA@9:12@9:13@NA@||@",
            "1:1@1:2@1:3@NA@1:4@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@1:15@NA@1:16@1:17@1:18@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@NA@2:16@2:17@2:18@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@NA@3:16@3:17@3:18@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@NA@4:16@4:17@4:18@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@NA@5:16@5:17@5:18@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@NA@6:16@6:17@6:18@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@NA@7:16@7:17@7:18@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@NA@8:16@8:17@8:18@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@NA@9:16@9:17@9:18@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@10:15@NA@10:16@10:17@10:18@||@",
            "1:1@1:2@1:3@1:4@1:5@1:6@1:7@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@||@3:1@NA@NA@3:4@NA@NA@3:7@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@NA@NA@NA@NA@NA@6:7@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@NA@1:14@1:15@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@NA@2:14@2:15@2:16@2:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@NA@3:14@3:15@3:16@3:17@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@NA@4:14@4:15@4:16@4:17@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@5:17@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@6:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@NA@||@NA@NA@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@NA@NA@||@NA@NA@NA@NA@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@NA@NA@NA@NA@||@4:17@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@||@5:17@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@||@6:17@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@||@7:17@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@8:16@||@8:17@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@9:14@9:15@9:16@||@9:17@",
            "NA@NA@NA@NA@1:4@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@1:14@1:15@NA@NA@NA@NA@||@NA@NA@NA@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@2:14@2:15@NA@NA@NA@NA@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@3:14@3:15@NA@3:16@3:17@3:18@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@4:14@4:15@NA@4:16@4:17@4:18@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@5:14@5:15@NA@5:16@5:17@5:18@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@6:14@6:15@NA@6:16@6:17@6:18@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@NA@NA@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@7:14@7:15@NA@NA@7:17@7:18@||@NA@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@8:14@8:15@NA@NA@NA@NA@||@NA@NA@NA@NA@9:4@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@9:14@9:15@NA@NA@NA@NA@||@10:1@NA@NA@NA@10:4@10:5@10:6@10:7@10:8@10:9@10:10@10:11@10:12@10:13@10:14@10:15@NA@NA@NA@10:18@||@",
            "1:1@1:2@NA@1:4@1:5@NA@1:7@1:8@NA@1:10@1:11@||@2:1@2:2@2:3@2:4@2:5@NA@2:7@2:8@2:9@2:10@2:11@||@3:1@NA@3:3@NA@3:5@NA@3:7@NA@3:9@NA@3:11@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@NA@1:13@1:14@1:15@1:16@||@NA@NA@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@2:13@2:14@2:15@2:16@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@NA@3:13@3:14@3:15@3:16@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@NA@4:13@4:14@4:15@4:16@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@5:13@5:14@5:15@5:16@||@6:1@6:2@6:3@6:4@NA@NA@NA@6:7@6:8@6:9@6:10@6:11@6:12@NA@6:13@6:14@6:15@6:16@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@NA@7:13@7:14@7:15@7:16@||@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@NA@8:13@8:14@NA@NA@||@9:1@9:2@9:3@9:4@NA@NA@NA@9:7@9:8@9:9@9:10@9:11@9:12@NA@9:13@9:14@NA@NA@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@NA@1:14@1:15@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@NA@2:14@2:15@2:16@2:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@NA@3:14@3:15@3:16@3:17@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@NA@4:14@4:15@4:16@4:17@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@5:17@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@6:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@NA@||@NA@NA@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@NA@NA@||@NA@NA@NA@NA@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@NA@NA@NA@NA@||@4:17@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@||@5:17@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@||@6:17@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@||@7:17@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@8:16@||@8:17@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@9:14@9:15@9:16@||@9:17@",
            "NA@NA@NA@1:4@1:5@1:6@1:7@1:8@1:9@1:10@1:11@NA@1:12@1:13@1:14@1:15@||@NA@NA@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@NA@2:12@2:13@2:14@2:15@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@NA@3:12@3:13@3:14@3:15@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@NA@4:12@4:13@4:14@4:15@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@NA@NA@NA@NA@NA@||@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@NA@6:12@6:13@6:14@6:15@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@NA@7:12@7:13@7:14@7:15@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@NA@NA@1:4@1:5@1:6@1:7@1:8@1:9@1:10@1:11@NA@1:12@1:13@1:14@1:15@||@NA@NA@NA@2:4@2:5@2:6@2:7@2:8@2:9@2:10@2:11@NA@2:12@2:13@2:14@2:15@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@NA@3:12@3:13@3:14@3:15@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@NA@4:12@4:13@4:14@4:15@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@NA@NA@NA@NA@NA@||@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@6:11@NA@6:12@6:13@6:14@6:15@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@7:10@7:11@NA@7:12@7:13@7:14@7:15@||@",
            "1:1@1:2@NA@NA@1:4@1:5@1:6@NA@NA@1:8@1:9@||@2:1@2:2@NA@2:3@2:4@2:5@2:6@2:7@NA@2:8@2:9@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@NA@3:8@3:9@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@NA@4:8@4:9@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@NA@5:8@5:9@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@NA@6:8@6:9@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@NA@7:8@7:9@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@NA@8:8@8:9@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@NA@9:8@9:9@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@NA@10:8@10:9@||@11:1@NA@NA@11:3@NA@NA@NA@11:7@NA@NA@11:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@12:1@12:2@NA@12:3@12:4@12:5@12:6@12:7@NA@12:8@12:9@||@13:1@13:2@NA@13:3@13:4@13:5@13:6@13:7@NA@13:8@13:9@||@14:1@14:2@NA@14:3@NA@NA@NA@14:7@NA@14:8@14:9@||@15:1@15:2@NA@15:3@15:4@15:5@15:6@15:7@NA@15:8@15:9@||@",
            "1:1@1:2@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@2:1@2:2@NA@NA@NA@2:5@2:6@2:7@NA@NA@NA@2:10@2:11@||@3:1@3:2@NA@NA@3:4@3:5@3:6@3:7@3:8@NA@NA@3:10@3:11@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@4:8@4:9@NA@4:10@4:11@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@5:8@5:9@NA@5:10@5:11@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@NA@6:10@6:11@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@7:10@7:11@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@8:8@8:9@NA@8:10@8:11@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@9:8@9:9@NA@9:10@9:11@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@10:8@10:9@NA@10:10@10:11@||@11:1@11:2@NA@11:3@11:4@NA@NA@NA@11:8@11:9@NA@11:10@11:11@||@11:1@11:2@NA@11:3@NA@NA@NA@NA@NA@11:9@NA@11:10@11:11@||@11:1@11:2@NA@11:3@NA@11:5@11:6@11:7@NA@11:9@NA@11:10@11:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@12:1@12:2@NA@NA@12:4@12:5@12:6@12:7@12:8@NA@NA@12:10@12:11@||@13:1@13:2@NA@13:3@13:4@13:5@13:6@13:7@13:8@13:9@NA@13:10@13:11@||@14:1@14:2@NA@14:3@14:4@14:5@14:6@14:7@14:8@14:9@NA@14:10@14:11@||@",
            "NA@1:2@1:3@NA@1:4@1:5@1:6@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@||@NA@4:2@4:3@NA@4:4@4:5@4:6@||@NA@5:2@5:3@NA@5:4@5:5@5:6@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@||@NA@NA@NA@NA@NA@NA@NA@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@||@11:1@11:2@11:3@NA@11:4@11:5@11:6@||@12:1@12:2@12:3@NA@12:4@12:5@NA@||@13:1@13:2@13:3@NA@13:4@13:5@NA@||@14:1@14:2@14:3@NA@14:4@14:5@14:6@||@NA@NA@NA@NA@15:4@15:5@15:6@||@",
            "1:1@1:2@NA@NA@1:5@NA@NA@1:8@1:9@||@2:1@2:2@NA@NA@2:5@NA@NA@2:8@2:9@||@3:1@3:2@NA@NA@3:5@NA@NA@3:8@3:9@||@4:1@4:2@NA@NA@4:5@NA@NA@4:8@4:9@||@5:1@5:2@NA@5:4@5:5@5:6@NA@5:8@5:9@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@NA@7:4@7:5@7:6@NA@7:8@7:9@||@8:1@8:2@NA@NA@NA@NA@NA@8:8@8:9@||@9:1@9:2@NA@NA@NA@NA@NA@9:8@9:9@||@",
            "1:1@1:2@NA@NA@1:5@NA@NA@1:8@1:9@||@2:1@2:2@NA@NA@2:5@NA@NA@2:8@2:9@||@3:1@3:2@NA@NA@3:5@NA@NA@3:8@3:9@||@4:1@4:2@NA@NA@4:5@NA@NA@4:8@4:9@||@5:1@5:2@NA@5:4@5:5@5:6@NA@5:8@5:9@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@NA@7:4@7:5@7:6@NA@7:8@7:9@||@8:1@8:2@NA@NA@NA@NA@NA@8:8@8:9@||@9:1@9:2@NA@NA@NA@NA@NA@9:8@9:9@||@",
            "NA@NA@1:3@NA@1:4@1:5@1:6@NA@1:7@1:8@1:9@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@NA@2:7@2:8@2:9@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@NA@3:7@3:8@3:9@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@NA@4:7@4:8@4:9@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@NA@5:7@5:8@5:9@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@NA@6:7@6:8@6:9@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@NA@7:7@7:8@7:9@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@NA@8:7@8:8@8:9@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@NA@9:7@9:8@9:9@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@NA@10:7@10:8@10:9@||@11:1@11:2@11:3@NA@11:4@11:5@11:6@NA@11:7@11:8@11:9@||@NA@12:2@12:3@NA@12:4@12:5@12:6@NA@12:7@12:8@12:9@||@NA@13:2@13:3@NA@13:4@13:5@13:6@NA@13:7@13:8@13:9@||@14:1@14:2@14:3@NA@14:4@14:5@14:6@NA@14:7@14:8@14:9@||@15:1@15:2@15:3@NA@NA@NA@NA@NA@15:7@15:8@15:9@||@16:1@16:2@16:3@NA@NA@NA@NA@NA@16:7@16:8@16:9@||@",
            "1:1@1:2@1:3@1:4@1:5@1:6@1:7@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@||@3:1@NA@NA@3:4@NA@NA@3:7@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@NA@NA@NA@NA@NA@6:7@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@||@",
            "1:1@NA@1:2@NA@1:3@NA@1:4@||@NA@NA@NA@NA@NA@NA@NA@||@2:1@NA@2:2@NA@2:3@NA@2:4@||@NA@NA@NA@NA@NA@NA@NA@||@3:1@NA@3:2@NA@3:3@NA@3:4@||@NA@NA@NA@NA@NA@NA@NA@||@4:1@NA@4:2@NA@4:3@NA@4:4@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@NA@NA@NA@NA@NA@1:13@1:14@NA@NA@||@NA@NA@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@NA@2:13@2:14@NA@NA@||@NA@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@NA@3:13@3:14@NA@NA@||@NA@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@NA@4:13@4:14@NA@NA@||@NA@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@NA@5:13@5:14@NA@NA@||@NA@6:2@6:3@6:4@NA@NA@NA@6:7@6:8@6:9@6:10@6:11@6:12@NA@6:13@6:14@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@NA@7:13@7:14@NA@NA@||@NA@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@NA@8:13@8:14@NA@NA@||@NA@9:2@9:3@9:4@NA@NA@NA@9:7@9:8@9:9@9:10@9:11@9:12@NA@9:13@9:14@NA@NA@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@NA@1:14@1:15@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@NA@2:14@2:15@2:16@2:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@NA@3:14@3:15@3:16@3:17@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@NA@4:14@4:15@4:16@4:17@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@5:17@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@6:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@NA@||@NA@NA@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@NA@NA@||@NA@NA@NA@NA@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@NA@NA@NA@NA@||@4:17@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@||@5:17@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@||@6:17@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@||@7:17@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@8:16@||@8:17@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@9:14@9:15@9:16@||@9:17@",
            "NA@NA@NA@NA@1:5@1:6@1:7@1:8@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@2:8@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@||@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@||@NA@7:2@7:3@7:4@7:5@7:6@7:7@NA@||@NA@NA@8:3@8:4@8:5@8:6@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@NA@1:3@1:4@1:5@1:6@1:7@NA@NA@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@7:8@7:9@||@8:1@8:2@8:3@8:4@8:5@8:6@8:7@8:8@8:9@||@NA@9:2@9:3@9:4@9:5@9:6@9:7@9:8@NA@||@NA@NA@10:3@10:4@10:5@10:6@10:7@NA@NA@||@NA@NA@NA@11:4@11:5@11:6@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@NA@12:3@12:4@12:5@12:6@12:7@NA@NA@||@13:1@13:2@13:3@13:4@13:5@13:6@13:7@13:8@13:9@||@14:1@14:2@14:3@14:4@14:5@14:6@14:7@14:8@14:9@||@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@",
            "NA@1:2@1:3@NA@1:4@1:5@1:6@NA@NA@NA@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@2:7@NA@NA@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@NA@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@4:9@||@NA@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@5:9@||@NA@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@7:7@7:8@7:9@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@8:7@8:8@8:9@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@9:7@9:8@9:9@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@10:7@10:8@10:9@||@11:1@11:2@11:3@NA@NA@11:5@11:6@11:7@11:8@11:9@||@NA@12:2@12:3@NA@NA@NA@12:6@12:7@12:8@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@13:1@13:2@13:3@NA@13:4@13:5@13:6@13:7@13:8@13:9@||@14:1@14:2@14:3@NA@14:4@14:5@14:6@14:7@14:8@14:9@||@15:1@15:2@15:3@NA@15:4@15:5@15:6@15:7@15:8@15:9@||@",
            "1:1@1:2@NA@NA@1:4@1:5@1:6@NA@NA@1:8@1:9@||@2:1@2:2@NA@2:3@2:4@2:5@2:6@2:7@NA@2:8@2:9@||@3:1@3:2@NA@3:3@3:4@3:5@3:6@3:7@NA@3:8@3:9@||@4:1@4:2@NA@4:3@4:4@4:5@4:6@4:7@NA@4:8@4:9@||@5:1@5:2@NA@5:3@5:4@5:5@5:6@5:7@NA@5:8@5:9@||@6:1@6:2@NA@6:3@6:4@6:5@6:6@6:7@NA@6:8@6:9@||@7:1@7:2@NA@7:3@7:4@7:5@7:6@7:7@NA@7:8@7:9@||@8:1@8:2@NA@8:3@8:4@8:5@8:6@8:7@NA@8:8@8:9@||@9:1@9:2@NA@9:3@9:4@9:5@9:6@9:7@NA@9:8@9:9@||@10:1@10:2@NA@10:3@10:4@10:5@10:6@10:7@NA@10:8@10:9@||@11:1@NA@NA@11:3@NA@NA@NA@11:7@NA@NA@11:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@12:1@12:2@NA@12:3@12:4@12:5@12:6@12:7@NA@12:8@12:9@||@13:1@13:2@NA@13:3@13:4@13:5@13:6@13:7@NA@13:8@13:9@||@14:1@14:2@NA@14:3@NA@NA@NA@14:7@NA@14:8@14:9@||@15:1@15:2@NA@15:3@15:4@15:5@15:6@15:7@NA@15:8@15:9@||@",
            "1:1@1:2@NA@NA@1:5@NA@NA@1:8@1:9@||@2:1@2:2@NA@NA@2:5@NA@NA@2:8@2:9@||@3:1@3:2@NA@NA@3:5@NA@NA@3:8@3:9@||@4:1@4:2@NA@NA@4:5@NA@NA@4:8@4:9@||@5:1@5:2@NA@5:4@5:5@5:6@NA@5:8@5:9@||@6:1@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@||@7:1@7:2@NA@7:4@7:5@7:6@NA@7:8@7:9@||@8:1@8:2@NA@NA@NA@NA@NA@8:8@8:9@||@9:1@9:2@NA@NA@NA@NA@NA@9:8@9:9@||@",
            "1:1@1:2@1:3@1:4@1:5@1:6@1:7@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@||@3:1@NA@NA@3:4@NA@NA@3:7@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@||@6:1@NA@NA@NA@NA@NA@6:7@||@7:1@7:2@7:3@7:4@7:5@7:6@7:7@||@",
            "1:1@1:2@1:3@NA@1:4@1:5@1:6@NA@1:7@1:8@1:9@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@NA@2:7@2:8@2:9@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@NA@3:7@3:8@3:9@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@NA@4:7@4:8@4:9@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@NA@5:7@5:8@5:9@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@NA@6:7@6:8@6:9@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@NA@7:7@7:8@7:9@||@8:1@8:2@8:3@NA@8:4@8:5@8:6@NA@8:7@8:8@8:9@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@NA@9:7@9:8@9:9@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@NA@10:7@10:8@10:9@||@",
            "1:1@NA@1:2@NA@1:3@NA@1:4@||@NA@NA@NA@NA@NA@NA@NA@||@2:1@NA@2:2@NA@2:3@NA@2:4@||@NA@NA@NA@NA@NA@NA@NA@||@3:1@NA@3:2@NA@3:3@NA@3:4@||@NA@NA@NA@NA@NA@NA@NA@||@4:1@NA@4:2@NA@4:3@NA@4:4@||@",
            "NA@1:2@1:3@NA@1:4@1:5@1:6@NA@NA@||@2:1@2:2@2:3@NA@2:4@2:5@2:6@NA@NA@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@||@7:1@7:2@7:3@NA@NA@7:5@7:6@7:7@7:8@||@8:1@8:2@8:3@NA@NA@8:5@8:6@8:7@8:8@||@9:1@9:2@9:3@NA@9:4@9:5@9:6@9:7@9:8@||@10:1@10:2@10:3@NA@10:4@10:5@10:6@10:7@10:8@||@11:1@11:2@11:3@NA@11:4@11:5@11:6@11:7@11:8@||@NA@12:2@12:3@NA@12:4@12:5@12:6@12:7@12:8@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@13:1@13:2@13:3@NA@13:4@13:5@13:6@13:7@13:8@||@14:1@14:2@14:3@NA@14:4@14:5@14:6@14:7@14:8@||@NA@15:2@15:3@NA@15:4@15:5@NA@NA@NA@||@",
            "NA@NA@1:3@1:4@1:5@1:6@1:7@NA@NA@NA@NA@1:11@1:12@||@2:1@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@NA@NA@2:11@2:12@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@NA@NA@3:11@3:12@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@NA@NA@4:11@4:12@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@NA@NA@5:11@5:12@||@NA@NA@6:3@6:4@6:5@6:6@6:7@6:8@6:9@NA@NA@6:11@6:12@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@7:10@7:11@7:12@||@NA@NA@8:3@8:4@8:5@8:6@8:7@NA@NA@NA@8:10@8:11@8:12@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@9:10@9:11@9:12@||@",
            "NA@NA@NA@NA@1:4@1:5@1:6@1:7@NA@NA@1:9@1:10@1:11@||@NA@NA@NA@NA@2:4@2:5@2:6@2:7@2:8@NA@2:9@2:10@2:11@||@3:1@3:2@3:3@NA@3:4@3:5@3:6@3:7@3:8@NA@3:9@3:10@3:11@||@4:1@4:2@4:3@NA@4:4@4:5@4:6@4:7@4:8@NA@4:9@4:10@4:11@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@5:1@5:2@5:3@NA@5:4@5:5@5:6@5:7@5:8@NA@5:9@5:10@5:11@||@6:1@6:2@6:3@NA@6:4@6:5@6:6@6:7@6:8@NA@6:9@6:10@6:11@||@7:1@7:2@7:3@NA@7:4@7:5@7:6@7:7@7:8@NA@7:9@7:10@7:11@||@",
            "NA@NA@1:3@1:4@NA@1:5@1:6@1:7@1:8@1:9@1:10@1:11@1:12@1:13@NA@1:14@1:15@NA@NA@||@2:1@2:2@2:3@2:4@NA@2:5@2:6@2:7@2:8@2:9@2:10@2:11@2:12@2:13@NA@2:14@2:15@2:16@2:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@3:1@3:2@3:3@3:4@NA@3:5@3:6@3:7@3:8@3:9@3:10@3:11@3:12@3:13@NA@3:14@3:15@3:16@3:17@||@4:1@4:2@4:3@4:4@NA@4:5@4:6@4:7@4:8@4:9@4:10@4:11@4:12@4:13@NA@4:14@4:15@4:16@4:17@||@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@5:17@||@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@6:17@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@NA@||@NA@NA@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@NA@NA@||@NA@NA@NA@NA@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@NA@NA@NA@NA@||@4:17@5:1@5:2@5:3@5:4@NA@5:5@5:6@5:7@5:8@5:9@5:10@5:11@5:12@5:13@NA@5:14@5:15@5:16@||@5:17@6:1@6:2@6:3@6:4@NA@6:5@6:6@6:7@6:8@6:9@6:10@6:11@6:12@6:13@NA@6:14@6:15@6:16@||@6:17@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@NA@7:1@7:2@7:3@7:4@NA@7:5@7:6@7:7@7:8@7:9@7:10@7:11@7:12@7:13@NA@7:14@7:15@7:16@||@7:17@8:1@8:2@8:3@8:4@NA@8:5@8:6@8:7@8:8@8:9@8:10@8:11@8:12@8:13@NA@8:14@8:15@8:16@||@8:17@9:1@9:2@9:3@9:4@NA@9:5@9:6@9:7@9:8@9:9@9:10@9:11@9:12@9:13@NA@9:14@9:15@9:16@||@9:17@",
            "NA@NA@1:3@1:4@1:5@NA@1:7@1:8@1:9@NA@NA@||@NA@2:2@2:3@2:4@2:5@2:6@2:7@2:8@2:9@2:10@NA@||@3:1@3:2@3:3@3:4@3:5@3:6@3:7@3:8@3:9@3:10@3:11@||@4:1@4:2@4:3@4:4@4:5@4:6@4:7@4:8@4:9@4:10@4:11@||@5:1@5:2@5:3@5:4@5:5@5:6@5:7@5:8@5:9@5:10@5:11@||@NA@6:2@6:3@6:4@6:5@6:6@6:7@6:8@6:9@6:10@NA@||@NA@NA@7:3@7:4@7:5@7:6@7:7@7:8@7:9@NA@NA@||@NA@NA@NA@8:4@8:5@8:6@8:7@8:8@NA@NA@NA@||@NA@NA@NA@NA@9:5@9:6@9:7@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@10:6@NA@NA@NA@NA@NA@||@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@NA@||@"
        }; //座位資料區

        private List<string> type = new List<string>()
        {
            "數位",
            "IMAX",
            "3D",
            "4DX"
        };


        public IActionResult makeCinemaSeat()
        {
            int maxcount = seatInfolist.Count();
            int typevalue = 0;
            List<電影院theater> Theaters = this._dbContext.電影院theaters.ToList();
            List<int> takevalue = fn_隨機取不同亂數(20, Theaters.Count());

            var q = this._dbContext.影廳cinemas;
            for (int x = 0; x < 8; x++)
            {
                foreach (int i in takevalue)
                {
                    typevalue = ran.Next(3);
                    影廳cinema cinema = new 影廳cinema()
                    {
                        影廳名稱cinemaName = $"{type[typevalue]}廳",
                        電影院編號theaterId = Theaters[i].電影院編號theaterId,
                        廳種名稱cinemaClsName = type[typevalue],
                        座位資訊seatInfo = seatInfolist[ran.Next(maxcount)],
                    };
                    this._dbContext.影廳cinemas.Add(cinema);
                }
            }



            this._dbContext.SaveChanges();
            return View();
        }

        public IActionResult makeScreen()
        {
            List<電影院theater> Theaters = this._dbContext.電影院theaters.ToList();
            List<電影代碼movieCode> MovieCodes = this._dbContext.電影代碼movieCodes.ToList();
            DateTime today = DateTime.Now;
            var ScreenDB = this._dbContext.場次screenings;
            var SeatInfo = this._dbContext.出售座位狀態seatStatuses;
            int maxvalue = MovieCodes.Count();

            List<int> codes = new List<int>();
            List<影廳cinema> Cinemas = new List<影廳cinema>();
            List<string> 時間表 = new List<string>();
            DateTime filedate = new DateTime();
            int runtime = 0;
            foreach (電影院theater theater in Theaters)
            {
                Cinemas = this._dbContext.影廳cinemas.Where(c => c.電影院編號theaterId == theater.電影院編號theaterId).ToList();
                codes = fn_隨機取不同亂數(Cinemas.Count(), maxvalue);
                filedate = today.Date;
                for (int i = 1; i <= 6; i++)
                {
                    filedate = filedate.AddDays(1.0);
                    for (int j = 0; j < Cinemas.Count(); j++)
                    {
                        runtime = MovieCodes.Where(m => m.電影代碼編號movieCodeId == codes[j]).Select(m => m.電影編號movie.片長runtime).FirstOrDefault();
                        時間表 = fn_設定場次時間(runtime, filedate);

                        foreach (string item in 時間表)
                        {
                            ScreenDB = this._dbContext.場次screenings;
                            場次screening screens = new 場次screening()
                            {
                                影廳編號cinemaId = Cinemas[j].影廳編號cinemaId,
                                電影代碼movieCode = codes[j],
                                放映日期playDate = filedate.Date,
                                放映開始時間playTime = item,
                            };
                            ScreenDB.Add(screens);
                            this._dbContext.SaveChanges();
                            SeatInfo = this._dbContext.出售座位狀態seatStatuses;
                            出售座位狀態seatStatus seat = new 出售座位狀態seatStatus()
                            {
                                場次編號screeningId = this._dbContext.場次screenings.OrderBy(s => s.場次編號screeningId).LastOrDefault().場次編號screeningId,
                                出售座位資訊seatSoldInfo = Cinemas[j].座位資訊seatInfo,
                            };
                            SeatInfo.Add(seat);
                            this._dbContext.SaveChanges();
                        }
                    }
                }
            }
            return View();
        }


        [NonAction]
        public List<string> fn_設定場次時間(int runtime, DateTime date)
        {
            date = date.Date;
            List<string> start_time = new List<string>();
            int Movietime = runtime;
            while (runtime - Movietime < 15 || runtime % 5 != 0)
            {
                runtime++;
            }
            DateTime Movie_Start_Time = date.AddHours(7.0);
            TimeSpan span = new TimeSpan();
            while (span.TotalDays <= 1.0)
            {
                Movie_Start_Time = Movie_Start_Time.AddMinutes((double)runtime);
                start_time.Add(Movie_Start_Time.ToString("HH:mm"));
                span = Movie_Start_Time.Subtract(date);
            }
            return start_time;
        }
        private List<int> fn_隨機取不同亂數(int count, int MaxValue)
        {
            if (count >= MaxValue)
                return null;
            List<int> nums = new List<int>();
            int value = 0;
            for (int i = 0; i < count; i++)
            {
                while (nums.Contains(value) || value == 0)
                {
                    value = ran.Next(1, MaxValue);
                }
                nums.Add(value);
            }
            return nums;
        }


        #endregion
    }
}
