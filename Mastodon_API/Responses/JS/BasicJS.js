let promoOpen = false;

function osoSliderClicked() {
    if (!promoOpen) {
        document.documentElement.style.overflowX = 'hidden';
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
    document.getElementById('osoContactForm').style.display = 'none';
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