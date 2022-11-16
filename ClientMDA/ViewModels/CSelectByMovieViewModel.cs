using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CSelectByMovieViewModel
    {
        public 電影代碼movieCode movie { get; set; }
        public 電影院theater theater { get; set; }
        public List<string> Delectors { get; set; }
        public List<string> Actors { get; set; }
    }
}
