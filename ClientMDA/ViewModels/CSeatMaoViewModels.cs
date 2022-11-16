using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CSeatMaoViewModels
    {
        private 出售座位狀態seatStatus _seatStatus;

        public CSeatMaoViewModels(出售座位狀態seatStatus seatStatus)
        {
            _seatStatus = seatStatus;
        }

        public int 出售座位編號seatId
        {
            get { return _seatStatus.出售座位編號seatId; }
            set { _seatStatus.出售座位編號seatId = value; }
        }
        public int 場次編號screeningId
        {
            get { return _seatStatus.場次編號screeningId; }
            set { _seatStatus.場次編號screeningId = value; }
        }
        public string 出售座位資訊seatSoldInfo
        {
            get { return _seatStatus.出售座位資訊seatSoldInfo; }
            set { _seatStatus.出售座位資訊seatSoldInfo = value; }
        }

        public int seatCount選擇座位數量 { get; set; }
        public string MovieName電影名稱 { get; set; }
        public int MovieID電影編號 { get; set; }
        public int MovieCode電影代碼 { get; set; }
        public string TheaterName影城名稱 { get; set; }
        public int TheaterID影城編號 { get; set; }
        public string Date日期 { get; set; }
        public string fileVersion電影版本 { get; set; }
        private string _StartTime開始時間;
        public string StartTime開始時間 
        {
            get { return _StartTime開始時間; }
            set 
            {
                string stringBuffer = value.ToString();
                string[] Arr = stringBuffer.Split(':');
                _StartTime開始時間 = Arr[0].Trim() + "點" + Arr[1].Trim() + "分";
            } 
        }

    }
}
