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
    }
    else {
        closePromotion();
    }
    promoOpen = !promoOpen;
}
function closePromotion() {
    document.getElementById('osoContainer').style.right = '-300px'
    document.getElementById('osoImage').style.right = '0px';
    document.getElementById('osoContactForm').style.visibility = 'hidden';
    document.getElementById('osoContainer').style.zIndex = '0';
}

function slidePromo() {
    let slidingDiv = document.getElementById('osoContainer');
    let stopPosition = 0;
    if (parseInt(slidingDiv.style.right) < stopPosition) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + 'px';
        setTimeout(slidePromo, 1);
    }
}

function slidePromoImage() {
    let slidingDiv = document.getElementById('osoImage');
    let stopPosition = 0;
    if (parseInt(slidingDiv.style.right) < 300) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + 'px';
        setTimeout(slidePromoImage, 1);
    }
}

//function submitOSOEasyPromotion() {
//    document.getElementById('thankYou').style.display = 'block';
//    document.getElementById('osoFormInput').style.display = 'none';
//}