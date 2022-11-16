using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMovieListViewModel
    {
        public int memberId { get; set; }
        public int listId { get; set; }
        public string listName { get; set; }
        public List<CMovieListSubViewModel> myLists { get; set; }



    }
}
