﻿var slickSliderOpen;
function slickSliderClicked() {
    if (!this.slickSliderOpen) {
        document.documentElement.style.overflowX = 'hidden';
        document.getElementById('slickContactForm').style.visibility = 'visible';
        document.getElementById('sliderContainer').style.right = '-200px';
        document.getElementById('slickImage').style.cssFloat = 'left';
        showSlickSlider();
        document.documentElement.style.overflowX = 'inherit';
    }
    else {
        closeSlickSlider();
    }
    this.slickSliderOpen = !this.slickSliderOpen;
}
function closeSlickSlider() {
    document.getElementById('slickContactForm').style.visibility = 'hidden';
    document.getElementById('slickImage').style.cssFloat = 'right';
}
function showSlickSlider() {
    var slidingDiv = document.getElementById('sliderContainer');
    var stopPosition = 0;
    if (parseInt(slidingDiv.style.right) < stopPosition) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 3 + 'px';
        setTimeout(showSlickSlider, 1);
    }
}