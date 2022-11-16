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
    public class 電影評論listViewComponent : ViewComponent //須繼承ViewComponent
    {
        private readonly MDAContext _MDAcontext;

        public 電影評論listViewComponent(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
            _MDAcontext.評論圖片明細commentImagesLists.ToList();
            _MDAcontext.評論圖片總表commentImages.ToList();
        }
        //用這個 async Task<IViewComponentResult> InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync(List<CCommentViewModel> datas, int? id)
        {
            var memPhoto = _MDAcontext.會員members.Select(i => i);
            datas = _MDAcontext.電影評論movieComments.Where(m => m.電影編號movieId == id).OrderByDescending(c => c.發佈時間commentTime).Select
                    (c => new CCommentViewModel
                    {
                        comment = c,
                        評論內容comments = StripHTML(c.評論內容comments),
                        暱稱nickName = c.會員編號member.暱稱nickName,
                        會員照片image = c.會員編號member.會員照片image,
                        cImgFrList = _MDAcontext.評論圖片明細commentImagesLists.Where(i => i.評論編號commentId == c.評論編號commentId)
                            .Select(c => c.評論圖庫編號commentImage.圖片image).ToList()
                    }).Take(10).ToList();

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
