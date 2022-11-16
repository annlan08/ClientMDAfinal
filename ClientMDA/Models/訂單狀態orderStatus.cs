using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 訂單狀態orderStatus
    {
        public 訂單狀態orderStatus()
        {
            訂單總表orders = new HashSet<訂單總表order>();
        }

        public int 訂單狀態編號orderStatusId { get; set; }
        public string 訂單狀態orderStatusName { get; set; }

        public virtual ICollection<訂單總表order> 訂單總表orders { get; set; }
    }
}
