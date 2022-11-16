using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CPasswordViewModel
    {
        public int memberId { get; set; }
        public string txt_old_password { get; set; }
        public string txt_new_password { get; set; }
        public string txt_new_password_confirm { get; set; }
    }
}
