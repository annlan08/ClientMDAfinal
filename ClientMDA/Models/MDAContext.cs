using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClientMDA.Models
{
    public partial class MDAContext : DbContext
    {
        public MDAContext()
        {
        }

        public MDAContext(DbContextOptions<MDAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<一般資訊generaInformation> 一般資訊generaInformations { get; set; }
        public virtual DbSet<使用優惠明細usingCouponList> 使用優惠明細usingCouponLists { get; set; }
        public virtual DbSet<優惠券couponQa> 優惠券couponQas { get; set; }
        public virtual DbSet<優惠明細couponList> 優惠明細couponLists { get; set; }
        public virtual DbSet<優惠總表coupon> 優惠總表coupons { get; set; }
        public virtual DbSet<公開等級編號publicId> 公開等級編號publicIds { get; set; }
        public virtual DbSet<出售座位明細seatSold> 出售座位明細seatSolds { get; set; }
        public virtual DbSet<出售座位狀態seatStatus> 出售座位狀態seatStatuses { get; set; }
        public virtual DbSet<加入片單addlistQa> 加入片單addlistQas { get; set; }
        public virtual DbSet<取票問題taketicketQa> 取票問題taketicketQas { get; set; }
        public virtual DbSet<商品資料product> 商品資料products { get; set; }
        public virtual DbSet<問題總表question> 問題總表questions { get; set; }
        public virtual DbSet<問題總類questionType> 問題總類questionTypes { get; set; }
        public virtual DbSet<回覆樓數floor> 回覆樓數floors { get; set; }
        public virtual DbSet<國家總表country> 國家總表countrys { get; set; }
        public virtual DbSet<地址address> 地址addresses { get; set; }
        public virtual DbSet<場次screening> 場次screenings { get; set; }
        public virtual DbSet<對象target> 對象targets { get; set; }
        public virtual DbSet<導演總表director> 導演總表directors { get; set; }
        public virtual DbSet<影城mainTheater> 影城mainTheaters { get; set; }
        public virtual DbSet<影廳cinema> 影廳cinemas { get; set; }
        public virtual DbSet<性別gender> 性別genders { get; set; }
        public virtual DbSet<我的片單myMovieList> 我的片單myMovieLists { get; set; }
        public virtual DbSet<我的追蹤清單myFollowList> 我的追蹤清單myFollowLists { get; set; }
        public virtual DbSet<會員member> 會員members { get; set; }
        public virtual DbSet<會員問題memQa> 會員問題memQas { get; set; }
        public virtual DbSet<會員權限permission> 會員權限permissions { get; set; }
        public virtual DbSet<標籤明細hashtagsList> 標籤明細hashtagsLists { get; set; }
        public virtual DbSet<標籤總表hashtag> 標籤總表hashtags { get; set; }
        public virtual DbSet<演員總表actor> 演員總表actors { get; set; }
        public virtual DbSet<片單總表movieList> 片單總表movieLists { get; set; }
        public virtual DbSet<片種總表totalType> 片種總表totalTypes { get; set; }
        public virtual DbSet<票價資訊ticketPrice> 票價資訊ticketPrices { get; set; }
        public virtual DbSet<票種ticketType> 票種ticketTypes { get; set; }
        public virtual DbSet<系列電影movieSeries> 系列電影movieSeries { get; set; }
        public virtual DbSet<訂單明細orderDetail> 訂單明細orderDetails { get; set; }
        public virtual DbSet<訂單狀態orderStatus> 訂單狀態orderStatuses { get; set; }
        public virtual DbSet<訂單總表order> 訂單總表orders { get; set; }
        public virtual DbSet<訂票問題orderticketQa> 訂票問題orderticketQas { get; set; }
        public virtual DbSet<評分問題rateQa> 評分問題rateQas { get; set; }
        public virtual DbSet<評論問題commentQa> 評論問題commentQas { get; set; }
        public virtual DbSet<評論圖片明細commentImagesList> 評論圖片明細commentImagesLists { get; set; }
        public virtual DbSet<評論圖片總表commentImage> 評論圖片總表commentImages { get; set; }
        public virtual DbSet<購物問題shopQa> 購物問題shopQas { get; set; }
        public virtual DbSet<購買商品明細receipt> 購買商品明細receipts { get; set; }
        public virtual DbSet<追讚倒actionType> 追讚倒actionTypes { get; set; }
        public virtual DbSet<退票問題refundQa> 退票問題refundQas { get; set; }
        public virtual DbSet<電影movie> 電影movies { get; set; }
        public virtual DbSet<電影主演cast> 電影主演casts { get; set; }
        public virtual DbSet<電影代碼movieCode> 電影代碼movieCodes { get; set; }
        public virtual DbSet<電影分級movieRating> 電影分級movieRatings { get; set; }
        public virtual DbSet<電影圖片movieIimagesList> 電影圖片movieIimagesLists { get; set; }
        public virtual DbSet<電影圖片總表movieImage> 電影圖片總表movieImages { get; set; }
        public virtual DbSet<電影導演movieDirector> 電影導演movieDirectors { get; set; }
        public virtual DbSet<電影排行movieRank> 電影排行movieRanks { get; set; }
        public virtual DbSet<電影片種movieType> 電影片種movieTypes { get; set; }
        public virtual DbSet<電影產地movieOrigin> 電影產地movieOrigins { get; set; }
        public virtual DbSet<電影評論movieComment> 電影評論movieComments { get; set; }
        public virtual DbSet<電影語言movieLanguage> 電影語言movieLanguages { get; set; }
        public virtual DbSet<電影院theater> 電影院theaters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MDA;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<一般資訊generaInformation>(entity =>
            {
                entity.ToTable("一般資訊GeneraInformation");

                entity.Property(e => e.一般資訊generaInformationId).HasColumnName("一般資訊GeneraInformationID");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<使用優惠明細usingCouponList>(entity =>
            {
                entity.HasKey(e => e.使用優惠明細編號usingCouponListId);

                entity.ToTable("使用優惠明細UsingCouponLists");

                entity.Property(e => e.使用優惠明細編號usingCouponListId).HasColumnName("使用優惠明細編號UsingCouponList_ID");

                entity.Property(e => e.優惠明細編號couponListId).HasColumnName("優惠明細編號CouponList_ID");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.HasOne(d => d.優惠明細編號couponList)
                    .WithMany(p => p.使用優惠明細usingCouponLists)
                    .HasForeignKey(d => d.優惠明細編號couponListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_使用優惠明細UsingCouponLists_優惠明細CouponLists");

                entity.HasOne(d => d.訂單編號order)
                    .WithMany(p => p.使用優惠明細usingCouponLists)
                    .HasForeignKey(d => d.訂單編號orderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_使用優惠明細UsingCouponLists_訂單總表Orders");
            });

            modelBuilder.Entity<優惠券couponQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("優惠券couponQa");

                entity.Property(e => e.優惠券couponQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("優惠券couponQaID");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<優惠明細couponList>(entity =>
            {
                entity.HasKey(e => e.優惠明細編號couponListId)
                    .HasName("PK_優惠明細BonusLists");

                entity.ToTable("優惠明細CouponLists");

                entity.Property(e => e.優惠明細編號couponListId).HasColumnName("優惠明細編號CouponList_ID");

                entity.Property(e => e.優惠編號couponId).HasColumnName("優惠編號Coupon_ID");

                entity.Property(e => e.是否使用優惠oxCouponUsing).HasColumnName("是否使用優惠OX_CouponUsing");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.HasOne(d => d.優惠編號coupon)
                    .WithMany(p => p.優惠明細couponLists)
                    .HasForeignKey(d => d.優惠編號couponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_優惠明細BonusLists_優惠總表Bonus");

                entity.HasOne(d => d.會員編號member)
                    .WithMany(p => p.優惠明細couponLists)
                    .HasForeignKey(d => d.會員編號memberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_優惠明細BonusLists_會員Members");
            });

            modelBuilder.Entity<優惠總表coupon>(entity =>
            {
                entity.HasKey(e => e.優惠編號couponId)
                    .HasName("PK_優惠 Bonus");

                entity.ToTable("優惠總表Coupon");

                entity.Property(e => e.優惠編號couponId).HasColumnName("優惠編號Coupon_ID");

                entity.Property(e => e.優惠代碼couponCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("優惠代碼Coupon_Code");

                entity.Property(e => e.優惠名稱couponName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("優惠名稱Coupon_Name");

                entity.Property(e => e.優惠截止日期couponDueDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("優惠截止日期CouponDueDate");

                entity.Property(e => e.優惠所需紅利bonusCost).HasColumnName("優惠所需紅利BonusCost");

                entity.Property(e => e.優惠折扣couponDiscount)
                    .HasColumnType("money")
                    .HasColumnName("優惠折扣CouponDiscount");
            });

            modelBuilder.Entity<公開等級編號publicId>(entity =>
            {
                entity.HasKey(e => e.公開等級編號publicId1);

                entity.ToTable("公開等級編號Public_ID");

                entity.Property(e => e.公開等級編號publicId1)
                    .ValueGeneratedNever()
                    .HasColumnName("公開等級編號Public_ID");

                entity.Property(e => e.公開等級public)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("公開等級Public");
            });

            modelBuilder.Entity<出售座位明細seatSold>(entity =>
            {
                entity.HasKey(e => e.出售明細編號soldId);

                entity.ToTable("出售座位明細SeatSold");

                entity.Property(e => e.出售明細編號soldId).HasColumnName("出售明細編號Sold_ID");

                entity.Property(e => e.場次編號screeningId).HasColumnName("場次編號Screening_ID");

                entity.Property(e => e.座位表編號seatId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("座位表編號Seat_ID");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.HasOne(d => d.場次編號screening)
                    .WithMany(p => p.出售座位明細seatSolds)
                    .HasForeignKey(d => d.場次編號screeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_出售座位明細SeatSold_場次Screening");

                entity.HasOne(d => d.訂單編號order)
                    .WithMany(p => p.出售座位明細seatSolds)
                    .HasForeignKey(d => d.訂單編號orderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_出售座位明細SeatSold_訂單總表Orders");
            });

            modelBuilder.Entity<出售座位狀態seatStatus>(entity =>
            {
                entity.HasKey(e => e.出售座位編號seatId)
                    .HasName("PK_座位表 Seat");

                entity.ToTable("出售座位狀態SeatStatus");

                entity.Property(e => e.出售座位編號seatId).HasColumnName("出售座位編號Seat_ID");

                entity.Property(e => e.出售座位資訊seatSoldInfo)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("出售座位資訊SeatSoldInfo");

                entity.Property(e => e.場次編號screeningId).HasColumnName("場次編號Screening_ID");

                entity.HasOne(d => d.場次編號screening)
                    .WithMany(p => p.出售座位狀態seatStatuses)
                    .HasForeignKey(d => d.場次編號screeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_出售座位狀態SeatStatus_場次Screening");
            });

            modelBuilder.Entity<加入片單addlistQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("加入片單addlistQa");

                entity.Property(e => e.加入片單addlistQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("加入片單addlistQaID");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<取票問題taketicketQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("取票問題taketicketQa");

                entity.Property(e => e.取票問題taketicketQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("取票問題taketicketQaID");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<商品資料product>(entity =>
            {
                entity.HasKey(e => e.商品編號productId)
                    .HasName("PK_商品資料 Products");

                entity.ToTable("商品資料Products");

                entity.Property(e => e.商品編號productId).HasColumnName("商品編號Product_ID");

                entity.Property(e => e.商品介紹introduce)
                    .HasMaxLength(200)
                    .HasColumnName("商品介紹Introduce");

                entity.Property(e => e.商品價格productPrice)
                    .HasColumnType("money")
                    .HasColumnName("商品價格Product_Price");

                entity.Property(e => e.商品名稱productName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("商品名稱Product_Name");

                entity.Property(e => e.商品圖片image).HasColumnName("商品圖片Image");

                entity.Property(e => e.商品圖片路徑imagePath)
                    .HasMaxLength(200)
                    .HasColumnName("商品圖片路徑ImagePath");

                entity.Property(e => e.電影院編號theaterId).HasColumnName("電影院編號Theater_ID");

                entity.Property(e => e.類別category)
                    .HasMaxLength(50)
                    .HasColumnName("類別Category");

                entity.HasOne(d => d.電影院編號theater)
                    .WithMany(p => p.商品資料products)
                    .HasForeignKey(d => d.電影院編號theaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_商品資料Products_電影院Theater");
            });

            modelBuilder.Entity<問題總表question>(entity =>
            {
                entity.HasKey(e => e.問題編號questionId);

                entity.ToTable("問題總表Question");

                entity.Property(e => e.問題編號questionId).HasColumnName("問題編號QuestionID");

                entity.Property(e => e.問題總類questionTypeId).HasColumnName("問題總類QuestionTypeID");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<問題總類questionType>(entity =>
            {
                entity.HasKey(e => e.問題總類編號questionTypeId);

                entity.ToTable("問題總類QuestionType");

                entity.Property(e => e.問題總類編號questionTypeId).HasColumnName("問題總類編號QuestionTypeID");

                entity.Property(e => e.問題分類questionClassification)
                    .HasMaxLength(10)
                    .HasColumnName("問題分類QuestionClassification");
            });

            modelBuilder.Entity<回覆樓數floor>(entity =>
            {
                entity.HasKey(e => e.樓數編號floorId)
                    .HasName("PK_回覆樓數 Floor");

                entity.ToTable("回覆樓數Floor");

                entity.Property(e => e.樓數編號floorId).HasColumnName("樓數編號Floor_ID");

                entity.Property(e => e.回覆內容floors)
                    .IsRequired()
                    .HasColumnName("回覆內容Floors");

                entity.Property(e => e.屏蔽invisible).HasColumnName("屏蔽Invisible");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.發佈時間floorTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("發佈時間Floor_Time");

                entity.Property(e => e.被倒讚次數thumbsDown).HasColumnName("被倒讚次數Thumbs_Down");

                entity.Property(e => e.被按讚次數thumbsUp).HasColumnName("被按讚次數Thumbs_Up");

                entity.Property(e => e.評論編號commentId).HasColumnName("評論編號Comment_ID");

                entity.HasOne(d => d.評論編號comment)
                    .WithMany(p => p.回覆樓數floors)
                    .HasForeignKey(d => d.評論編號commentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_回覆樓數 Floor_電影評論 Movie Comment");
            });

            modelBuilder.Entity<國家總表country>(entity =>
            {
                entity.HasKey(e => e.國家編號countryId)
                    .HasName("PK_國家總表 Countrys");

                entity.ToTable("國家總表Countrys");

                entity.Property(e => e.國家編號countryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("國家編號Country_ID");

                entity.Property(e => e.國家名稱countryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("國家名稱Country_Name");

                entity.Property(e => e.國旗countryImage)
                    .HasMaxLength(200)
                    .HasColumnName("國旗Country_Image");
            });

            modelBuilder.Entity<地址address>(entity =>
            {
                entity.ToTable("地址Address");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .HasColumnName("city");

                entity.Property(e => e.Road)
                    .HasMaxLength(200)
                    .HasColumnName("road");

                entity.Property(e => e.SiteId)
                    .HasMaxLength(50)
                    .HasColumnName("site_id");
            });

            modelBuilder.Entity<場次screening>(entity =>
            {
                entity.HasKey(e => e.場次編號screeningId)
                    .HasName("PK_場次 Screening");

                entity.ToTable("場次Screening");

                entity.Property(e => e.場次編號screeningId).HasColumnName("場次編號Screening_ID");

                entity.Property(e => e.影廳編號cinemaId).HasColumnName("影廳編號Cinema_ID");

                entity.Property(e => e.放映日期playDate)
                    .HasColumnType("date")
                    .HasColumnName("放映日期Play_Date");

                entity.Property(e => e.放映開始時間playTime)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("放映開始時間Play_Time");

                entity.Property(e => e.電影代碼movieCode).HasColumnName("電影代碼Movie_Code");

                entity.HasOne(d => d.影廳編號cinema)
                    .WithMany(p => p.場次screenings)
                    .HasForeignKey(d => d.影廳編號cinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_場次 Screening_影廳 Cinema");

                entity.HasOne(d => d.電影代碼movieCodeNavigation)
                    .WithMany(p => p.場次screenings)
                    .HasForeignKey(d => d.電影代碼movieCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_場次 Screening_電影代碼 Movie Code");
            });

            modelBuilder.Entity<對象target>(entity =>
            {
                entity.ToTable("對象Targets");

                entity.Property(e => e.對象targetId).HasColumnName("對象Target_ID");

                entity.Property(e => e.對象名稱targetName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("對象名稱Target_Name");
            });

            modelBuilder.Entity<導演總表director>(entity =>
            {
                entity.HasKey(e => e.導演編號directorId)
                    .HasName("PK_導演總表 Director");

                entity.ToTable("導演總表Director");

                entity.Property(e => e.導演編號directorId).HasColumnName("導演編號Director_ID");

                entity.Property(e => e.導演中文名字nameCht)
                    .HasMaxLength(50)
                    .HasColumnName("導演中文名字Name_Cht");

                entity.Property(e => e.導演照片image)
                    .HasMaxLength(200)
                    .HasColumnName("導演照片Image");

                entity.Property(e => e.導演英文名字nameEng)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("導演英文名字Name_Eng");
            });

            modelBuilder.Entity<影城mainTheater>(entity =>
            {
                entity.HasKey(e => e.影城編號mainTheaterId);

                entity.ToTable("影城MainTheater");

                entity.Property(e => e.影城編號mainTheaterId).HasColumnName("影城編號MainTheater_ID");

                entity.Property(e => e.影城名稱mainTheaterName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("影城名稱MainTheater_Name");
            });

            modelBuilder.Entity<影廳cinema>(entity =>
            {
                entity.HasKey(e => e.影廳編號cinemaId)
                    .HasName("PK_影廳 Auditorium");

                entity.ToTable("影廳Cinema");

                entity.Property(e => e.影廳編號cinemaId).HasColumnName("影廳編號Cinema_ID");

                entity.Property(e => e.座位資訊seatInfo)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("座位資訊SeatInfo");

                entity.Property(e => e.廳種名稱cinemaClsName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("廳種名稱Cinema_Cls_Name");

                entity.Property(e => e.影廳名稱cinemaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("影廳名稱Cinema_Name");

                entity.Property(e => e.影廳照片image)
                    .HasMaxLength(200)
                    .HasColumnName("影廳照片Image");

                entity.Property(e => e.電影院編號theaterId).HasColumnName("電影院編號Theater_ID");

                entity.HasOne(d => d.電影院編號theater)
                    .WithMany(p => p.影廳cinemas)
                    .HasForeignKey(d => d.電影院編號theaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_影廳 Cinema_電影院 Theater");
            });

            modelBuilder.Entity<性別gender>(entity =>
            {
                entity.HasKey(e => e.性別gender1)
                    .HasName("PK__性別Gender__52CE6A2412E6A0EA");

                entity.ToTable("性別Gender");

                entity.Property(e => e.性別gender1)
                    .ValueGeneratedNever()
                    .HasColumnName("性別Gender");

                entity.Property(e => e.性別名稱genderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("性別名稱Gender_Name");
            });

            modelBuilder.Entity<我的片單myMovieList>(entity =>
            {
                entity.ToTable("我的片單MyMovieLists");

                entity.Property(e => e.我的片單myMovieListId).HasColumnName("我的片單MyMovieList_ID");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.片單總表編號movieListId).HasColumnName("片單總表編號MovieList_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.會員編號member)
                    .WithMany(p => p.我的片單myMovieLists)
                    .HasForeignKey(d => d.會員編號memberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的片單MyMovieLists_會員Members");

                entity.HasOne(d => d.片單總表編號movieList)
                    .WithMany(p => p.我的片單myMovieLists)
                    .HasForeignKey(d => d.片單總表編號movieListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的片單MyMovieLists_片單總表MovieLists");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.我的片單myMovieLists)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的片單MyMovieLists_電影Movies");
            });

            modelBuilder.Entity<我的追蹤清單myFollowList>(entity =>
            {
                entity.HasKey(e => e.我的追蹤清單編號cfId)
                    .HasName("PK_評論清單 Rate List");

                entity.ToTable("我的追蹤清單MyFollowLists");

                entity.Property(e => e.我的追蹤清單編號cfId).HasColumnName("我的追蹤清單編號CF_ID");

                entity.Property(e => e.對象targetId).HasColumnName("對象Target_ID");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.檢舉理由reportReason).HasColumnName("檢舉理由ReportReason");

                entity.Property(e => e.處理狀態status).HasColumnName("處理狀態Status");

                entity.Property(e => e.追讚倒編號actionTypeId).HasColumnName("追讚倒編號ActionType_ID");

                entity.Property(e => e.連接編號connectId).HasColumnName("連接編號Connect_ID");

                entity.HasOne(d => d.對象target)
                    .WithMany(p => p.我的追蹤清單myFollowLists)
                    .HasForeignKey(d => d.對象targetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的追蹤清單MyFollowLists_對象Targets");

                entity.HasOne(d => d.會員編號member)
                    .WithMany(p => p.我的追蹤清單myFollowLists)
                    .HasForeignKey(d => d.會員編號memberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的追蹤清單MyFollowLists_會員Members2");

                entity.HasOne(d => d.追讚倒編號actionType)
                    .WithMany(p => p.我的追蹤清單myFollowLists)
                    .HasForeignKey(d => d.追讚倒編號actionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_我的追蹤清單MyFollowLists_追讚倒ActionTypes");
            });

            modelBuilder.Entity<會員member>(entity =>
            {
                entity.HasKey(e => e.會員編號memberId)
                    .HasName("PK_會員 Members");

                entity.ToTable("會員Members");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.名字fName)
                    .HasMaxLength(50)
                    .HasColumnName("名字F_Name");

                entity.Property(e => e.地址address)
                    .HasMaxLength(50)
                    .HasColumnName("地址Address");

                entity.Property(e => e.姓氏lName)
                    .HasMaxLength(50)
                    .HasColumnName("姓氏L_Name");

                entity.Property(e => e.密碼password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("密碼Password");

                entity.Property(e => e.建立時間createdTime)
                    .HasColumnType("date")
                    .HasColumnName("建立時間CreatedTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.性別gender).HasColumnName("性別Gender");

                entity.Property(e => e.暱稱nickName)
                    .HasMaxLength(50)
                    .HasColumnName("暱稱NickName");

                entity.Property(e => e.會員權限permission).HasColumnName("會員權限Permission");

                entity.Property(e => e.會員照片image)
                    .HasMaxLength(200)
                    .HasColumnName("會員照片Image");

                entity.Property(e => e.會員電話cellphone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("會員電話Cellphone")
                    .IsFixedLength(true);

                entity.Property(e => e.正式會員formal).HasColumnName("正式會員Formal");

                entity.Property(e => e.生日birthDate)
                    .HasColumnType("date")
                    .HasColumnName("生日BirthDate");

                entity.Property(e => e.紅利點數bonus).HasColumnName("紅利點數Bonus");

                entity.Property(e => e.電子信箱email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("電子信箱Email");

                entity.HasOne(d => d.性別genderNavigation)
                    .WithMany(p => p.會員members)
                    .HasForeignKey(d => d.性別gender)
                    .HasConstraintName("FK_會員Members_性別Gender");

                entity.HasOne(d => d.會員權限permissionNavigation)
                    .WithMany(p => p.會員members)
                    .HasForeignKey(d => d.會員權限permission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_會員Members_會員權限Permission");
            });

            modelBuilder.Entity<會員問題memQa>(entity =>
            {
                entity.ToTable("會員問題memQa");

                entity.Property(e => e.會員問題memQaId).HasColumnName("會員問題memQaID");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");
            });

            modelBuilder.Entity<會員權限permission>(entity =>
            {
                entity.HasKey(e => e.會員權限permission1)
                    .HasName("PK__會員權限Perm__DF1151B2B13FEE0E");

                entity.ToTable("會員權限Permission");

                entity.Property(e => e.會員權限permission1)
                    .ValueGeneratedNever()
                    .HasColumnName("會員權限Permission");

                entity.Property(e => e.權限名稱permissionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("權限名稱Permission_Name");
            });

            modelBuilder.Entity<標籤明細hashtagsList>(entity =>
            {
                entity.HasKey(e => e.標籤明細編號chId)
                    .HasName("PK_標籤明細");

                entity.ToTable("標籤明細HashtagsList");

                entity.Property(e => e.標籤明細編號chId).HasColumnName("標籤明細編號CH_ID");

                entity.Property(e => e.標籤編號hashtagId).HasColumnName("標籤編號Hashtag_ID");

                entity.Property(e => e.評論編號commentId).HasColumnName("評論編號Comment_ID");

                entity.HasOne(d => d.標籤編號hashtag)
                    .WithMany(p => p.標籤明細hashtagsLists)
                    .HasForeignKey(d => d.標籤編號hashtagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_標籤明細_標籤總表Hashtag");

                entity.HasOne(d => d.評論編號comment)
                    .WithMany(p => p.標籤明細hashtagsLists)
                    .HasForeignKey(d => d.評論編號commentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_標籤明細_電影評論MovieComment");
            });

            modelBuilder.Entity<標籤總表hashtag>(entity =>
            {
                entity.HasKey(e => e.標籤編號hashtagId)
                    .HasName("PK_標籤總表Hashtag");

                entity.ToTable("標籤總表Hashtags");

                entity.Property(e => e.標籤編號hashtagId).HasColumnName("標籤編號Hashtag_ID");

                entity.Property(e => e.標籤hashtag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("標籤Hashtag");
            });

            modelBuilder.Entity<演員總表actor>(entity =>
            {
                entity.HasKey(e => e.演員編號actorsId)
                    .HasName("PK_Actors");

                entity.ToTable("演員總表Actors");

                entity.Property(e => e.演員編號actorsId).HasColumnName("演員編號Actors_ID");

                entity.Property(e => e.演員中文名字nameCht)
                    .HasMaxLength(20)
                    .HasColumnName("演員中文名字Name_Cht");

                entity.Property(e => e.演員照片image)
                    .HasMaxLength(200)
                    .HasColumnName("演員照片Image");

                entity.Property(e => e.演員英文名字nameEng)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("演員英文名字Name_Eng");
            });

            modelBuilder.Entity<片單總表movieList>(entity =>
            {
                entity.HasKey(e => e.片單總表編號movieListId);

                entity.ToTable("片單總表MovieLists");

                entity.Property(e => e.片單總表編號movieListId).HasColumnName("片單總表編號MovieList_ID");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.片單總表名稱listName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("片單總表名稱List_Name");
            });

            modelBuilder.Entity<片種總表totalType>(entity =>
            {
                entity.HasKey(e => e.片種編號totalTypeId);

                entity.ToTable("片種總表TotalTypes");

                entity.Property(e => e.片種編號totalTypeId).HasColumnName("片種編號TotalType_ID");

                entity.Property(e => e.片種名稱totalTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("片種名稱TotalType_Name");
            });

            modelBuilder.Entity<票價資訊ticketPrice>(entity =>
            {
                entity.HasKey(e => e.票價明細ticketId)
                    .HasName("PK_票價資訊 Ticket Price");

                entity.ToTable("票價資訊TicketPrice");

                entity.Property(e => e.票價明細ticketId).HasColumnName("票價明細Ticket_ID");

                entity.Property(e => e.價格ticketPrice)
                    .HasColumnType("money")
                    .HasColumnName("價格TicketPrice");

                entity.Property(e => e.影城編號mainTheaterId).HasColumnName("影城編號MainTheater_ID");

                entity.Property(e => e.票種編號ticketTypeId).HasColumnName("票種編號TicketType_ID");

                entity.HasOne(d => d.影城編號mainTheater)
                    .WithMany(p => p.票價資訊ticketPrices)
                    .HasForeignKey(d => d.影城編號mainTheaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_票價資訊TicketPrice_影城MainTheater");

                entity.HasOne(d => d.票種編號ticketType)
                    .WithMany(p => p.票價資訊ticketPrices)
                    .HasForeignKey(d => d.票種編號ticketTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_票價資訊TicketPrice_票種TicketType");
            });

            modelBuilder.Entity<票種ticketType>(entity =>
            {
                entity.HasKey(e => e.票種編號ticketTypeId);

                entity.ToTable("票種TicketType");

                entity.Property(e => e.票種編號ticketTypeId).HasColumnName("票種編號TicketType_ID");

                entity.Property(e => e.票種名稱ticketTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("票種名稱TicketType_Name");
            });

            modelBuilder.Entity<系列電影movieSeries>(entity =>
            {
                entity.HasKey(e => e.系列編號seriesId)
                    .HasName("PK_系列電影 Series");

                entity.ToTable("系列電影MovieSeries");

                entity.Property(e => e.系列編號seriesId).HasColumnName("系列編號Series_ID");

                entity.Property(e => e.系列名稱seriesName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("系列名稱Series_Name");
            });

            modelBuilder.Entity<訂單明細orderDetail>(entity =>
            {
                entity.HasKey(e => e.訂單明細編號orderDetailId)
                    .HasName("PK_會員付款明細 Member Paid");

                entity.ToTable("訂單明細OrderDetail");

                entity.Property(e => e.訂單明細編號orderDetailId).HasColumnName("訂單明細編號OrderDetail_ID");

                entity.Property(e => e.張數count).HasColumnName("張數Count");

                entity.Property(e => e.票價明細ticketId).HasColumnName("票價明細Ticket_ID");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.HasOne(d => d.票價明細ticket)
                    .WithMany(p => p.訂單明細orderDetails)
                    .HasForeignKey(d => d.票價明細ticketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_訂單明細OrderDetail_票價資訊TicketPrice");

                entity.HasOne(d => d.訂單編號order)
                    .WithMany(p => p.訂單明細orderDetails)
                    .HasForeignKey(d => d.訂單編號orderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_訂單明細OrderDetail_訂單總表Orders");
            });

            modelBuilder.Entity<訂單狀態orderStatus>(entity =>
            {
                entity.HasKey(e => e.訂單狀態編號orderStatusId);

                entity.ToTable("訂單狀態OrderStatus");

                entity.Property(e => e.訂單狀態編號orderStatusId).HasColumnName("訂單狀態編號OrderStatus_ID");

                entity.Property(e => e.訂單狀態orderStatusName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("訂單狀態OrderStatus_Name");
            });

            modelBuilder.Entity<訂單總表order>(entity =>
            {
                entity.HasKey(e => e.訂單編號orderId);

                entity.ToTable("訂單總表Orders");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.Property(e => e.場次編號screeningId).HasColumnName("場次編號Screening_ID");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.訂單時間orderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("訂單時間OrderTime");

                entity.Property(e => e.訂單狀態編號orderStatusId).HasColumnName("訂單狀態編號OrderStatus_ID");

                entity.HasOne(d => d.場次編號screening)
                    .WithMany(p => p.訂單總表orders)
                    .HasForeignKey(d => d.場次編號screeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_訂單總表Orders_場次Screening");

                entity.HasOne(d => d.會員編號member)
                    .WithMany(p => p.訂單總表orders)
                    .HasForeignKey(d => d.會員編號memberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_訂單總表Orders_會員Members");

                entity.HasOne(d => d.訂單狀態編號orderStatus)
                    .WithMany(p => p.訂單總表orders)
                    .HasForeignKey(d => d.訂單狀態編號orderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_訂單總表Orders_訂單狀態OrderStatus");
            });

            modelBuilder.Entity<訂票問題orderticketQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("訂票問題orderticketQa");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");

                entity.Property(e => e.訂票問題orderticketQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("訂票問題orderticketQaID");
            });

            modelBuilder.Entity<評分問題rateQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("評分問題rateQa");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");

                entity.Property(e => e.評分問題rateQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("評分問題rateQaID");
            });

            modelBuilder.Entity<評論問題commentQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("評論問題commentQa");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");

                entity.Property(e => e.評論問題commentQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("評論問題commentQaID");
            });

            modelBuilder.Entity<評論圖片明細commentImagesList>(entity =>
            {
                entity.HasKey(e => e.評論圖片編號ccId)
                    .HasName("PK_評論圖片明細CommandImagesList");

                entity.ToTable("評論圖片明細CommentImagesList");

                entity.Property(e => e.評論圖片編號ccId).HasColumnName("評論圖片編號CC_ID");

                entity.Property(e => e.評論圖庫編號commentImageId).HasColumnName("評論圖庫編號CommentImage_ID");

                entity.Property(e => e.評論編號commentId).HasColumnName("評論編號Comment_ID");

                entity.HasOne(d => d.評論圖庫編號commentImage)
                    .WithMany(p => p.評論圖片明細commentImagesLists)
                    .HasForeignKey(d => d.評論圖庫編號commentImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_評論圖片明細CommandImagesList_評論圖庫CommandImages");

                entity.HasOne(d => d.評論編號comment)
                    .WithMany(p => p.評論圖片明細commentImagesLists)
                    .HasForeignKey(d => d.評論編號commentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_評論圖片明細CommandImagesList_電影評論MovieComment");
            });

            modelBuilder.Entity<評論圖片總表commentImage>(entity =>
            {
                entity.HasKey(e => e.評論圖庫編號commentImageId)
                    .HasName("PK_評論圖庫CommandImages");

                entity.ToTable("評論圖片總表CommentImages");

                entity.Property(e => e.評論圖庫編號commentImageId).HasColumnName("評論圖庫編號CommentImage_ID");

                entity.Property(e => e.圖片image)
                    .HasMaxLength(200)
                    .HasColumnName("圖片Image");

                entity.Property(e => e.屏蔽invisible).HasColumnName("屏蔽Invisible");
            });

            modelBuilder.Entity<購物問題shopQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("購物問題shopQa");

                entity.Property(e => e.問題question).HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");

                entity.Property(e => e.購物問題shopQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("購物問題shopQaID");
            });

            modelBuilder.Entity<購買商品明細receipt>(entity =>
            {
                entity.HasKey(e => e.購買商品明細編號receiptId);

                entity.ToTable("購買商品明細Receipt");

                entity.Property(e => e.購買商品明細編號receiptId).HasColumnName("購買商品明細編號Receipt_ID");

                entity.Property(e => e.商品數量qty).HasColumnName("商品數量Qty");

                entity.Property(e => e.商品編號productId).HasColumnName("商品編號Product_ID");

                entity.Property(e => e.訂單編號orderId).HasColumnName("訂單編號Order_ID");

                entity.HasOne(d => d.商品編號product)
                    .WithMany(p => p.購買商品明細receipts)
                    .HasForeignKey(d => d.商品編號productId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_購買商品明細Receipt_商品資料Products");

                entity.HasOne(d => d.訂單編號order)
                    .WithMany(p => p.購買商品明細receipts)
                    .HasForeignKey(d => d.訂單編號orderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_購買商品明細Receipt_訂單總表Orders");
            });

            modelBuilder.Entity<追讚倒actionType>(entity =>
            {
                entity.HasKey(e => e.追讚倒編號actionTypeId);

                entity.ToTable("追讚倒ActionTypes");

                entity.Property(e => e.追讚倒編號actionTypeId).HasColumnName("追讚倒編號ActionType_ID");

                entity.Property(e => e.追讚倒名稱actionTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("追讚倒名稱ActionType_Name");
            });

            modelBuilder.Entity<退票問題refundQa>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("退票問題refundQa");

                entity.Property(e => e.問題question)
                    .IsRequired()
                    .HasColumnName("問題Question");

                entity.Property(e => e.答案answer).HasColumnName("答案Answer");

                entity.Property(e => e.退票問題refundQaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("退票問題refundQaID");
            });

            modelBuilder.Entity<電影movie>(entity =>
            {
                entity.HasKey(e => e.電影編號movieId)
                    .HasName("PK_Movies");

                entity.ToTable("電影Movies");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.Property(e => e.上映年份releaseYear).HasColumnName("上映年份Release_Year");

                entity.Property(e => e.上映日期releaseDate)
                    .HasColumnType("date")
                    .HasColumnName("上映日期Release_Date");

                entity.Property(e => e.中文標題titleCht)
                    .HasMaxLength(50)
                    .HasColumnName("中文標題Title_Cht");

                entity.Property(e => e.劇情大綱plot).HasColumnName("劇情大綱Plot");

                entity.Property(e => e.期待度anticipation).HasColumnType("numeric(2, 1)");

                entity.Property(e => e.片長runtime).HasColumnName("片長Runtime");

                entity.Property(e => e.票房boxOffice).HasColumnName("票房BoxOffice");

                entity.Property(e => e.系列編號seriesId)
                    .HasColumnName("系列編號Series_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.英文標題titleEng)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("英文標題Title_Eng");

                entity.Property(e => e.評分rate)
                    .HasColumnType("numeric(2, 1)")
                    .HasColumnName("評分Rate");

                entity.Property(e => e.電影分級編號ratingId).HasColumnName("電影分級編號Rating_ID");

                entity.Property(e => e.預告片trailer).HasColumnName("預告片Trailer");

                entity.HasOne(d => d.系列編號series)
                    .WithMany(p => p.電影movies)
                    .HasForeignKey(d => d.系列編號seriesId)
                    .HasConstraintName("FK_電影Movies_系列電影MovieSeries");

                entity.HasOne(d => d.電影分級編號rating)
                    .WithMany(p => p.電影movies)
                    .HasForeignKey(d => d.電影分級編號ratingId)
                    .HasConstraintName("FK_電影Movies_電影分級MovieRating");
            });

            modelBuilder.Entity<電影主演cast>(entity =>
            {
                entity.HasKey(e => e.主演編號maId)
                    .HasName("PK_電影主演 Cast");

                entity.ToTable("電影主演Cast");

                entity.Property(e => e.主演編號maId).HasColumnName("主演編號MA_ID");

                entity.Property(e => e.演員編號actorId).HasColumnName("演員編號Actor_ID");

                entity.Property(e => e.角色名字characterName)
                    .HasMaxLength(50)
                    .HasColumnName("角色名字Character_Name");

                entity.Property(e => e.角色說明characterIllustrate).HasColumnName("角色說明Character_illustrate");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.演員編號actor)
                    .WithMany(p => p.電影主演casts)
                    .HasForeignKey(d => d.演員編號actorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_主演 Cast_演員 Actors");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影主演casts)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影主演Cast_電影Movies");
            });

            modelBuilder.Entity<電影代碼movieCode>(entity =>
            {
                entity.HasKey(e => e.電影代碼編號movieCodeId)
                    .HasName("PK_電影代碼 Movie Code");

                entity.ToTable("電影代碼MovieCode");

                entity.Property(e => e.電影代碼編號movieCodeId).HasColumnName("電影代碼編號MovieCode_ID");

                entity.Property(e => e.語言編號languageId).HasColumnName("語言編號Language_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.語言編號language)
                    .WithMany(p => p.電影代碼movieCodes)
                    .HasForeignKey(d => d.語言編號languageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影代碼 Movie Code_電影語言 Movie Language");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影代碼movieCodes)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影代碼MovieCode_電影Movies");
            });

            modelBuilder.Entity<電影分級movieRating>(entity =>
            {
                entity.HasKey(e => e.分級編號ratingId);

                entity.ToTable("電影分級MovieRating");

                entity.Property(e => e.分級編號ratingId).HasColumnName("分級編號Rating_ID");

                entity.Property(e => e.分級圖片ratingImage).HasColumnName("分級圖片Rating_Image");

                entity.Property(e => e.分級級數ratingLevel)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("分級級數Rating_Level");

                entity.Property(e => e.分級說明ratingIllustrate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("分級說明Rating_illustrate");
            });

            modelBuilder.Entity<電影圖片movieIimagesList>(entity =>
            {
                entity.HasKey(e => e.電影圖片編號miId)
                    .HasName("PK_電影圖片MovieIImages");

                entity.ToTable("電影圖片MovieIImagesList");

                entity.Property(e => e.電影圖片編號miId).HasColumnName("電影圖片編號MI_ID");

                entity.Property(e => e.圖片編號imageId).HasColumnName("圖片編號Image_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.圖片編號image)
                    .WithMany(p => p.電影圖片movieIimagesLists)
                    .HasForeignKey(d => d.圖片編號imageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影圖片MovieIImages_圖片總表Images");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影圖片movieIimagesLists)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影圖片MovieIImagesList_電影Movies");
            });

            modelBuilder.Entity<電影圖片總表movieImage>(entity =>
            {
                entity.HasKey(e => e.圖片編號imageId)
                    .HasName("PK_圖片總表Images_1");

                entity.ToTable("電影圖片總表MovieImages");

                entity.Property(e => e.圖片編號imageId).HasColumnName("圖片編號Image_ID");

                entity.Property(e => e.圖片image)
                    .HasMaxLength(200)
                    .HasColumnName("圖片Image");

                entity.Property(e => e.圖片雲端imageImdb)
                    .HasMaxLength(200)
                    .HasColumnName("圖片雲端ImageIMDB")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.圖片類型imageType)
                    .HasMaxLength(3)
                    .HasColumnName("圖片類型ImageType")
                    .IsFixedLength(true);

                entity.Property(e => e.屏蔽invisible).HasColumnName("屏蔽Invisible");

                entity.Property(e => e.電影名稱movieName)
                    .HasMaxLength(100)
                    .HasColumnName("電影名稱MovieName");
            });

            modelBuilder.Entity<電影導演movieDirector>(entity =>
            {
                entity.HasKey(e => e.電影導演編號mdId)
                    .HasName("PK_電影導演 Movie_Director");

                entity.ToTable("電影導演MovieDirector");

                entity.Property(e => e.電影導演編號mdId).HasColumnName("電影導演編號MD_ID");

                entity.Property(e => e.導演編號directorId).HasColumnName("導演編號Director_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.導演編號director)
                    .WithMany(p => p.電影導演movieDirectors)
                    .HasForeignKey(d => d.導演編號directorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影導演 Movie_Director_導演總表 Director");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影導演movieDirectors)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影導演MovieDirector_電影Movies");
            });

            modelBuilder.Entity<電影排行movieRank>(entity =>
            {
                entity.HasKey(e => e.排行編號rankId);

                entity.ToTable("電影排行MovieRank");

                entity.Property(e => e.排行編號rankId).HasColumnName("排行編號Rank_ID");

                entity.Property(e => e.周末票房boxOfficeWeekend)
                    .HasMaxLength(50)
                    .HasColumnName("周末票房BoxOffice_Weekend");

                entity.Property(e => e.周次weeks)
                    .HasMaxLength(50)
                    .HasColumnName("周次Weeks");

                entity.Property(e => e.累積票房boxOfficeGross)
                    .HasMaxLength(50)
                    .HasColumnName("累積票房BoxOffice_Gross");

                entity.Property(e => e.統計時間).HasMaxLength(50);

                entity.Property(e => e.電影movie)
                    .HasMaxLength(100)
                    .HasColumnName("電影Movie");

                entity.Property(e => e.電影排名movieRank)
                    .HasMaxLength(5)
                    .HasColumnName("電影排名Movie_Rank");

                entity.Property(e => e.電影英movieEn)
                    .HasMaxLength(100)
                    .HasColumnName("電影英Movie_En");
            });

            modelBuilder.Entity<電影片種movieType>(entity =>
            {
                entity.HasKey(e => e.電影片種編號mtId)
                    .HasName("PK_電影片種 Movie Type");

                entity.ToTable("電影片種MovieType");

                entity.Property(e => e.電影片種編號mtId).HasColumnName("電影片種編號MT_ID");

                entity.Property(e => e.片種編號typeId).HasColumnName("片種編號Type_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.片種編號type)
                    .WithMany(p => p.電影片種movieTypes)
                    .HasForeignKey(d => d.片種編號typeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影片種MovieType_片種總表TotalTypes");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影片種movieTypes)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影片種MovieType_電影Movies");
            });

            modelBuilder.Entity<電影產地movieOrigin>(entity =>
            {
                entity.HasKey(e => e.電影產地編號mcId)
                    .HasName("PK_電影產地 Movie Origin");

                entity.ToTable("電影產地MovieOrigin");

                entity.Property(e => e.電影產地編號mcId).HasColumnName("電影產地編號MC_ID");

                entity.Property(e => e.國家編號countryId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("國家編號Country_ID");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.國家編號country)
                    .WithMany(p => p.電影產地movieOrigins)
                    .HasForeignKey(d => d.國家編號countryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影產地 Movie Origin_國家總表 Countrys");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影產地movieOrigins)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影產地MovieOrigin_電影Movies");
            });

            modelBuilder.Entity<電影評論movieComment>(entity =>
            {
                entity.HasKey(e => e.評論編號commentId)
                    .HasName("PK_電影評論 Movie Comment");

                entity.ToTable("電影評論MovieComment");

                entity.Property(e => e.評論編號commentId).HasColumnName("評論編號Comment_ID");

                entity.Property(e => e.公開等級編號publicId).HasColumnName("公開等級編號Public_ID");

                entity.Property(e => e.屏蔽invisible).HasColumnName("屏蔽Invisible");

                entity.Property(e => e.是否開放討論串oxFloor).HasColumnName("是否開放討論串OX_Floor");

                entity.Property(e => e.會員編號memberId).HasColumnName("會員編號Member_ID");

                entity.Property(e => e.期待度anticipation).HasColumnType("numeric(2, 1)");

                entity.Property(e => e.發佈時間commentTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("發佈時間Comment_Time");

                entity.Property(e => e.觀影方式source)
                    .HasMaxLength(50)
                    .HasColumnName("觀影方式Source");

                entity.Property(e => e.觀影日期viewingTime)
                    .HasColumnType("date")
                    .HasColumnName("觀影日期Viewing_Time");

                entity.Property(e => e.評分rate)
                    .HasColumnType("numeric(2, 1)")
                    .HasColumnName("評分Rate");

                entity.Property(e => e.評論內容comments).HasColumnName("評論內容Comments");

                entity.Property(e => e.評論標題commentTitle)
                    .HasMaxLength(50)
                    .HasColumnName("評論標題Comment_Title");

                entity.Property(e => e.電影編號movieId).HasColumnName("電影編號Movie_ID");

                entity.HasOne(d => d.公開等級編號public)
                    .WithMany(p => p.電影評論movieComments)
                    .HasForeignKey(d => d.公開等級編號publicId)
                    .HasConstraintName("FK_電影評論MovieComment_公開等級編號Public_ID");

                entity.HasOne(d => d.會員編號member)
                    .WithMany(p => p.電影評論movieComments)
                    .HasForeignKey(d => d.會員編號memberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影評論MovieComment_會員Members");

                entity.HasOne(d => d.電影編號movie)
                    .WithMany(p => p.電影評論movieComments)
                    .HasForeignKey(d => d.電影編號movieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影評論MovieComment_電影Movies");
            });

            modelBuilder.Entity<電影語言movieLanguage>(entity =>
            {
                entity.HasKey(e => e.語言編號languageId)
                    .HasName("PK_電影語言 Movie Language");

                entity.ToTable("電影語言MovieLanguage");

                entity.Property(e => e.語言編號languageId).HasColumnName("語言編號Language_ID");

                entity.Property(e => e.語言名稱languageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("語言名稱Language_Name");
            });

            modelBuilder.Entity<電影院theater>(entity =>
            {
                entity.HasKey(e => e.電影院編號theaterId)
                    .HasName("PK_電影院 Theater");

                entity.ToTable("電影院Theater");

                entity.Property(e => e.電影院編號theaterId).HasColumnName("電影院編號Theater_ID");

                entity.Property(e => e.地址address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("地址Address");

                entity.Property(e => e.官方網站url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("官方網站URL");

                entity.Property(e => e.影城編號mainTheaterId).HasColumnName("影城編號MainTheater_ID");

                entity.Property(e => e.營業時間businessHours)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("營業時間BusinessHours");

                entity.Property(e => e.電影院名稱theaterName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("電影院名稱Theater_Name");

                entity.Property(e => e.電影院照片image)
                    .HasMaxLength(200)
                    .HasColumnName("電影院照片Image");

                entity.Property(e => e.電話phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("電話Phone");

                entity.HasOne(d => d.影城編號mainTheater)
                    .WithMany(p => p.電影院theaters)
                    .HasForeignKey(d => d.影城編號mainTheaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_電影院Theater_影城MainTheater");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
