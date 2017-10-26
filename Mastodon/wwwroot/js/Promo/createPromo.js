"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        Promotion: []
    },
    created: function () {
        //get settings onLoad
        getPromotionModel();
    }
});

$(function () {
    $('#promoDate .input-group.date').datepicker({
        format: 'mm/dd/yyyy',
        maxViewMode: 2,
        autoclose: true,
        todayHighlight: true
    }).on('changeDate', function (ev) {
        //Have to force Vue to update date after datepicker changes date value in text input
        newPromoApp.Promotion.EndDate = ev.date.toLocaleDateString();
    });
});

function getPromotionModel() {
    axios.get('/Promotion/CreatePromo/GetPromoModel')
        .then(function (response) {
            newPromoApp.Promotion = response.data;

            if (newPromoApp.Promotion.Id) {
                $("#deletePromoBtn").show();
                $("#slickContactForm").css({ "background-color": newPromoApp.Promotion.BackgroundColor });
                $("#sliderButton").css({ "background-color": newPromoApp.Promotion.ButtonColor });
                if (newPromoApp.Promotion.ShowCouponBorder) { $("#slickContactForm").css({ "border": "4px dashed #ccc" });}
            } else {
                //New promo, no slider selected, select 1st image for user and colors
                newPromoApp.Promotion.ImageName = "promo1";
                newPromoApp.Promotion.BackgroundColor = "#ffffff";
                newPromoApp.Promotion.ButtonColor = "#4CAF50";
            }

            showCustomSliderImage();
        })
        .catch(function (error) {
            //todo handle errors
        });
}

function showCouponBorder() {
    if (!newPromoApp.Promotion.ShowCouponBorder) {
        $("#slickContactForm").css({ "border": "4px dashed #ccc" });
    } else {
        $("#slickContactForm").css({ "border": "1px solid #d8d8d8" });
    }
}

function setFormBackgroundColor() {
    let selectedColor = $("#backgroundColor").val();
    $("#slickContactForm").css({ "background-color": selectedColor });
}

function setFormButtonColor() {
    let selectedColor = $("#buttonColor").val();
    $("#sliderButton").css({ "background-color": selectedColor });
}

function submitSlider() {
    $("#submitSliderClick").modal('show');
}

function saveCustomSettings(activatePromo, responseMessageId) {
    $("#saveSuccessMsg").hide();
    $("#saveSuccessMsg").removeClass('animated fadeInDown');
    $("#activateSuccessMsg").hide();
    $("#activateSuccessMsg").removeClass('animated fadeInDown');

    if (!validForm()) { return; }

    axios.post('/Promotion/CreatePromo/SaveNewPromo',
        newPromoApp.Promotion
    )
        .then(function (response) {
            if (!activatePromo) {
                $("#" + responseMessageId).show();
                $("#" + responseMessageId).addClass('animated fadeInDown');
            } else {
                activatePromoNow("");
            }
        })
        .catch(function (error) {
            //todo handle errors
        });
}

function validForm() {

    hideErrors();
    let validForm = true;

    if (!newPromoApp.Promotion.Title) {
        $("#titleMissing").show()
        validForm = false;
    }

    if (!newPromoApp.Promotion.EndDate) {
        $("#dateMissing").show()
        validForm = false;
    }

    if (!newPromoApp.Promotion.Code) {
        $("#codeMissing").show()
        validForm = false;
    }

    if (!newPromoApp.Promotion.Discount) {
        $("#discountMissing").show()
        validForm = false;
    }

    return validForm;
}

function hideErrors() {
    $("#titleMissing").hide()
    $("#dateMissing").hide()
    $("#codeMissing").hide()
    $("#discountMissing").hide()
}

function activatePromoNow(promoId) {
    axios.get('/Promotion/CreatePromo/ActivatePromo?promoId=' + promoId)
        .then(function (response) {
            $("#activateSuccessMsg").show();
            $("#activateSuccessMsg").addClass('animated fadeInDown');
        })
        .catch(function (error) {
            //todo Handle errors
        });
}

function deletePromo() {
    $("#deletePromoModal").modal('show');
}

function confirmDeletePromo() {
    axios.get('/Promotion/CreatePromo/DeletePromo?promoId=' + newPromoApp.Promotion.Id)
        .then(function (response) {
            window.location.href = '/Dashboard';
        })
        .catch(function (error) {
            //todo Handle errors
        });
}

function showHelpStep1() {
    $("#promoHelpModalStep1").modal('show');
}

function showHelpStep2() {
    $("#promoHelpModalStep2").modal('show');
    $("#promoHelpModalStep1").modal('hide');
}

function closeHelpStep2() {
    $("#promoHelpModalStep2").modal('hide');
}
