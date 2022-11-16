using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CAjaxScreenViewModel //==>一個場次的資訊
    {
        public int id{ get; set; }
        public string Start_time { get; set; }
        public int seatCount { get; set; }
    }
}
