using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;

namespace ClientMDA.Controllers
{
    public class MemberController : Controller
    {
        private readonly MDAContext _MDAcontext;
        private IWebHostEnvironment _enviro;
        public MemberController(MDAContext MDAcontext, IWebHostEnvironment p)  //相依性注入
        {
            _MDAcontext = MDAcontext;
            _enviro = p;
        }

        #region login-->login2 or signup-->logout
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            if(string.IsNullOrEmpty(vModel.txtphone))
            {
                vModel.txtphone = "";
            }
            HttpContext.Session.SetString(CDictionary.SK_USER_PHONE, vModel.txtphone);

            bool isExist = _MDAcontext.會員members.Any(m => m.會員電話cellphone == vModel.txtphone);
            if (isExist)
                return RedirectToAction("Login2");
            else
                return RedirectToAction("SignUp");//, new { phone = vModel.txtphone }
        }
        public IActionResult Login2(/*string phone*/)
        {
            //ViewBag.phone = phone;
            ViewBag.phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            //CLogin2ViewModel mem = new CLogin2ViewModel();
            //mem.txtphone = phone;
            return View();
        }
        [HttpPost]
        public IActionResult Login2(CLogin2ViewModel vModel)
        {
            會員member mem = _MDAcontext.會員members.FirstOrDefault(t => t.會員電話cellphone.Equals(vModel.txtPhone));
            if (mem != null && mem.密碼password.Equals(vModel.txtPassword))
            {
                //登入成功存入session
                string jsonUser = JsonSerializer.Serialize(mem);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);

                //轉跳原頁面
                string page = HttpContext.Session.GetString(CDictionary.SK登後要前往的頁面);
                if (!string.IsNullOrEmpty(page))
                {
                    HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "");
                    return Redirect(page);
                }
                else
                    return RedirectToAction("MemberMain");
            }

            else
            {
                ViewBag.phone = vModel.txtPhone;
                ViewBag.txtError = false;
                return View();
            }
        }
        public IActionResult SignUp()
        {
            ViewBag.phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            return View();
        }
        [HttpPost]
        public IActionResult SignUp(CSignUpViewModel vModel)
        {
            會員member m = new 會員member();
            m.會員電話cellphone = vModel.txtPhone;
            m.電子信箱email = vModel.txtEmail;
            m.密碼password = vModel.txtPassword;
            m.會員權限permission = 0;
            m.紅利點數bonus = 0;
            m.正式會員formal = false;
            _MDAcontext.會員members.Add(m);
            _MDAcontext.SaveChanges();
            片單總表movieList l = new 片單總表movieList();
            l.片單總表名稱listName = "我的片單(預設)";
            l.會員編號memberId = _MDAcontext.會員members.Where(m => m.會員電話cellphone == vModel.txtPhone).Select(m => m.會員編號memberId).FirstOrDefault();
            _MDAcontext.片單總表movieLists.Add(l);
            _MDAcontext.SaveChanges();

            string jsonUser = JsonSerializer.Serialize(m);
            HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);

            優惠明細couponList coupon = new 優惠明細couponList()
            {
                會員編號memberId = this._MDAcontext.會員members.OrderBy(m => m.會員編號memberId).LastOrDefault().會員編號memberId,
                優惠編號couponId=1,
                是否使用優惠oxCouponUsing=false,
                訂單編號orderId=0,
            };

            this._MDAcontext.優惠明細couponLists.Add(coupon);
            this._MDAcontext.SaveChanges();
            string page = HttpContext.Session.GetString(CDictionary.SK登後要前往的頁面);
            if (!string.IsNullOrEmpty(page))
            {
                HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "");
                return Redirect(page);
            }
            else
                return RedirectToAction("MemberMain");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
            HttpContext.Session.Remove(CDictionary.SK_USER_PHONE);
            HttpContext.Session.Remove(CDictionary.SK_FORGETPASSWORD);
            HttpContext.Session.Remove(CDictionary.SK登後要前往的頁面);
            return View();
        }

        #endregion

        #region forgePwd
        public ActionResult checkValidatePic(string code)
        {
            string picCode = HttpContext.Session.GetString(CDictionary.SK_PICTURECODE);
            if (code == picCode)
            {
                return Content("T", "text/plain");
            }
            else
            {
                return Content("F", "text/plain");
            }
        }

        public IActionResult Login2ForgetPwd()
        {
            ViewBag.phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            sendmail();
            return View();
        }
        [HttpPost]
        public IActionResult Login2ForgetPwd(CLogin2ViewModel vModel)
        {
            ViewBag.phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            string Code = HttpContext.Session.GetString(CDictionary.SK_FORGETPASSWORD);

            if (Code != null)
            {
                if (Code == vModel.txtPassword)
                {
                    ViewBag.txtError = true;
                    return RedirectToAction("Login2ResetPwd");
                }
                else
                {
                    ViewBag.txtError = false;
                    return View();
                }
            }
            return View();
        }
        public IActionResult Login2ResetPwd()
        {
            string phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            ViewBag.memId = _MDAcontext.會員members.Where(m => m.會員電話cellphone == phone).Select(m => m.會員編號memberId).FirstOrDefault();
            ViewBag.phone = phone;
            return View();
        }
        [HttpPost]
        public IActionResult Login2ResetPwd(CPasswordViewModel vm)
        {
            會員member mem = _MDAcontext.會員members.FirstOrDefault(m => m.會員編號memberId == vm.memberId);
            if (mem != null)
            {
                mem.密碼password = vm.txt_new_password;
                _MDAcontext.SaveChanges();

                string jsonUser = JsonSerializer.Serialize(mem);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);

                string page = HttpContext.Session.GetString(CDictionary.SK登後要前往的頁面);
                if (!string.IsNullOrEmpty(page))
                {
                    HttpContext.Session.SetString(CDictionary.SK登後要前往的頁面, "");
                    return Redirect(page);
                }
                else
                    return RedirectToAction("MemberMain");
            }
            else
            {
                ViewBag.txtError = false;
                ViewBag.phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);
            }

            return View();
        }

        public IActionResult sendmail()
        {
            string phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            string email = _MDAcontext.會員members.Where(m => m.會員電話cellphone == phone).Select(m => m.電子信箱email).FirstOrDefault();
            string name = "";
            string nick = _MDAcontext.會員members.Where(m => m.會員電話cellphone == phone).Select(m => m.暱稱nickName).FirstOrDefault();
            if (string.IsNullOrEmpty(nick))
                name = phone;
            else
                name = nick;

            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();

            Random ran = new Random();
            string rndPsw = RandomCode(8);
            HttpContext.Session.SetString(CDictionary.SK_FORGETPASSWORD, rndPsw);
            builder.HtmlBody = $"<p>{name}您好，重設密碼的驗證碼(8碼)為{rndPsw}</p>" +
                              $"<hr/>" +
                              $"<p>當前時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";

            message.From.Add(new MailboxAddress("MDA官網", "jo3wait@outlook.com"));
            message.To.Add(new MailboxAddress("親愛的顧客", "ilovemdaofficialok@gmail.com"));//email
            message.Subject = "MDA重設密碼驗證信";
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls); //587 TLS
                client.Authenticate("jo3wait@outlook.com", "msit143group3");
                client.Send(message);
                client.Disconnect(true);
            }
            return Json("send");
        }


        private string RandomCode(int length)
        {
            string s = "0123456789zxcvbnmasdfghjklqwertyuiop";
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int index;
            for (int i = 0; i < length; i++)
            {
                index = rand.Next(0, s.Length);
                sb.Append(s[index]);
            }
            return sb.ToString();
        }
        private void PaintInterLine(Graphics g, int num, int width, int height)
        {
            Random r = new Random();
            int startX, startY, endX, endY;
            for (int i = 0; i < num; i++)
            {
                startX = r.Next(0, width);
                startY = r.Next(0, height);
                endX = r.Next(0, width);
                endY = r.Next(0, height);
                g.DrawLine(new Pen(Brushes.Red), startX, startY, endX, endY);
            }
        }
        public ActionResult GetValidatePic()
        {
            byte[] data = null;
            string code = RandomCode(5);
            HttpContext.Session.SetString(CDictionary.SK_PICTURECODE, code);
            //TempData["code"] = code;
            //定義一個畫板
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(100, 40))
            {
                //畫筆,在指定畫板畫板上畫圖
                //g.Dispose();
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.White);
                    g.DrawString(code, new Font("黑體", 18.0F), Brushes.Blue, new Point(10, 8));
                    //繪製干擾線(數字代表幾條)
                    PaintInterLine(g, 10, map.Width, map.Height);
                }
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            data = ms.GetBuffer();
            return File(data, "image/jpeg");
        }
        public ActionResult queryEmail()
        {
            string phone = HttpContext.Session.GetString(CDictionary.SK_USER_PHONE);

            string data = _MDAcontext.會員members.Where(m => m.會員電話cellphone == phone).Select(m => m.電子信箱email).FirstOrDefault();
            return Content(data, "text/plain");
        }
        public ActionResult testValidatePic()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            else
                return View();
        }
        #endregion

        public IActionResult MemberMain()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            return View();
        }

        #region edit-member, password
        public IActionResult MemberEdit()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public IActionResult MemberEdit(CMemberDemoViewModel inputMember)
        {
            會員member mem = _MDAcontext.會員members.FirstOrDefault(m => m.會員編號memberId == inputMember.會員編號memberId);
            if (mem != null)
            {
                if (inputMember.memberPhoto != null)
                {
                    string photoName = "member" + mem.會員編號memberId + ".jpg";
                    mem.會員照片image = photoName;
                    string path = _enviro.WebRootPath + "/images/Member/" + photoName;
                    inputMember.memberPhoto.CopyTo(new FileStream(path, FileMode.Create));
                }
                if (!string.IsNullOrEmpty(inputMember.birthDate))
                {
                    DateTime bd = DateTime.ParseExact(inputMember.birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    mem.生日birthDate = bd;
                }
                mem.暱稱nickName = inputMember.暱稱nickName;
                mem.名字fName = inputMember.名字fName;
                mem.姓氏lName = inputMember.姓氏lName;
                mem.性別gender = inputMember.性別gender;
                mem.地址address = inputMember.地址address;
                if (mem.正式會員formal == false && inputMember.地址address != null && inputMember.暱稱nickName != null
                    && inputMember.性別gender != null && inputMember.birthDate != null && inputMember.名字fName != null
                    && inputMember.姓氏lName != null)
                {
                    mem.正式會員formal = true;
                }

                _MDAcontext.SaveChanges();

                string jsonUser = JsonSerializer.Serialize(mem);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);
            }
            return RedirectToAction("MemberEdit");
        }

        public IActionResult PasswordEdit()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpPost]
        public IActionResult PasswordEdit(CPasswordViewModel vModel)
        {
            會員member mem = _MDAcontext.會員members.FirstOrDefault(m => m.會員編號memberId == vModel.memberId);
            if (mem != null && mem.密碼password == vModel.txt_old_password)
            {
                mem.密碼password = vModel.txt_new_password;
                _MDAcontext.SaveChanges();

                string jsonUser = JsonSerializer.Serialize(mem);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);

                ViewBag.txtSuccess = "s";
            }
            else
            {
                ViewBag.txtError = false;

            }
            return View();
        }
        #endregion

        public IActionResult checkExist(string phone)
        {
            bool isExist = _MDAcontext.會員members.Any(m => m.會員電話cellphone == phone);
            return Content(isExist.ToString(), "text/plain");
        }

        public bool checkPsw(int id, string psw)
        {
            bool isPsw = _MDAcontext.會員members.Any(m => m.會員編號memberId == id && m.密碼password == psw);
            return isPsw;
        }

       

        #region coupon 
        public IActionResult MemberDiscount(string msg)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (a == null)
                return RedirectToAction("Login");
            else
            {
                ViewData.Clear();
                會員member mem = JsonSerializer.Deserialize<會員member>(a);
                var q = _MDAcontext.優惠明細couponLists.Where(c => c.會員編號memberId == mem.會員編號memberId).Select(c => new CCouponListViewModel
                {
                    memberId = c.會員編號memberId,
                    couponListId = c.優惠明細編號couponListId,
                    couponName = c.優惠編號coupon.優惠名稱couponName,
                    dueDate = c.優惠編號coupon.優惠截止日期couponDueDate,
                    diccount = c.優惠編號coupon.優惠折扣couponDiscount,
                    used = c.是否使用優惠oxCouponUsing

                }).OrderByDescending(c => c.dueDate).ToList();
                if (msg != null)
                    ViewBag.under = msg;
                return View(q);
            }
        }
        public IActionResult checkCoupon(string coupon)
        {
            string result = "";
            var q = _MDAcontext.優惠總表coupons.Where(c => c.優惠代碼couponCode == coupon).FirstOrDefault();
            if (q != null)
            {
                if (q.優惠截止日期couponDueDate < DateTime.Now)
                {
                    result = "ex";
                }
                else
                    result = q.優惠所需紅利bonusCost.ToString();
            }
            else
                result = "no";
            return Content(result, "text/plain");
        }
        public IActionResult AddCoupon(CKeywordViewModel model)
        {
            string alert = "";
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            var q = _MDAcontext.優惠總表coupons.Where(c => c.優惠代碼couponCode == model.txtKeyword).FirstOrDefault();
            if (q != null)
            {
                if (q.優惠所需紅利bonusCost <= mem.紅利點數bonus)
                {
                    var m = _MDAcontext.會員members.Where(i => i.會員編號memberId == mem.會員編號memberId).FirstOrDefault();
                    m.紅利點數bonus = m.紅利點數bonus - q.優惠所需紅利bonusCost;
                    string jsonUser = JsonSerializer.Serialize(m);
                    HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, jsonUser);

                    優惠明細couponList coupon = new 優惠明細couponList
                    {
                        會員編號memberId = mem.會員編號memberId,
                        優惠編號couponId = q.優惠編號couponId,
                        是否使用優惠oxCouponUsing = false,
                    };

                    _MDAcontext.優惠明細couponLists.Add(coupon);
                    _MDAcontext.SaveChanges();

                }
                else { alert = "不足"; }

                return RedirectToAction("MemberDiscount", new { msg = alert });
            }
            else
                return RedirectToAction("MemberDiscount");
        }
        #endregion


        public IActionResult OrderList()
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (a == null)
                return RedirectToAction("Login");
            else
            {
                會員member mem = JsonSerializer.Deserialize<會員member>(a);
                //List<COrderListViewModel> vmList = new List<COrderListViewModel>();
                var q = _MDAcontext.訂單總表orders.Where(o => o.會員編號memberId == mem.會員編號memberId).Select(i => new COrderListViewModel
                {
                    memberId = i.會員編號memberId,
                    orderDate = i.訂單時間orderTime,
                    orderId = i.訂單編號orderId,
                    status = i.訂單狀態編號orderStatus.訂單狀態orderStatusName,
                    tickets = i.訂單明細orderDetails.Select(d => d.張數count).ToList(),
                    ticketPrice = i.訂單明細orderDetails.Select(d => d.票價明細ticket.價格ticketPrice).ToList(),
                }).ToList();
                return View(q);
            }
        }
        public IActionResult NotFormal()
        {
            return View();
        }
       
        public IActionResult WishList() //followList
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (a == null)
                return RedirectToAction("Login");
            else
            {
                會員member mem = JsonSerializer.Deserialize<會員member>(a);
                var q = _MDAcontext.我的追蹤清單myFollowLists.Where(f => f.會員編號memberId == mem.會員編號memberId && f.追讚倒編號actionTypeId == 0).Select(f => new CFollowListViewModel
                {
                    memberId = f.會員編號memberId,
                    targetName = f.對象target.對象名稱targetName,
                    targetId = f.對象targetId,
                    connectId = f.連接編號connectId,
                    followMemName = _MDAcontext.會員members.Where(m => m.會員編號memberId == f.連接編號connectId).Select(m => m.暱稱nickName).FirstOrDefault(),
                    comments = _MDAcontext.電影評論movieComments
                        .Where(m => m.會員編號memberId == f.連接編號connectId)
                        .OrderByDescending(c => c.發佈時間commentTime)
                        .Select(c => new CWriteCommentViewModel {
                        CommentId=c.評論編號commentId,
                        comTitle=c.評論標題commentTitle,
                        })
                        .Take(3).ToList(),
                    followComTitle = _MDAcontext.電影評論movieComments.Where(c => c.評論編號commentId == f.連接編號connectId).Select(c => c.評論標題commentTitle).FirstOrDefault(),
                    replies = _MDAcontext.回覆樓數floors
                        .Where(c => c.評論編號commentId == f.連接編號connectId)
                        .OrderByDescending(r => r.發佈時間floorTime)
                        .Select(r => r.回覆內容floors)
                        .Take(3).ToList()
                }).ToList();

                return View(q);
            }
        }
        public IActionResult queryFollowMember(int id)
        {
            var q = _MDAcontext.會員members.FirstOrDefault(m => m.會員編號memberId == id);
            return Content(q.暱稱nickName, "text/plain", System.Text.Encoding.UTF8);
        }

        #region load cities
        //讀取所有城市資料
        public IActionResult City()
        {
            var cities = _MDAcontext.地址addresses.Select(a => a.City).Distinct();
            return Json(cities);

        }
        //依據城市名稱讀取鄉鎮區資料
        public IActionResult Site(string city)
        {
            var sites = _MDAcontext.地址addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(sites);

        }
        //依據城市名稱讀取鄉鎮區資料
        public IActionResult Road(string site)
        {
            var roads = _MDAcontext.地址addresses.Where(a => a.SiteId == site).Select(a => a.Road).Distinct();
            return Json(roads);

        }

        #endregion

        public IActionResult autoCmpMovie(string movie)
        {
            var movies = _MDAcontext.電影movies.Where(m => m.中文標題titleCht.Contains(movie) || m.英文標題titleEng.ToUpper().Contains(movie.ToUpper())).Select(p => p.中文標題titleCht);
            return Json(movies);

        }

        #region watchList
        public IActionResult WatchList()
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (a == null)
                return RedirectToAction("Login");
            else
            {
                會員member mem = JsonSerializer.Deserialize<會員member>(a);

                var q = _MDAcontext.片單總表movieLists.Where(m => m.會員編號memberId == mem.會員編號memberId).Select(m => new CMovieListViewModel
                {
                    memberId = m.會員編號memberId,
                    listId = m.片單總表編號movieListId,
                    listName = m.片單總表名稱listName,
                    myLists = _MDAcontext.我的片單myMovieLists.Where(l => l.片單總表編號movieListId == m.片單總表編號movieListId).Select(m => new CMovieListSubViewModel
                    {
                        listId = m.片單總表編號movieListId,
                        memberId = m.會員編號memberId,
                        myMovieListId = m.我的片單myMovieListId,
                        movieId = m.電影編號movieId,
                        movieTitle = m.電影編號movie.中文標題titleCht,
                        moviePic = m.電影編號movie.電影圖片movieIimagesLists.Select(c => c.圖片編號image.圖片雲端imageImdb).FirstOrDefault()
                    }).ToList(),


                }).ToList();

                return View(q);
            }

        }
       
        public IActionResult WatchListCreate(List<CWatchListCreateViewModel> ls)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);

            var q = _MDAcontext.片單總表movieLists.Where(l => l.會員編號memberId == mem.會員編號memberId);

            foreach (CWatchListCreateViewModel item in ls)
            {
                if (item.listId == 0)
                {
                    片單總表movieList ml = new 片單總表movieList();
                    ml.片單總表名稱listName = item.listName;
                    ml.會員編號memberId = mem.會員編號memberId;
                    _MDAcontext.片單總表movieLists.Add(ml);
                }
                else if (q.Any(l => l.片單總表編號movieListId == item.listId))
                {
                    var toEdit = q.First(l => l.片單總表編號movieListId == item.listId);
                    toEdit.片單總表名稱listName = item.listName;
                }
            }

            foreach (片單總表movieList list in q.ToList())
            {
                if (ls.Any(l => l.listId == list.片單總表編號movieListId) == false)
                {
                    var toDel = _MDAcontext.我的片單myMovieLists.Where(l => l.片單總表編號movieListId == list.片單總表編號movieListId);
                    _MDAcontext.我的片單myMovieLists.RemoveRange(toDel);

                    _MDAcontext.片單總表movieLists.Remove(list);
                }
            }

            _MDAcontext.SaveChanges();
            return RedirectToAction("WatchList");
        }
        public IActionResult MyWatchListEdit(List<CWatchListEditViewModel> mv)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            int listId = mv.First(m => m.listId != 0).listId;
            foreach (CWatchListEditViewModel item in mv)
            {
                if (item.movieId == 0)
                {
                    我的片單myMovieList ml = new 我的片單myMovieList();
                    ml.電影編號movieId = _MDAcontext.電影movies.First(m => m.中文標題titleCht == item.movieName).電影編號movieId;
                    ml.片單總表編號movieListId = listId;
                    ml.會員編號memberId = mem.會員編號memberId;
                    _MDAcontext.我的片單myMovieLists.Add(ml);
                }
            }
            var q = _MDAcontext.我的片單myMovieLists.Where(l => l.片單總表編號movieListId == listId);
            foreach (我的片單myMovieList item in q.ToList())
            {
                if (mv.Any(m => m.mylistId == item.我的片單myMovieListId) == false)
                {
                    _MDAcontext.我的片單myMovieLists.Remove(item);
                }
            }

            _MDAcontext.SaveChanges();
            return RedirectToAction("WatchList");
        }
        public IActionResult checkWatchList(string name)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            bool isExist = _MDAcontext.片單總表movieLists.Where(l => l.會員編號memberId == mem.會員編號memberId).Any(l => l.片單總表名稱listName == name);
            return Content(isExist.ToString(), "text/plain");
        }
        public IActionResult checkWatchListMoive(string name)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            bool isExist = _MDAcontext.我的片單myMovieLists.Where(l => l.片單總表編號movieList.會員編號memberId == mem.會員編號memberId).Any(l => l.電影編號movie.中文標題titleCht == name);
            return Content(isExist.ToString(), "text/plain");
        }


        #endregion

        #region comment
        public IActionResult CommentList(CKeywordViewModel model)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            else
            {
                var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                會員member mem = JsonSerializer.Deserialize<會員member>(a);
                IEnumerable<電影評論movieComment> datas = null;
                var q = _MDAcontext.電影評論movieComments.Where(c => c.會員編號memberId == mem.會員編號memberId);
                if (string.IsNullOrEmpty(model.txtKeyword))
                {
                    datas = q;
                    foreach (var item in datas)
                    {
                        item.評論內容comments = StripHTML(item.評論內容comments);
                    }
                }
                else
                {
                    datas = q.Where(c => c.評論標題commentTitle.Contains(model.txtKeyword));
                    foreach (var item in datas)
                    {
                        item.評論內容comments = StripHTML(item.評論內容comments);
                    }
                }
                return View(datas);
            }
        }
        public IActionResult CommentDelete(int? id)
        {
            var q = _MDAcontext.電影評論movieComments.First(c => c.評論編號commentId == id);
            _MDAcontext.電影評論movieComments.Remove(q);
            _MDAcontext.SaveChanges();

            return RedirectToAction("CommentList");
        }
        public IActionResult CommentEdit(int? id)
        {
            if (id == null)
                return RedirectToAction("CommentList");
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            CWriteCommentViewModel com = new CWriteCommentViewModel();
            var q = _MDAcontext.電影評論movieComments.Where(c => c.評論編號commentId == id).Select(c => new CWriteCommentViewModel
            {
                CommentId = (int)id,
                MemberId = c.會員編號memberId,
                comTitle = c.評論標題commentTitle,
                movieName = _MDAcontext.電影movies.Where(m => m.電影編號movieId == c.電影編號movieId).Select(m => m.中文標題titleCht).FirstOrDefault(),
                nick = mem.暱稱nickName,
                watchDate = String.Format("{0:yyyy-MM-dd}", c.觀影日期viewingTime),
                content = c.評論內容comments,
                rate = (decimal)c.評分rate,
                way = c.觀影方式source,
                open = (int)c.公開等級編號publicId,
                floor = (bool)c.是否開放討論串oxFloor == true ? 1 : 0,
            }).FirstOrDefault();

            return View(q);
        }
        [HttpPost]
        public IActionResult CommentEdit(CWriteCommentViewModel vm)
        {
            var q = _MDAcontext.電影評論movieComments.First(c => c.評論編號commentId == vm.CommentId);
            q.評論標題commentTitle = vm.comTitle;
            q.評論內容comments = vm.content;
            q.評分rate = vm.rate;
            q.觀影日期viewingTime = DateTime.Parse(vm.watchDate);
            q.觀影方式source = vm.way;
            q.公開等級編號publicId = vm.open;
            if (vm.floor == 1)
                q.是否開放討論串oxFloor = true;
            else
                q.是否開放討論串oxFloor = false;

            _MDAcontext.SaveChanges();

            return RedirectToAction("電影評論", "Comment", new { id = vm.CommentId });
        }
        public IActionResult WriteComment(int? id)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return RedirectToAction("Login");
            else
            {
                if (id != null)
                {
                    CWriteCommentViewModel vm = new CWriteCommentViewModel();
                    vm.movieName = _MDAcontext.電影movies.Where(m => m.電影編號movieId == id).Select(m => m.中文標題titleCht).FirstOrDefault();
                    return View(vm);
                }
                else
                {
                    var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                    會員member mem = JsonSerializer.Deserialize<會員member>(a);
                    if (mem.正式會員formal == false)
                    {
                        return RedirectToAction("NotFormal");
                    }
                    return View();
                }
            }
        }
        [HttpPost]
        public IActionResult WriteComment(CWriteCommentViewModel vm)
        {
            var a = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member mem = JsonSerializer.Deserialize<會員member>(a);
            bool floor = true;
            if (vm.floor != 1)
                floor = false;
            電影評論movieComment com = new 電影評論movieComment()
            {
                公開等級編號publicId = vm.open,
                屏蔽invisible = 0,
                是否開放討論串oxFloor = floor,
                會員編號memberId = mem.會員編號memberId,
                //期待度anticipation=vm.anti,
                觀影方式source = vm.way,
                評分rate = vm.rate,
                觀影日期viewingTime = DateTime.Parse(vm.watchDate),
                發佈時間commentTime = DateTime.Now,
                電影編號movieId = _MDAcontext.電影movies.Where(m => m.中文標題titleCht == vm.movieName || m.英文標題titleEng == vm.movieName).Select(m => m.電影編號movieId).FirstOrDefault(),
                評論內容comments = vm.content,
                評論標題commentTitle = vm.comTitle,
            };
            _MDAcontext.電影評論movieComments.Add(com);
            _MDAcontext.SaveChanges();
            var id = _MDAcontext.電影評論movieComments.Where(c => c.會員編號memberId == mem.會員編號memberId && c.評論標題commentTitle == vm.comTitle)
                .Select(c => c.評論編號commentId).FirstOrDefault();
            return RedirectToAction("電影評論", "Comment", new { id = id });
        }



        #endregion
        public string StripHTML(string input)
        {
            if (input == null)
                return "";
            return Regex.Replace(input, "<[a-zA-Z/].*?>", String.Empty);
        }

        #region refund
        public IActionResult GetfullOrderInfo(int orderId) //訂單詳情
        {
            HttpContext.Session.SetInt32(CDictionary.SK_訂單詳情當前訂單, orderId);
            return ViewComponent("OrderInfo", orderId);
        }

        public IActionResult CheckCode(string code)
        {
            int? id = HttpContext.Session.GetInt32(CDictionary.SK_訂單詳情當前訂單);
            int codevalue = (int)id;
            if (code != (codevalue.ToString()))
                return Json('F');
            var order = this._MDAcontext.訂單總表orders.Where(o => o.訂單編號orderId == (int)id).FirstOrDefault();
            var screen = this._MDAcontext.場次screenings.Where(s => s.場次編號screeningId == order.場次編號screeningId).FirstOrDefault();
            if (screen.放映日期playDate < DateTime.Now.AddDays(1.0) || order.訂單狀態編號orderStatusId == 3)
                return Json('O');
            return Json('T');
        }

        public IActionResult PasswordCheck(string password, string code)
        {
            string piccode = HttpContext.Session.GetString(CDictionary.SK_PICTURECODE);
            string Mjson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            會員member member = JsonSerializer.Deserialize<會員member>(Mjson);
            if (password == member.密碼password && code == piccode)
                return Json('T');
            else
                return Json('F');
        }

        public IActionResult Ajaxrefund()
        {
            int? id = HttpContext.Session.GetInt32(CDictionary.SK_訂單詳情當前訂單);
            int orderID = (int)id;
            try
            {
                fn_退票(orderID);
                fn_退票座位回歸可選(orderID);
                return Json('T');
            }
            catch
            {
                return Json('F');
            }          
        }

        public void fn_退票(int orderID)
        {
            訂單總表order order = this._MDAcontext.訂單總表orders.Where(o => o.訂單編號orderId == orderID).FirstOrDefault();
            order.訂單狀態編號orderStatusId = 3;
            this._MDAcontext.SaveChanges();
        }
        public void fn_退票座位回歸可選(int orderID)
        {
            訂單總表order order = this._MDAcontext.訂單總表orders.Where(o => o.訂單編號orderId == orderID).FirstOrDefault();
            List<string> seatInfo = this._MDAcontext.出售座位明細seatSolds
                      .Where(s => s.訂單編號orderId == orderID)
                      .Select(s => s.座位表編號seatId).ToList();
            出售座位狀態seatStatus seatStatus = this._MDAcontext.出售座位狀態seatStatuses.Where(s => s.場次編號screeningId == order.場次編號screeningId).FirstOrDefault();
            string[] strArr = seatStatus.出售座位資訊seatSoldInfo.Split('@');
            for (int i = 0; i < strArr.Length; i++)
            {
                foreach (string item in seatInfo)
                {
                    if (strArr[i].Trim() == $"{item.Trim()}saled")
                    {
                        strArr[i] = item.Trim();
                    }
                }
            }
            string newdata = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                newdata += strArr[i]+'@';
            }
            seatStatus.出售座位資訊seatSoldInfo = newdata;
            this._MDAcontext.SaveChanges();
        }

        #endregion
    }
}
