using System;
using System.Collections.Generic;

#nullable disable

namespace ClientMDA.Models
{
    public partial class 會員member
    {
        public 會員member()
        {
            優惠明細couponLists = new HashSet<優惠明細couponList>();
            我的片單myMovieLists = new HashSet<我的片單myMovieList>();
            我的追蹤清單myFollowLists = new HashSet<我的追蹤清單myFollowList>();
            訂單總表orders = new HashSet<訂單總表order>();
            電影評論movieComments = new HashSet<電影評論movieComment>();
        }

        public int 會員編號memberId { get; set; }
        public string 會員電話cellphone { get; set; }
        public string 密碼password { get; set; }
        public string 姓氏lName { get; set; }
        public string 名字fName { get; set; }
        public string 暱稱nickName { get; set; }
        public DateTime? 生日birthDate { get; set; }
        public int? 性別gender { get; set; }
        public string 電子信箱email { get; set; }
        public string 地址address { get; set; }
        public int? 紅利點數bonus { get; set; }
        public bool 正式會員formal { get; set; }
        public int 會員權限permission { get; set; }
        public string 會員照片image { get; set; }
        public DateTime 建立時間createdTime { get; set; }

        public virtual 性別gender 性別genderNavigation { get; set; }
        public virtual 會員權限permission 會員權限permissionNavigation { get; set; }
        public virtual ICollection<優惠明細couponList> 優惠明細couponLists { get; set; }
        public virtual ICollection<我的片單myMovieList> 我的片單myMovieLists { get; set; }
        public virtual ICollection<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }
        public virtual ICollection<訂單總表order> 訂單總表orders { get; set; }
        public virtual ICollection<電影評論movieComment> 電影評論movieComments { get; set; }
    }
}
