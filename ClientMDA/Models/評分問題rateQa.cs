using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 評分問題rateQa
    {
        public int 評分問題rateQaId { get; set; }
        public string 問題question { get; set; }
        public string 答案answer { get; set; }
    }
}
