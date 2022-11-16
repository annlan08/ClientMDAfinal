using ClientMDA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewComponents
{
    public class ShowCouponViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<優惠明細couponList> coupons)
        {
            return View(coupons);
        }
    }
}
