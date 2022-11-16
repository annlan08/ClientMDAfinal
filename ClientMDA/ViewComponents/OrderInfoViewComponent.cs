using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class OrderInfoViewComponent : ViewComponent
    {
        private MDAContext _dbContext;

        public OrderInfoViewComponent(MDAContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.電影movies.ToList();
            _dbContext.電影圖片movieIimagesLists.ToList();
            _dbContext.票種ticketTypes.ToList();
            _dbContext.票價資訊ticketPrices.ToList();
            _dbContext.商品資料products.ToList();
            _dbContext.購買商品明細receipts.ToList();
            _dbContext.優惠明細couponLists.ToList();
            _dbContext.優惠總表coupons.ToList();
        }

        public IViewComponentResult Invoke(int orderID)
        {
            COrderInfoViewModel view = new COrderInfoViewModel();
            view = fn_顯示訂單詳情(orderID);
            return View(view);
        }

        public COrderInfoViewModel fn_顯示訂單詳情(int orderID)
        {
            int screenID = this._dbContext.訂單總表orders.Where(o => o.訂單編號orderId == orderID).Select(o => o.場次編號screeningId).FirstOrDefault();
            string seatInfo = fn_顯示座位圖(orderID);
            List<string> TicketInfo = fn_顯示每張票買幾張(orderID);
            List<string> ProductInfo = fn_顯示每個產品買幾個(orderID);
            int fullprice = fn_計算總價(orderID);
            _movieinfo Info = fn_取得電影相關資訊(screenID);

            COrderInfoViewModel info = new COrderInfoViewModel()
            {
                fullprice = fullprice,
                TicketInfo = TicketInfo,
                MovieName = Info.MovieName,
                MoviePicture = Info.MoviePicture,
                MovieVersion = Info.MovieVersion,
                OrderId = orderID,
                StartDate = Info.StartDate,
                StartTime = Info.StartTime,
                TheaterAddress = Info.TheaterAddress,
                TheaterName = Info.TheaterName,
                SelectSeatInfo = seatInfo,
                ProductInfo = ProductInfo,
            };

            return info;
        }

        public class _movieinfo
        {
            public string MovieName { get; set; }
            public string MoviePicture { get; set; }
            public string MovieVersion { get; set; }
            public string StartTime { get; set; }
            public string StartDate { get; set; }
            public string TheaterAddress { get; set; }
            public string TheaterName { get; set; }
        }
        public _movieinfo fn_取得電影相關資訊(int screenID)
        {
            _movieinfo Info = new _movieinfo();
            Info = this._dbContext.場次screenings
                        .Where(s => s.場次編號screeningId == screenID)
                        .Select(s => new _movieinfo
                        {
                            MovieName = s.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht,
                            MoviePicture = s.電影代碼movieCodeNavigation.電影編號movie.電影圖片movieIimagesLists.FirstOrDefault().圖片編號image.圖片雲端imageImdb,
                            MovieVersion = s.影廳編號cinema.廳種名稱cinemaClsName,
                            StartTime = s.放映開始時間playTime,
                            StartDate = s.放映日期playDate.ToString("yyyy/MMMdd" + "日"),
                            TheaterAddress = s.影廳編號cinema.電影院編號theater.地址address,
                            TheaterName = s.影廳編號cinema.電影院編號theater.電影院名稱theaterName,
                        }).FirstOrDefault();

            return Info;
        }
        public int fn_計算總價(int orderID)
        {
            int price = 0;
            var priceList = this._dbContext.訂單明細orderDetails.AsEnumerable()
                       .Where(o => o.訂單編號orderId == orderID)
                       .Select(o => o.張數count * o.票價明細ticket.價格ticketPrice).ToList();
            foreach (decimal item in priceList)
            {
                price += (int)item;
            }
            var productList = this._dbContext.購買商品明細receipts.AsEnumerable()
                                  .Where(o => o.訂單編號orderId == orderID)
                                  .Select(o => o.商品數量qty * o.商品編號product.商品價格productPrice).ToList();
            foreach (decimal item in productList)
            {
                price += (int)item;
            }

            var discount = this._dbContext.使用優惠明細usingCouponLists.AsEnumerable()
                                        .Where(c => c.訂單編號orderId == orderID)
                                        .Select(c => c.優惠明細編號couponList.優惠編號coupon).FirstOrDefault();

            if (discount != null)
                price -= (int)(discount.優惠折扣couponDiscount);

            return price;
        }
        public List<string> fn_顯示每張票買幾張(int orderID)
        {
            return this._dbContext.訂單明細orderDetails.AsEnumerable()
                      .Where(o => o.訂單編號orderId == orderID)
                      .Select(o => $"{o.票價明細ticket.票種編號ticketType.票種名稱ticketTypeName}X{o.張數count}").ToList();

        }

        public List<string> fn_顯示每個產品買幾個(int orderID)
        {
            return this._dbContext.購買商品明細receipts.AsEnumerable()
                      .Where(o => o.訂單編號orderId == orderID)
                      .Select(o => $"{o.商品編號product.商品名稱productName}X{o.商品數量qty}").ToList();

        }

        public string fn_顯示座位圖(int orderID)
        {
            string seatInfo = this._dbContext.訂單總表orders
                                  .Where(o => o.訂單編號orderId == orderID)
                                  .Select(o => o.場次編號screening.影廳編號cinema.座位資訊seatInfo).FirstOrDefault();
            List<string> selectSeat = this._dbContext.出售座位明細seatSolds
                                        .Where(s => s.訂單編號orderId == orderID)
                                        .Select(s => s.座位表編號seatId).ToList();
            string[] seatArr = seatInfo.Split('@');
            for (int i = 0; i < seatArr.Count(); i++)
            {
                foreach (string selectitem in selectSeat)
                {
                    if (seatArr[i].Trim() == selectitem.Trim())
                    {
                        seatArr[i] += "selected";
                    }
                }
            }

            seatInfo = "";

            for (int i = 0; i < seatArr.Count(); i++)
            {
                seatInfo += seatArr[i] + "#";
            }
            return seatInfo;
        }
    }
}
