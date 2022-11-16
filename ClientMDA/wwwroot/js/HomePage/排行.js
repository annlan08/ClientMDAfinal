const openModalButtons = document.querySelectorAll('#iii')
const closeModalButtons = document.querySelectorAll('[data-close-button]')
const overlay = document.querySelector('#overlay')
const headerofstar = document.getElementById("in-rating-movie");
const movienum = document.getElementById('movienum');
const inputstar = document.querySelectorAll('.inputstar')
const 電影編號in = document.getElementById('movienum')
const 評論編號in = document.getElementById('memrankid')
const 評論分數in = document.getElementById('scorenumber')
const x = document.getElementById('評論編號Comment_ID')


function disableBtn() {
    document.getElementById("submit").style.backgroundColor = "dimgrey";
    document.getElementById("submit").disabled = true;
}
function openModal(modal) {
    if (modal == null) return
    modal.classList.add('active')
    overlay.classList.add('active')
}

function closeModal(modal) {
    if (modal == null) return
    modal.classList.remove('active')
    overlay.classList.remove('active')
    disableBtn();
    $("#deletestar").css("display", 'none')
    resetmodel()
    rankposter();
    newmovie();
    listmovie();

}

function resetmodel() {
    rm.innerText = "?"
    $("#1").prop('checked', false);
    $("#2").prop('checked', false);
    $("#3").prop('checked', false);
    $("#4").prop('checked', false);
    $("#5").prop('checked', false);
}

for (let i = 0; i < openModalButtons.length; i++) {
    openModalButtons[i].addEventListener('click', e => {
        
        const modal = document.querySelector("#modal")
        openModal(modal)
        const f = $(event.currentTarget).parents(".box").find("h5")[0].textContent
        const o = $(event.currentTarget).parents(".box").find("a")[1].getAttribute('data')
        const g = $(event.currentTarget).parents(".box").find("a")[1].getAttribute('data-rank')
        const ef = $(event.currentTarget).parents(".box").find("a")[1].getAttribute('data-rate')
        headerofstar.textContent = f
        電影編號in.value = o;
        document.getElementById('memrankid').value = g;
        document.getElementById('scorenumber').value = ef;
        if ($('#會員編號Member_ID').val() != 0) {
            if (document.getElementById('scorenumber').value == 1 || document.getElementById('scorenumber').value == 2 || document.getElementById('scorenumber').value == 3 || document.getElementById('scorenumber').value == 4 || document.getElementById('scorenumber').value == 5) {
                rm.innerText = document.getElementById('scorenumber').value;
                $("#deletestar").css("display", 'inline')
                if (rm.innerText == 1) {
                    $("#1").prop('checked', true);
                }
                else if (rm.innerText == 2) {
                    $("#1").prop('checked', true);
                    $("#2").prop('checked', true);
                }
                else if (rm.innerText == 3) {
                    $("#1").prop('checked', true);
                    $("#2").prop('checked', true);
                    $("#3").prop('checked', true);
                }
                else if (rm.innerText == 4) {
                    $("#1").prop('checked', true);
                    $("#2").prop('checked', true);
                    $("#3").prop('checked', true);
                    $("#4").prop('checked', true);
                }
                else if (rm.innerText == 5) {
                    $("#1").prop('checked', true);
                    $("#2").prop('checked', true);
                    $("#3").prop('checked', true);
                    $("#4").prop('checked', true);
                    $("#5").prop('checked', true);
                }

            }
            else {
                rankposter();
                newmovie();
                listmovie();
                $("#deletestar").css("display", 'none')
                resetmodel()
            }
        }
    })
}

overlay.addEventListener('click', () => {
    const modals = document.querySelectorAll('.modalst.active')
    modals.forEach(modal => {
        closeModal(modal)
    })
})

closeModalButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.modalst')
        closeModal(modal)
    })
})


