using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClientMDA.ViewModels
{
    public class CQANDAViewModel
    {
        public List<問題總類questionType> 問題總類questionType { get; set; }
        public List<問題總表question> 問題總表Question{get;set;}
        public List<一般資訊generaInformation> 一般資訊GeneraInformation { get; set; }
        public List<會員問題memQa> 會員問題memQa { get; set; }
        public List<評分問題rateQa> 評分問題rateQa { get; set; }
        public List<評論問題commentQa> 評論問題commentQa { get; set; }
        public List<加入片單addlistQa> 加入片單addlistQa { get; set; }
        public List<訂票問題orderticketQa> 訂票問題orderticketQa { get; set; }
        public List<取票問題taketicketQa> 取票問題taketicketQa { get; set; }
        public List<退票問題refundQa> 退票問題refundQa { get; set; }
        public List<優惠券couponQa> 優惠券couponQa { get; set; }
        public List<購物問題shopQa> 購物問題shopQa { get; set; }

    }
}
