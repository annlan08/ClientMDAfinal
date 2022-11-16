using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;

namespace ClientMDA.ViewConponents
{
    public class 電影劇照牆listViewComponent : ViewComponent //須繼承ViewComponent
    {
        private readonly MDAContext _MDAcontext;

        public 電影劇照牆listViewComponent(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
        }
        //用這個 async Task<IViewComponentResult> InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync(List<CMovieImagesListViewModel> datas, int? id)
        {
            datas = _MDAcontext.電影圖片movieIimagesLists.Where(m => m.電影編號movieId == id).Select
            (m => new CMovieImagesListViewModel
            {
                ImagesList = m,
                圖片image = m.圖片編號image.圖片image,
            }).ToList();
            return View(datas);
        }
    }
}
