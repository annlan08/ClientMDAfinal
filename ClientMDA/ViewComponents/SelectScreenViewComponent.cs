using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class SelectScreenViewComponent:ViewComponent
    {
        private MDAContext _dbContext;

        public SelectScreenViewComponent(MDAContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.場次screenings.ToList();
            _dbContext.影廳cinemas.ToList();
            _dbContext.電影代碼movieCodes.ToList();
        }

        public IViewComponentResult Invoke(DateTime time)
        {
            int? theaterid = HttpContext.Session.GetInt32(CDictionary.SK_選擇的電影院ID);
            List<CAjaxCinemaTypeViewModel> list = new List<CAjaxCinemaTypeViewModel>();
            int? Code = HttpContext.Session.GetInt32(CDictionary.SK_選擇的電影Code);
            if (Code != null)
            {
                list = this._dbContext.場次screenings.AsEnumerable()
                        .Where(s => s.電影代碼movieCode == (int)Code && s.放映日期playDate.Date == time.Date && s.影廳編號cinema.電影院編號theaterId==theaterid)
                        .GroupBy(s => s.影廳編號cinema.廳種名稱cinemaClsName)
                        .Select(s => new CAjaxCinemaTypeViewModel
                        {
                            Info_list = s.Select(info => new CAjaxScreenViewModel
                            { 
                                id = info.場次編號screeningId,
                                Start_time = info.放映開始時間playTime,
                                seatCount = fn_計算座位數(info.出售座位狀態seatStatuses.FirstOrDefault().出售座位資訊seatSoldInfo),
                            }).ToList(),
                            TypeName = s.Key.ToString()
                        }).ToList();
            }
            return View(list);
        }

        [NonAction]
        public int fn_計算座位數(string seatInfo)
        {
            string[] seatArr = seatInfo.Split('@');
            int count = 0;
            foreach (string item in seatArr)
            {
                if (!(item.Contains("NA")) && !(item.Contains("saled")) && !(string.IsNullOrWhiteSpace(item)) && !item.Contains("||")) 
                    count++;
            }
            return count;
        }
    }
}
