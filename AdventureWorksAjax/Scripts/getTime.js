"use strict";

$(function () {
    $("#btnGetTime").click(function () {
        $.ajax({
            url: "/Home/GetTime",
            success: GetTimeSucceeded,
            error: AjaxFailed,
            cache: false
        });
    });
});

function GetTimeSucceeded(response) {
    $("#whatsTheTime").html(response).css({ "background-color" : "yellow", "color" : "blue","fontsize":"larger" });
}

function AjaxFailed(response) {
    alert(response.status + ' ' + response.statusText);
}
