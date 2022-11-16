using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class COrderListViewModel
    {
        public int memberId { get; set; }
        public int orderId { get; set; }
        public string status { get; set; }
        public DateTime orderDate { get; set; }
        public List<訂單明細orderDetail> orderDetal { get; set; }
        public decimal orderPrice { get; set; }
        public List<int> tickets { get; set; }
        public List<decimal> ticketPrice { get; set; }
        public decimal total { 
            get {
                return Decimal.Round(test(tickets, ticketPrice));
            } 
        }
        decimal test(List<int> t, List<decimal> tp)
        {
            decimal p = 0;
            for(int i = 0; i < t.Count; i++)
            {
                p += t[i] * tp[i];
            }
            return p; 
        }
}
}
