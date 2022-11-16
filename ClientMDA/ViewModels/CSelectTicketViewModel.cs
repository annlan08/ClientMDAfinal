using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMDA.Models;

namespace ClientMDA.ViewModels
{
    public class CSelectTicketViewModel
    {
        private CGetInfoForTicketActionViewModel _cGetInfoForTicketAction;

        public CSelectTicketViewModel(CGetInfoForTicketActionViewModel cGetInfoForTicketAction)
        {
            _cGetInfoForTicketAction = cGetInfoForTicketAction;
        }

        public string seatInfo
        {
            get { return _cGetInfoForTicketAction.seatInfo; }
            set { _cGetInfoForTicketAction.seatInfo = value; }
        }
        public int screenID
        {
            get { return _cGetInfoForTicketAction.screenID; }
            set { _cGetInfoForTicketAction.screenID = value; }
        }

        public int theaterID
        {
            get { return _cGetInfoForTicketAction.theaterID; }
            set { _cGetInfoForTicketAction.theaterID = value; }
        }

        public string theaterName
        {
            get { return _cGetInfoForTicketAction.theaterName; }
            set { _cGetInfoForTicketAction.theaterName = value; }
        }

        public string Date
        {
            get { return _cGetInfoForTicketAction.Date; }
            set { _cGetInfoForTicketAction.Date = value; }
        }

        public string Time
        {
            get { return _cGetInfoForTicketAction.Time; }
            set { _cGetInfoForTicketAction.Time = value; }
        }

        public int ticketCount
        {
            get { return _cGetInfoForTicketAction.ticketCount; }
            set { _cGetInfoForTicketAction.ticketCount = value; }
        }

        public int movieID 
        {
            get { return _cGetInfoForTicketAction.MovieID; }
            set { _cGetInfoForTicketAction.MovieID = value; }
        }

        public int movieCode
        {
            get { return _cGetInfoForTicketAction.MovieCode; }
            set { _cGetInfoForTicketAction.MovieCode = value; }
        }
        public List<CTicketInfoViewModelcs> ALLticketInfo { get; set; }
    }
}
