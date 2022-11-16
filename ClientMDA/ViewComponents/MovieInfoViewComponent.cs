using ClientMDA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class MovieInfoViewComponent:ViewComponent
    {
        private MDAContext _dbContext;

        public MovieInfoViewComponent(MDAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke(int id)
        {
            電影movie movie = this._dbContext.電影movies.Where(m => m.電影編號movieId == id).FirstOrDefault();
            return View(movie);
        }
    }
}
