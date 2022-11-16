using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewConponents
{
    public class 時間ViewComponent : ViewComponent
    {
        private readonly MDAContext _MDA;
        public 時間ViewComponent(MDAContext MDA)
        {
            _MDA = MDA;
            _MDA.電影圖片movieIimagesLists.ToList();
            _MDA.電影圖片總表movieImages.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, List<CTheater> datas) {
            DateTime today = DateTime.Now;
            int mcid = this._MDA.電影代碼movieCodes.Where(p => p.電影編號movieId == id).Select(x => x.電影代碼編號movieCodeId).FirstOrDefault();
            DateTime timer = _MDA.場次screenings.Where(p => p.放映日期playDate.Date == today).Select(x => x.放映日期playDate).FirstOrDefault();
          
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
           場次座位數量 = _MDA.場次screenings.Where(x => x.電影代碼movieCode == mcid && x.放映日期playDate == timer).Select(s => new CSeatViewMode
           {
               座位數量 = s.出售座位狀態seatStatuses.Select(o => o.出售座位資訊seatSoldInfo).FirstOrDefault()

           }).ToList()
       }).ToList(),

   }).ToList();
            return View(datas);

        }
    }
}
