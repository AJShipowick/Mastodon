var sliderApp = new Vue({
    el: '#sliderApp',
    data: {
        ClientsWebsite: [],
        axajLoading: true,
        picked: []
    },
    created: function () {
        getUserSettings()
    }
})

function getUserSettings() {
    axios.get('/Slider/Slider/GetUserSettings')
        .then(function (response) {
            sliderApp.ClientsWebsite = response.data;
            $("#vueData").show();
            showCustomSliderImage();
            sliderApp.axajLoading = false;
        })
        .catch(function (error) {
            //todo Handle errors
        });
}

function saveCustomSettings() {
    $("#saveSuccessMsg").hide();
    $("#saveSuccessMsg").removeClass('animated fadeInDown');

    axios.post('/Slider/Slider/SaveCustomSettings',
        sliderApp.ClientsWebsite
    )
        .then(function (response) {
            $("#saveSuccessMsg").show();
            $("#saveSuccessMsg").addClass('animated fadeInDown');
        })
        .catch(function (error) {
            //todo handle errors
        });
}

//todo seperate out below stuff....
var currentImage = false;
function showCustomSliderImage() {
    var currentImage = document.getElementById("slickImage");

    if (sliderApp.ClientsWebsite.SliderImageName) {
        var image = document.getElementById(sliderApp.ClientsWebsite.SliderImageName);
        currentImage.src = image.src;

        $(image).addClass('animated bounceIn');  //Animation only fires 1 time for each image...
    }
}


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