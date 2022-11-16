using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;
using System.Text.RegularExpressions;

namespace ClientMDA.ViewConponents
{
    public class 評論留言listViewComponent : ViewComponent //須繼承ViewComponent
    {
        private readonly MDAContext _MDAcontext;

        public 評論留言listViewComponent(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
            //_MDAcontext.評論圖片明細commentImagesLists.ToList();
            //_MDAcontext.評論圖片總表commentImages.ToList();
        }
        //用這個 async Task<IViewComponentResult> InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync(List<CFloorViewModel> datas, int? id) //id=評論id
        {
            var mem = _MDAcontext.會員members.Select(m => m);
            datas = _MDAcontext.回覆樓數floors.Where(f => f.評論編號commentId == id).Select
                    (f => new CFloorViewModel
                    {
                        floor = f,
                        fMemImg = _MDAcontext.會員members.Where(m => m.會員編號memberId == f.會員編號memberId).Select(f => f.會員照片image).ToList(),
                        fNickName = _MDAcontext.會員members.Where(m => m.會員編號memberId == f.會員編號memberId).Select(f => f.暱稱nickName).ToList(),
                        發佈時間floorTime = f.發佈時間floorTime,
                        回覆內容floors = f.回覆內容floors
                    }).ToList();
            return View(datas);
        }

        public static string StripHTML(string input)
        {
            if (input == null)
                return "";
            return Regex.Replace(input, "<[a-zA-Z/].*?>", String.Empty);
        }
    }
}
