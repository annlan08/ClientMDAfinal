using ClientMDA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMDA.ViewModels
{
    public class CFloorViewModel
    {
        private 電影評論movieComment _comment;
        private 回覆樓數floor _floor;
        private 會員member _member;
        private 我的追蹤清單myFollowList _myFollowList;

        public 電影評論movieComment comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public 回覆樓數floor floor
        {
            get { return _floor; }
            set { _floor = value; }
        }
        public 會員member member
        {
            get { return _member; }
            set { _member = value; }
        }
        public 我的追蹤清單myFollowList myFollowList
        {
            get { return _myFollowList; }
            set { _myFollowList = value; }
        }

        public CFloorViewModel()  //每做一個就建一個
        {
            _comment = new 電影評論movieComment();
            _floor = new 回覆樓數floor();
            _member = new 會員member();
            _myFollowList = new 我的追蹤清單myFollowList();
        }

        //---------------------------回覆---------------------------//


        public int 樓數編號floorId
        {
            get { return _floor.樓數編號floorId; }
            set { _floor.樓數編號floorId = value; }
        }
        public int 評論編號commentId
        {
            get { return _floor.評論編號commentId; }
            set { _floor.評論編號commentId = value; }
        }
        public int 會員編號memberId
        {
            get { return _floor.會員編號memberId; }
            set { _floor.會員編號memberId = value; }
        }

        public string 回覆內容floors
        {
            get { return _floor.回覆內容floors; }
            set { _floor.回覆內容floors = value; }
        }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime 發佈時間floorTime
        {
            get { return _floor.發佈時間floorTime; }
            set { _floor.發佈時間floorTime = value; }
        }
        public int? 被按讚次數thumbsUp
        {
            get { return _floor.被按讚次數thumbsUp; }
            set { _floor.被按讚次數thumbsUp = value; }
        }
        public int? 被倒讚次數thumbsDown
        {
            get { return _floor.被倒讚次數thumbsDown; }
            set { _floor.被倒讚次數thumbsDown = value; }
        }
        public int 屏蔽invisible
        {
            get
            { return _floor.屏蔽invisible; }
            set
            { _floor.屏蔽invisible = value; }
        }


        //---------------------------電影評論---------------------------//

        public int 電影編號movieId { get; set; }
        public decimal? 評分rate { get; set; }
        public decimal? 期待度anticipation { get; set; }
        public string 評論標題commentTitle { get; set; }
        public string 評論內容comments { get; set; }
        public DateTime 發佈時間commentTime { get; set; }
        public DateTime? 觀影日期viewingTime { get; set; }
        public string 觀影方式source { get; set; }
        public int? 公開等級編號publicId { get; set; }
        public bool? 是否開放討論串oxFloor { get; set; }
        //public int 屏蔽invisible { get; set; }

        //---------------------------會員---------------------------//
        public string 暱稱nickName { get; set; }
        public string 會員照片image { get; set; }
        public DateTime 建立時間createdTime { get; set; }

        //public virtual 會員member 會員編號member { get; set; }
     
        //---------------------------我的追蹤清單---------------------------//
        public int 我的追蹤清單編號cfId { get; set; }
        //public int 會員編號memberId { get; set; }
        public int 對象targetId { get; set; }
        public int 追讚倒編號actionTypeId { get; set; }
        public int 連接編號connectId { get; set; }
        public string 檢舉理由reportReason { get; set; }
        public int? 處理狀態status { get; set; }
        //public virtual ICollection<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }  

        public List<string> fNickName { get; set; }
        public List<string> fMemImg { get; set; }

    }
}
