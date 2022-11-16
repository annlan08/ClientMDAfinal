using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMovieListSubViewModel
    {
        public int memberId { get; set; }
        public int myMovieListId { get; set; }
        public int movieId { get; set; }
        public string movieTitle { get; set; }
        public string moviePic { get; set; }
        public int listId { get; set; }
    }
}
