using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CReportViewModel
    {
        public int 評論編號commentId { get; set; }
        public int 會員編號memberId { get; set; }
        public int 對象targetId { get; set; }
        public int 追讚倒編號actionTypeId { get; set; }
        public int 連接編號connectId { get; set; }
        public string 檢舉理由reportReason { get; set; }
    }
}
