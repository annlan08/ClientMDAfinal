using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CWriteCommentViewModel
    {
        public int CommentId { get; set; }
        public int MemberId { get; set; }
        public string nick { get; set; }
        public string comTitle { get; set; }
        public int Comment { get; set; }
        public string movieName { get; set; }
        public string watchDate { get; set; }
        public string commentDate { get; set; }
        public string way { get; set; }
        public decimal rate { get; set; }
        public decimal anti { get; set; }
        public int open { get; set; }
        public int floor { get; set; }
        public string content { get; set; }

    }
}
