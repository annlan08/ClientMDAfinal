using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class s1ViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View();

        }

    }
}
