"use strict";

let currentImage = false;
function showCustomSliderImage() {
    $("#sliderContainer").show()
    let currentImage = document.getElementById("slickImage");

    if (newPromoApp.Promotion.ImageName) {
        let image = document.getElementById(newPromoApp.Promotion.ImageName);
        currentImage.src = image.src;

        $(image).addClass('animated bounceIn');  //Animation only fires 1 time for each image...
    }
}

let slickSliderOpen;
function slickSliderClicked() {
    if (!slickSliderOpen) {
        document.documentElement.style.overflowX = 'hidden';
        document.getElementById('slickContactForm').style.visibility = 'visible';
        document.getElementById('sliderContainer').style.right = '-300px';
        document.getElementById('slickImage').style.cssFloat = 'left';
        showSlickSlider();
        document.documentElement.style.overflowX = 'inherit';
        document.getElementById('sliderContainer').style.zIndex = "10";
    }
    else {
        closeSlickSlider();
    }
    slickSliderOpen = !slickSliderOpen;
}
function closeSlickSlider() {
    document.getElementById('slickContactForm').style.visibility = 'hidden';
    document.getElementById('slickImage').style.cssFloat = 'right';
    document.getElementById('sliderContainer').style.zIndex = "0";

}

function showSlickSlider() {
    let slidingDiv = document.getElementById('sliderContainer');
    let stopPosition = 0;
    if (parseInt(slidingDiv.style.right) < stopPosition) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + 'px';
        setTimeout(showSlickSlider, 1);
    }
}