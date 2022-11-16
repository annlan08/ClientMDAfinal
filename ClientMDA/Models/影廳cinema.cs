using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 影廳cinema
    {
        public 影廳cinema()
        {
            場次screenings = new HashSet<場次screening>();
        }

        public int 影廳編號cinemaId { get; set; }
        public string 影廳名稱cinemaName { get; set; }
        public int 電影院編號theaterId { get; set; }
        public string 廳種名稱cinemaClsName { get; set; }
        public string 座位資訊seatInfo { get; set; }
        public string 影廳照片image { get; set; }

        public virtual 電影院theater 電影院編號theater { get; set; }
        public virtual ICollection<場次screening> 場次screenings { get; set; }
    }
}
