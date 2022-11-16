using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMovieActorViewModel
    {
        public int ActorID { get; set; }
        public string ActorChtName { get; set; }
        public string ActorEngName { get; set; }
        public string ActorImg { get; set; }
        public string CharacterName { get; set; }
        public string CharacterTxt { get; set; }
    }
}
