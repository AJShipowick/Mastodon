"use strict";

function onGoogleLogin(googleUser) {

    let user = googleUser.getBasicProfile();
    axios.get('RegisterGoogleUser' + "?name=" + user.getName() + "&email=" + user.getEmail())
        .then(function (response) {
            if (response.data === "Success") {
                $("#googleLoginStatus").show();
                setTimeout(
                    function () {
                        //wait a few seconds so the user knows they are being logged in...
                        window.location.href = '/Dashboard';
                    }, 2000);
            }
        })
        .catch(function (error) {
            //todo Handle errors
        });
}

function onGoogleFailure(error) {
    //todo, handle login error
}

function showGoogleLogin() {
    gapi.signin2.render('google-signin', {
        'scope': 'profile email',
        'width': 240,
        'height': 50,
        'longtitle': true,
        'theme': 'dark',
        'onsuccess': onGoogleLogin,
        'onfailure': onGoogleFailure
    });
}