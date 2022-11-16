using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CFollowListViewModel
    {
        public int memberId { get; set; }
        public int targetId { get; set; }
        public string targetName { get; set; }
        public int connectId { get; set; }
        public string followMemName { get; set; }
        public List<CWriteCommentViewModel> comments { get; set; }
        public string followComTitle { get; set; }
        public List<string> replies { get; set; }

    }
}
