
//----------------------------------------------------------------------------------------------------------評分視窗 Start

const openModalButtons = document.querySelectorAll('[data-modal-target]')//宣告呼叫評分視窗
const closeModalButtons = document.querySelectorAll('[data-close-button]')//宣告關閉評分視窗
const overlay = document.getElementById('overlay')
const headerofstar = document.getElementById("ipc-rating-prompt__content-title");//宣告評分視窗-電影標題

for (let i = 0; i < openModalButtons.length; i++) {
    openModalButtons[i].addEventListener('click', event => {
        const MovieRate = document.querySelector("#MovieRate")
        openModal(MovieRate)
        //const f = $(event.currentTarget).parents(".box").find("h5")[0].textContent
        //headerofstar.textContent = f
    })
}

overlay.addEventListener('click', () => {//呼叫評分視窗
    const modals = document.querySelectorAll('.MovieRate.active')
    modals.forEach(modal => {
        closeModal(modal)
    })
})

closeModalButtons.forEach(button => {//關閉評分視窗
    button.addEventListener('click', () => {
        const modal = button.closest('.MovieRate')
        closeModal(modal)
    })
})

function openModal(modal) {
    if (modal == null) return
    modal.classList.add('active')
    overlay.classList.add('active')
}

function closeModal(modal) {
    if (modal == null) return
    modal.classList.remove('active')
    overlay.classList.remove('active')
}

//----------------------------------------------------------------------------------------------------------評分視窗 End


//----------------------------------------------------------------------------------------------------------篩選器按鈕 Start

    //$(document).ready(function () {
    //    $("#btnCk1check0").click(function () {
    //        $(".btnCk1").toggle();
    //    });
    //});

$("#btnCk1check0").click(function () {
    $(this).siblings().removeClass("aBorderF.btn-check:checked + .btn-outline-rankocus").end().addClass("aBorderFocus");
});

$("#btnCk1check0").click(function () {
    /*方法2-1 JQ addClass失敗*/
    /*$(this).siblings().removeClass("aBorderFocus").end().addClass("aBorderFocus");*/
    /*方法2-2 JQ addCss失敗*/
    //$(this).css("background", "#00b0f0")
    //$(this).siblings().css("background", "transparent") 
    /*方法2-3 JQ addClass失敗*/
        $(this).removeClass("AAA").siblings().removeClass("AAA").end().addClass("aBorderFocus");
})
    
//----------------------------------------------------------------------------------------------------------篩選器按鈕 End