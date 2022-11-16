using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影主演cast
    {
        public int 主演編號maId { get; set; }
        public int 電影編號movieId { get; set; }
        public int 演員編號actorId { get; set; }
        public string 角色名字characterName { get; set; }
        public string 角色說明characterIllustrate { get; set; }

        public virtual 演員總表actor 演員編號actor { get; set; }
        public virtual 電影movie 電影編號movie { get; set; }
    }
}
