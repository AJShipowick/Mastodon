"use strict";

let currentImage = false;
let promoOpen = false;

function showCustomPromoImage() {
    document.getElementById('osoContainer').style.display = 'block';
    let currentImage = document.getElementById('osoImage');
}

function osoSliderClicked() {
    if (!promoOpen) {
        document.documentElement.style.overflowX = 'hidden';
        document.getElementById('osoContactForm').style.visibility = 'visible';
        document.getElementById('osoContainer').style.zIndex = '999';
        slidePromo();
        slidePromoImage();
        promoOpen = true;
    }
    else {
        closePromotion();        
    }
}
function closePromotion() {
    if (newPromoApp.PromoModel.sideOfScreen === "right") {
        document.getElementById('osoContainer').style.right = '-300px';
        document.getElementById('osoImage').style.right = '0px';

    } else {
        document.getElementById('osoContainer').style.left = '-300px';
        document.getElementById('osoImage').style.left = '0px';
    }

    document.getElementById('osoContactForm').style.visibility = 'hidden';
    document.getElementById('osoContainer').style.zIndex = '0';

    promoOpen = false;
}

function slidePromo() {
    let slidingDiv = document.getElementById('osoContainer');
    let stopPosition = 0;

    if (newPromoApp.PromoModel.sideOfScreen === "right") {
        if (parseInt(slidingDiv.style.right) < stopPosition) {
            slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + 'px';
            setTimeout(slidePromo, 1);
        }
    } else {
        if (parseInt(slidingDiv.style.left) < stopPosition) {
            slidingDiv.style.left = parseInt(slidingDiv.style.left) + 5 + 'px';
            setTimeout(slidePromo, 1);
        }
    }

}

function slidePromoImage() {
    let slidingDiv = document.getElementById('osoImage');
    let stopPosition = 0;

    if (newPromoApp.PromoModel.sideOfScreen === "right") {
        if (parseInt(slidingDiv.style.right) < 300) {
            slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + 'px';
            setTimeout(slidePromoImage, 1);
        }
    } else {
        if (parseInt(slidingDiv.style.left) < 300) {
            slidingDiv.style.left = parseInt(slidingDiv.style.left) + 5 + 'px';
            setTimeout(slidePromoImage, 1);
        }
    }
}

function submitOSOEasyPromotion() {
    document.getElementById('thankYou').style.display = 'block';
    document.getElementById('osoFormInput').style.display = 'none';
}