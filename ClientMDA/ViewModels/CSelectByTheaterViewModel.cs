using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CSelectByTheaterViewModel
    {
        public int theaterId { get; set; }
        public string theaterName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public List<場次screening> Allscreen { get; set; }
    }
}
