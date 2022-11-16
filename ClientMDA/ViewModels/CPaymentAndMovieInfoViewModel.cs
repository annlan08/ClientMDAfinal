using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CPaymentAndMovieInfoViewModel
    {
        private CPaymentweb1ViewModel _web1;
        public CPaymentAndMovieInfoViewModel(CPaymentweb1ViewModel web1)
        {
            _web1 = web1;
        }

        public int screenID場次ID 
        { 
            get { return _web1.screenID; } 
            set { _web1.screenID = value; } 
        }
        public int count人數
        {
            get { return _web1.count; }
            set { _web1.count = value; }
        }
        public int theaterID電影院ID
        {
            get { return _web1.theaterID; }
            set { _web1.theaterID = value; }
        }
        public string ticketInfo訂票資訊
        {
            get { return _web1.Ticketstring; }
            set { _web1.Ticketstring = value; }
        }
        public string seatInfo座位資訊
        {
            get { return _web1.seatInfo; }
            set { _web1.seatInfo = value; }
        }
        public string Data日期
        {
            get { return _web1.Data; }
            set { _web1.Data = value; }
        }
        public string Time時間
        {
            get { return _web1.Time; }
            set { _web1.Time = value; }
        }

        public string theaterName電影院名稱 { get; set; }
        public string Movieimage電影照片 { get; set; }
        public string MovieName電影名稱  { get; set; }
        public string MovieVersion電影版本 { get; set; }
        public string MovieInfo電影介紹 { get; set; }
        public List<CTicketItemViewModel> Alltciket { get; set; }
    }
}
