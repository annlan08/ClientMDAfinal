using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 問題總表question
    {
        public int 問題編號questionId { get; set; }
        public string 答案answer { get; set; }
        public int? 問題總類questionTypeId { get; set; }
    }
}
