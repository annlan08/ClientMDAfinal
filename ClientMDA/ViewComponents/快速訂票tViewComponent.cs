using ClientMDA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewConponents
{
    public class 快速訂票ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CTheater datas)
        {
            return View(datas);

        }
    }
}
