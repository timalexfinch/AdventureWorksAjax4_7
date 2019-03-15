'use strict';

$(function () {

    $("#btnGetRandomUser").click(function () {
        $.ajax({
             url: 'https://randomuser.me/api/',
            dataType: 'json',
            success: gotUser,
            error: AjaxFailed
        });
    });
});

function gotUser(user) {
    var thisUser = user.results[0];

    $('#randomUser').html(
        '<img id=userPicture src=' + thisUser.picture.large + ' /><br /><br />' +
        thisUser.name.title + ' ' +
        thisUser.name.first + ' ' +
        thisUser.name.last + '<br />' +
        thisUser.location.street + '<br />' +
        thisUser.location.city + '<br />' +
        thisUser.nat + '<br />' +
        thisUser.email + '<br />' +
        'Username: ' +
        thisUser.login.username + '<br /><br />'
    )
        .css({
            'background-color' : 'lightblue',
            'color': 'red',
            'font-size' : 'large'
        });
}

function AjaxFailed(response) {
    alert(response.status + ' ' + response.statusText);
}