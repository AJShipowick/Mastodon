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
    document.getElementById('osoContainer').style.oso_side_of_screen = '-300px'
    document.getElementById('osoImage').style.oso_side_of_screen = '0px';
    document.getElementById('osoContactForm').style.display = 'none';
    document.getElementById('osoContainer').style.zIndex = '0';
}

function slidePromo() {
    document.getElementById('osoContactForm').style.display = 'block';
    let slidingDiv = document.getElementById('osoContainer');
    let stopPosition = 0;
    if (parseInt(slidingDiv.style.oso_side_of_screen) < stopPosition) {
        slidingDiv.style.oso_side_of_screen = parseInt(slidingDiv.style.oso_side_of_screen) + 5 + 'px';
        setTimeout(slidePromo, 1);
    }
}

function slidePromoImage() {
    let slidingDiv = document.getElementById('osoImage');
    let stopPosition = 0;
    if (parseInt(slidingDiv.style.oso_side_of_screen) < 300) {
        slidingDiv.style.oso_side_of_screen = parseInt(slidingDiv.style.oso_side_of_screen) + 5 + 'px';
        setTimeout(slidePromoImage, 1);
    }
}