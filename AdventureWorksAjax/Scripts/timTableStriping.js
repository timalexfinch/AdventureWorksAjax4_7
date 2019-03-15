"use strict";

$(function () {
    $("#tblTwo tr:even").addClass("forestGreen");
    $("#tblTwo tr:odd").addClass("yellowGreen");

    $("#tblTwo tr").mouseover(function () {
        $(this).removeClass("forestGreen")
            .addClass("redbg");
    })
        .mouseout(function () {
            $(this).removeClass("redbg");
            $("#tblTwo tr:even").addClass("forestGreen");
            $("#tblTwo tr:odd").addClass("yellowGreen");
        });

});