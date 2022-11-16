using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 會員問題memQa
    {
        public int 會員問題memQaId { get; set; }
        public string 問題question { get; set; }
        public string 答案answer { get; set; }
    }
}
