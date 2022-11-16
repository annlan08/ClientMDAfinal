
    var pmer = 0;
    const help = document.querySelector(".小幫手")
    const helper = document.querySelector(".幫幫忙")

    $(".hope").click(function () {
        pmer++
            if (pmer == 1) {
        help.style.display = "inline";
    helper.style.top = "41%"

            }
            else if (pmer>1){
        pmer = 0
                help.style.display = "none"
    helper.style.top = "90%"

            }
        })
