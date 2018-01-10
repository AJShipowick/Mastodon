"use strict";

let currentImage = false;
let slickSliderOpen = false;

function showCustomSliderImage() {
    document.getElementById('sliderContainer').style.display = 'block';
    let currentImage = document.getElementById("slickImage");
}

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

function submitSlider() {
    document.getElementById('thankYou').style.display = 'block';
    document.getElementById('osoFormInput').style.display = 'none';
}