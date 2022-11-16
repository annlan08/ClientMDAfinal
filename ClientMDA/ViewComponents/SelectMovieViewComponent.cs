using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class SelectMovieViewComponent:ViewComponent
    {
        private MDAContext _dbContext;

        public SelectMovieViewComponent(MDAContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(DateTime date)
        {
            int? theaterID = HttpContext.Session.GetInt32(CDictionary.SK_選擇的電影院ID);
            List<CAjaxMovieCodeViewModel> list = new List<CAjaxMovieCodeViewModel>();
            if (theaterID != null)
            {
                list = await (this._dbContext.場次screenings
                           .Where(s => s.影廳編號cinema.電影院編號theaterId == theaterID && s.放映日期playDate.Date == date.Date)
                           .Select(s => new CAjaxMovieCodeViewModel
                           {
                               MovieCode = s.電影代碼movieCode,
                               MovieName = s.電影代碼movieCodeNavigation.電影編號movie.中文標題titleCht,
                               MoviePicture = s.電影代碼movieCodeNavigation.電影編號movie.電影圖片movieIimagesLists.FirstOrDefault().圖片編號image.圖片image,
                               MovieVersion = s.電影代碼movieCodeNavigation.語言編號language.語言名稱languageName,
                               MovieOnlineUrl=s.電影代碼movieCodeNavigation.電影編號movie.電影圖片movieIimagesLists.FirstOrDefault().圖片編號image.圖片雲端imageImdb,
                           }).Distinct().ToListAsync());
            }
            return View(list);
        }
    }
}
