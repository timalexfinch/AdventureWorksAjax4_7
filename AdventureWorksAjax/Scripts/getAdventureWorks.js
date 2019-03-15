'use strict';
$(function () {
    $("body").on("click", "#productsTable .pagination a", function (event) {
        event.preventDefault();
        var url = $(this).attr("href");

        $.ajax({
            url: url,
            success: function (result) {
                $("#productsTable").html(result);
            },
            failure: function (result) {
                alert(result.status);
            }
        });
    });
});