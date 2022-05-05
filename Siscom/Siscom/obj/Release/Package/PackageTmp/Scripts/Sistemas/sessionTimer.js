var HsTimer;
var segundos = 0;
var count = 0;
var countSession = 0;
var HsTimer;
localStorage.setItem("countSession", 0);

if (localStorage.getItem("stateSession") === null) {
    localStorage.setItem("stateSession", "false");
} else {
    localStorage.setItem("stateSession", "true");
}

var explode = function () {
    countSession=localStorage.getItem("countSession");
    count++;
    if (localStorage.getItem("stateSession") == "true") {
        segundos = 0; 
    } 
    segundos++;
    if (count == 119) {
        localStorage.setItem("stateSession", "false");
        segundos = segundos;
        count = 0;
    }
    if ((segundos / 60) == 30) {
        countSession++;
        segundos = 0;
        localStorage.setItem("countSession", countSession);
        var src = $("#KeepAliveFrame").attr("src");
        $("#KeepAliveFrame").attr("src", src);
    }
    if (localStorage.getItem("countSession") == 0) {
        $('#ModalSession').modal('hide');
    } else {
        $('#ModalSession').modal('show');
    }
    if (localStorage.getItem("countSession") == 99) HsTimerStop();
    if (localStorage.getItem("countSession") == 4) {
        var url = "/Login/LogOut";
        $(location).attr('href', url);
    }
    HsTimer = setTimeout(explode, 1000);
};

function HsTimerStart() {
    explode();
}

function HsTimerRestart() {
    segundos = 0;
    countSession = 0;
    count = 0;
    localStorage.setItem("countSession", countSession);
    localStorage.setItem("stateSession", "true");
    var src = $("#KeepAliveFrame").attr("src");
    $("#KeepAliveFrame").attr("src", src);
    $('#ModalSession').modal('hide');
}

function HsTimerStop() {
    localStorage.setItem("countSession", 99);
    var url = "/Login/LogOut";
    $(location).attr('href', url);
}

HsTimerStart();