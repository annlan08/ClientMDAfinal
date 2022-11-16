using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CCouponListViewModel
    {
        public int memberId { get; set; }
        public int couponListId { get; set; }
        public string couponName { get; set; }
        public DateTime dueDate { get; set; }       
        public decimal diccount { get; set; }
        public bool used { get; set; }
        //public int? points { get; set; }
    }
}
