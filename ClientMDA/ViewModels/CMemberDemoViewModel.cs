using ClientMDA.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMemberDemoViewModel
    {
        private 會員member _member;
        public 會員member member
        {
            get { return _member; }
            set { _member = value; }
        }
        public CMemberDemoViewModel()
        {
            _member = new 會員member();
        }
        public int 會員編號memberId { get { return _member.會員編號memberId; } set { _member.會員編號memberId = value; } }
        public string 會員電話cellphone { get { return _member.會員電話cellphone; } set { _member.會員電話cellphone = value; } }
        public string 密碼password { get { return _member.密碼password; } set { _member.密碼password = value; } }
        public string 姓氏lName { get { return _member.姓氏lName; } set { _member.姓氏lName = value; } }
        public string 名字fName { get { return _member.名字fName; } set { _member.名字fName = value; } }
        public string 暱稱nickName { get { return _member.暱稱nickName; } set { _member.暱稱nickName = value; } }
        public DateTime? 生日birthDate { get { return _member.生日birthDate; } set { _member.生日birthDate = value; } }
        public int? 性別gender { get { return _member.性別gender; } set { _member.性別gender = value; } }
        public string 電子信箱email { get { return _member.電子信箱email; } set { _member.電子信箱email = value; } }
        public string 地址address { get { return _member.地址address; } set { _member.地址address = value; } }
        public int? 紅利點數bonus { get { return _member.紅利點數bonus; } set { _member.紅利點數bonus = value; } }
        public bool 正式會員formal { get { return _member.正式會員formal; } set { _member.正式會員formal = value; } }
        public int 會員權限permission { get { return _member.會員權限permission; } set { _member.會員權限permission = value; } }
        public string 會員照片image { get { return _member.會員照片image; } set { _member.會員照片image = value; } }
        public DateTime 建立時間createdTime { get { return _member.建立時間createdTime; } set { _member.建立時間createdTime = value; } }

        public IFormFile memberPhoto { get; set; }
        public string birthDate { get; set; }
    }
}
