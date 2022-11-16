using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;

namespace ClientMDA.ViewConponents
{
    public class 演員listViewComponent : ViewComponent //須繼承ViewComponent
    {
        private readonly MDAContext _MDAcontext;

        public 演員listViewComponent(MDAContext MDAcontext)  //相依性注入
        {
            _MDAcontext = MDAcontext;
        }
        //用這個 async Task<IViewComponentResult> InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync( int? id)
        {
            List<CMovieViewModel> datas=null;
            //var memPhoto = _MDAcontext.會員members.Select(i => i);
            datas = _MDAcontext.電影movies.Where(m => m.電影編號movieId == id).Select
                (m => new CMovieViewModel
                {
                    movie = m,
                    電影編號movieId = m.電影編號movieId,
                    //演員List
                    mActorList = _MDAcontext.電影主演casts.Where(i => i.電影編號movieId == m.電影編號movieId)
                                                          .OrderBy(m => m.演員編號actorId).Select
                    (c => new CMovieActorViewModel
                    {
                        ActorID = c.演員編號actorId,
                        ActorChtName = c.演員編號actor.演員中文名字nameCht,
                        ActorEngName = c.演員編號actor.演員英文名字nameEng,
                        ActorImg = c.演員編號actor.演員照片image,
                        CharacterName = c.角色名字characterName,
                        CharacterTxt = c.角色說明characterIllustrate,
                    }).ToList(),
                }).Skip(0).Take(10).ToList();
            return View(datas);
        }
    }
}
