var currentImage = false;
window.onload = function () {
    var currentImage = document.getElementById("slickImage");
    var image = document.getElementById("ContactUs2");
    currentImage.src = image.src;
};
var slickSliderOpen;
function slickSliderClicked() {
    if (!this.slickSliderOpen) {
        document.documentElement.style.overflowX = 'hidden';
        document.getElementById("slickContactForm").style.visibility = "visible";
        document.getElementById("slickSlider").style.right = "-300px";
        document.getElementById("slickImage").style.right = "300px";
        showSlickSlider();
        document.documentElement.style.overflowX = 'inherit';
    }
    else {
        closeSlickSlider();
    }
    this.slickSliderOpen = !this.slickSliderOpen;
}
function closeSlickSlider() {
    document.getElementById("slickContactForm").style.visibility = "hidden";
    document.getElementById("slickImage").style.right = "0px";
}
function showSlickSlider() {
    var slidingDiv = document.getElementById("slickSlider");
    var stopPosition = 0;
    if (parseInt(slidingDiv.style.right) < stopPosition) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + "px";
        setTimeout(showSlickSlider, 1);
    }
}
//# sourceMappingURL=clientSlider.js.map