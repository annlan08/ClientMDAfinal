let flag = false;
$(".dateHidden .datebox").click(function () {
    $(this).removeClass("checked").siblings().removeClass("checked").end().addClass("checked");
});
$(".sssss").click(function () {
    $(".sts").attr('style', 'display:none');
    $(".sssss").attr('style', 'display:none');
    $("#test2").attr('style', 'display:inline');
});
$("#test2").click(function () {
    $(".sts").attr('style', 'display:inline');
    $("#test2").attr('style', 'display:none');
    $(".sssss").attr('style', 'display:inline');
});
const play = document.querySelector("#videostart")
const 影片 = document.querySelector("#video")
const 關閉 = document.querySelector("#close")
const area = document.querySelector("#地區")
const areaCH = document.querySelector("#地區選擇")
const 北 = document.querySelector("#no01")
const 桃 = document.querySelector("#no02")
const 中 = document.querySelector("#no03")
const 南 = document.querySelector("#no04")
play.addEventListener("click", () => {
    console.log("ok")
    影片.style.display = "inline"

})
關閉.addEventListener("click", () => {
    影片.style.display = "none"
})
area.addEventListener("click", () => {
    if (areaCH.style.display == "inline") { areaCH.style.display = "none" }
    else
        areaCH.style.display = "inline"
})
北.addEventListener("click", () => {
    document.querySelector("#北部").style.display = "inline"
    areaCH.style.display = "none"
    document.querySelector("#地區名稱").innerHTML = "北區"
    console.log(北.innerHTML)
})
桃.addEventListener("click", () => {
    document.querySelector("#北部").style.display = "none"

    areaCH.style.display = "none"
    document.querySelector("#地區名稱").innerHTML = "桃竹苗"
})
中.addEventListener("click", () => {
    document.querySelector("#北部").style.display = "none"
    areaCH.style.display = "none"
    document.querySelector("#地區名稱").innerHTML = "中區"
})
南.addEventListener("click", () => {
    document.querySelector("#北部").style.display = "none"
    areaCH.style.display = "none"
    document.querySelector("#地區名稱").innerHTML = "南區及離島"
})
