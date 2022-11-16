using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CAjaxCinemaTypeViewModel //==>所有場次的資訊
    {
        public string TypeName { get; set; }
        public List<CAjaxScreenViewModel> Info_list { get; set; }
    }
}
