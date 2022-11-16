using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 票種ticketType
    {
        public 票種ticketType()
        {
            票價資訊ticketPrices = new HashSet<票價資訊ticketPrice>();
        }

        public int 票種編號ticketTypeId { get; set; }
        public string 票種名稱ticketTypeName { get; set; }

        public virtual ICollection<票價資訊ticketPrice> 票價資訊ticketPrices { get; set; }
    }
}
