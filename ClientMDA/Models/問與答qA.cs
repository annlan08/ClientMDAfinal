using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 問與答qA
    {
        public int QAId { get; set; }
        public string 問題question { get; set; }
        public string 答案answer { get; set; }
    }
}
