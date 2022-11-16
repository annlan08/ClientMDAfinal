using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 場次screening
    {
        public 場次screening()
        {
            出售座位明細seatSolds = new HashSet<出售座位明細seatSold>();
            出售座位狀態seatStatuses = new HashSet<出售座位狀態seatStatus>();
            訂單總表orders = new HashSet<訂單總表order>();
        }

        public int 場次編號screeningId { get; set; }
        public int 影廳編號cinemaId { get; set; }
        public int 電影代碼movieCode { get; set; }
        public DateTime 放映日期playDate { get; set; }
        public string 放映開始時間playTime { get; set; }

        public virtual 影廳cinema 影廳編號cinema { get; set; }
        public virtual 電影代碼movieCode 電影代碼movieCodeNavigation { get; set; }
        public virtual ICollection<出售座位明細seatSold> 出售座位明細seatSolds { get; set; }
        public virtual ICollection<出售座位狀態seatStatus> 出售座位狀態seatStatuses { get; set; }
        public virtual ICollection<訂單總表order> 訂單總表orders { get; set; }
    }
}
