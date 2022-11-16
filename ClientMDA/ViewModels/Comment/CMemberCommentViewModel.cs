using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CMemberCommentViewModel
    {
        private 會員member _member;
        private 電影評論movieComment _movieComment;
        private 我的追蹤清單myFollowList _myFollowList;

        public 會員member member
        {
            get { return _member; }
            set { _member = value; }
        }
        public 電影評論movieComment movieComment
        {
            get { return _movieComment; }
            set { _movieComment = value; }
        }
        public 我的追蹤清單myFollowList myFollowList
        {
            get { return _myFollowList; }
            set { _myFollowList = value; }
        }

        public CMemberCommentViewModel()  //每做一個就建一個
        {
            _member = new 會員member();
            _movieComment = new 電影評論movieComment();
            _myFollowList = new 我的追蹤清單myFollowList();
        }

        //--------------會員--------------

        public int 會員編號memberId { get; set; }
        public string 暱稱nickName { get; set; }
        public string 會員照片image { get; set; }
        public DateTime 建立時間createdTime { get; set; }

        //--------------評論--------------
        public int 評論編號commentId { get; set; }
        public string 評論標題commentTitle { get; set; }
        //public int? 公開等級編號publicId { get; set; }
        //public bool? 是否開放討論串oxFloor { get; set; }
        //public int 屏蔽invisible { get; set; }
        //public virtual ICollection<電影評論movieComment> 電影評論movieComments { get; set; }


        //--------------我的追蹤清單--------------
        //public int 評論編號commentId { get; set; }
        //public int 會員編號memberId { get; set; }
        public int 電影編號movieId { get; set; }
        public decimal? 評分rate { get; set; }
        public decimal? 期待度anticipation { get; set; }
        //public string 評論標題commentTitle { get; set; }
        public string 評論內容comments { get; set; }
        public DateTime 發佈時間commentTime { get; set; }
        public DateTime? 觀影日期viewingTime { get; set; }
        public string 觀影方式source { get; set; }
        public int? 公開等級編號publicId { get; set; }
        public bool? 是否開放討論串oxFloor { get; set; }
        public int 屏蔽invisible { get; set; }
        //public virtual ICollection<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }


        public int commentCount { get; set; } //會員評論數
        public int memberfollow { get; set; } //會員被追蹤人數

        public List<string> commentList { get; set; }
        public List<string> memFollowList { get; set; }
        public List<int> memFollowIdList { get; set; }
        
    }
}
