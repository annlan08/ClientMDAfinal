using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 評論問題commentQa
    {
        public int 評論問題commentQaId { get; set; }
        public string 問題question { get; set; }
        public string 答案answer { get; set; }
    }
}
