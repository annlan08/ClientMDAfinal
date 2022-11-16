using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 電影語言movieLanguage
    {
        public 電影語言movieLanguage()
        {
            電影代碼movieCodes = new HashSet<電影代碼movieCode>();
        }

        public int 語言編號languageId { get; set; }
        public string 語言名稱languageName { get; set; }

        public virtual ICollection<電影代碼movieCode> 電影代碼movieCodes { get; set; }
    }
}
