(function () {

    let path = $(location).attr('pathname').toUpperCase();

    if (path.indexOf("FEATURES") !== -1) {
        $('#features').addClass('navClicked')
    } else if (path.indexOf("PRICING") !== -1) {
        $('#pricing').addClass('navClicked')
    } else if (path.indexOf("DASHBOARD") !== -1 || (path.indexOf("DETAILS")) !== -1) {
        $('#dashboard').addClass('navClicked')
    } else if (path.indexOf("CREATEPROMO") !== -1) {
        $('#createPromo').addClass('navClicked')
    } else if (path.indexOf("MANAGE") !== -1) {
        $('#manage').addClass('navClicked')
    } else if (path.indexOf("STATS") !== -1) {
        $('#activeStats').addClass('navClicked')
    } else if (path.indexOf("REGISTER") !== -1) {
        $('#register').addClass('navClicked')
    } else if (path.indexOf("LOGIN") !== -1) {
        $('#login').addClass('navClicked')
    } else {
        $('#home').addClass('navClicked')
    }

})();