using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 影城mainTheater
    {
        public 影城mainTheater()
        {
            票價資訊ticketPrices = new HashSet<票價資訊ticketPrice>();
            電影院theaters = new HashSet<電影院theater>();
        }

        public int 影城編號mainTheaterId { get; set; }
        public string 影城名稱mainTheaterName { get; set; }

        public virtual ICollection<票價資訊ticketPrice> 票價資訊ticketPrices { get; set; }
        public virtual ICollection<電影院theater> 電影院theaters { get; set; }
    }
}
