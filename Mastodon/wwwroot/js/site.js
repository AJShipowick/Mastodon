
//USE TYPESCRIPT!!
//https://code.visualstudio.com/docs/languages/typescript

//Setting image src to an item, needs to be updated to reflect user selection of image as well...
let currentImage = document.getElementById("slickImage");
currentImage.src = document.getElementById("ContactUs1").src;


let slickSliderOpen = false;
function slickSliderClicked() {

    if (!slickSliderOpen) {
        document.documentElement.style.overflowX = 'hidden';

        document.getElementById("slickContactForm").style.visibility = "visible";
        document.getElementById("slickSlider").style.right = "-300px";
        document.getElementById("slickImage").style.right = "300px";
        showSlickSlider();

        document.documentElement.style.overflowX = 'inherit';
    } else {
        closeSlickSlider()
    }

    slickSliderOpen = !slickSliderOpen;
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