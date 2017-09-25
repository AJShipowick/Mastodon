let currentImage: any = false;

window.onload = function (){
    let currentImage: any = document.getElementById("slickImage");
    let image: any = document.getElementById("ContactUs2");
    currentImage.src = image.src;
}

let slickSliderOpen: boolean;
function slickSliderClicked(): void {
    if (!this.slickSliderOpen) {
        document.documentElement.style.overflowX = 'hidden';

        document.getElementById("slickContactForm").style.visibility = "visible";
        document.getElementById("slickSlider").style.right = "-300px";
        document.getElementById("slickImage").style.right = "300px";
        showSlickSlider();

        document.documentElement.style.overflowX = 'inherit';
    } else {
        closeSlickSlider()
    }

    this.slickSliderOpen = !this.slickSliderOpen;
}

function closeSlickSlider(): void {
    document.getElementById("slickContactForm").style.visibility = "hidden";
    document.getElementById("slickImage").style.right = "0px";
}

function showSlickSlider(): void {
    let slidingDiv: HTMLElement = document.getElementById("slickSlider");
    let stopPosition: number = 0;
    if (parseInt(slidingDiv.style.right) < stopPosition) {
        slidingDiv.style.right = parseInt(slidingDiv.style.right) + 5 + "px";
        setTimeout(showSlickSlider, 1);
    }
}