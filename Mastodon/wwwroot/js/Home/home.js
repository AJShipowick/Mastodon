function animateHomeImage(imageID) {
    $('#' + imageID).addClass('animated tada');
}

$(document).ready(function () {
    $("#contentContainer").removeClass("container");
    $("#contentContainer").removeClass("body-content");
});