using ClientMDA.Models;
using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientMDA.ViewConponents
{
    public class 關注評論ViewComponent : ViewComponent //須繼承ViewComponent
    {
        private readonly MDAContext _MDAcontext;

        public 關注評論ViewComponent(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
            _MDAcontext.評論圖片明細commentImagesLists.ToList();
            _MDAcontext.評論圖片總表commentImages.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync(List<CCommentViewModel> datas) //元
        {
            var mPoster = _MDAcontext.評論圖片明細commentImagesLists.Select(i => i);
            datas = _MDAcontext.電影評論movieComments.Where(c => c.公開等級編號publicId != 2 || c.屏蔽invisible == 0) //0公開 0正常                    
                                                     .OrderBy(c => c.會員編號memberId).Select
                    (c => new CCommentViewModel
                    {
                        comment = c,
                        評論內容comments = StripHTML(c.評論內容comments),
                        暱稱nickName = c.會員編號member.暱稱nickName,
                        會員照片image = c.會員編號member.會員照片image,
                        cImgFrList = _MDAcontext.評論圖片明細commentImagesLists.Where(i => i.評論編號commentId == c.評論編號commentId)
                        .Select(c => c.評論圖庫編號commentImage.圖片image).ToList(),
                        mPoster = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == c.電影編號movieId).Select
                        (i => new CMovieImagesListViewModel
                        {
                            movieIMDB = i.圖片編號image.圖片雲端imageImdb,
                            movieImage = i.圖片編號image.圖片image,
                        }
                        ).ToList(),
                    }).Take(6).ToList();
            return View(datas);
        }

        //用這個 async Task<IViewComponentResult> InvokeAsync
        //public async Task<IViewComponentResult> InvokeAsync(int? id) //參考暐婷
        //{
        //    var q = _MDAcontext.我的追蹤清單myFollowLists.Where(f => f.會員編號memberId == id && f.追讚倒編號actionTypeId == 0 && f.對象targetId==1).Select //會員
        //        (f => new CCommentViewModel
        //        {
        //            會員編號memberId = f.會員編號memberId,
        //            暱稱nickName = f.對象target.對象名稱targetName,
        //            對象targetId = f.對象targetId,
        //            連接編號connectId = f.連接編號connectId,
        //            會員照片image = _MDAcontext.會員members.Where(m => m.會員編號memberId == f.連接編號connectId).Select(m => m.會員照片image).FirstOrDefault(),//照片
        //            followMemName = _MDAcontext.會員members.Where(m => m.會員編號memberId == f.連接編號connectId).Select(m => m.暱稱nickName).FirstOrDefault(),//暱稱
        //            comments = _MDAcontext.電影評論movieComments.Where(m => m.會員編號memberId == f.連接編號connectId).OrderByDescending(c => c.發佈時間commentTime).Select
        //            (c => new CCommentViewModel
        //            {                        
        //                發佈時間commentTime = c.發佈時間commentTime,
        //                評論編號commentId = c.評論編號commentId,
        //                評論標題commentTitle = c.評論標題commentTitle,
        //                評論內容comments = c.評論內容comments,
        //                mPosterImg = _MDAcontext.電影圖片movieIimagesLists.Where(i => i.電影編號movieId == c.電影編號movieId).Select
        //                (i => i.圖片編號image.圖片image).FirstOrDefault(),
        //            })
        //            .Take(3).ToList(),
        //        }).ToList();

        //    return View(q);
        //}

        public static string StripHTML(string input) //HTML格式顯示消除
        {
            if (input == null)
                return "";
            return Regex.Replace(input, "<[a-zA-Z/].*?>", String.Empty);
        }
    }
}
