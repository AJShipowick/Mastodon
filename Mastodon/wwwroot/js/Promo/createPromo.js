"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        Promotion: []
    },
    created: function () {
        //get settings onLoad
        getPromotionModel()
    }
})

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
                showCustomSliderImage();
            }
        })
        .catch(function (error) {
            //todo handle errors
        });
}

function saveCustomSettings(activatePromo, responseMessageId) {
    $("#" + responseMessageId).hide();
    $("#" + responseMessageId).removeClass('animated fadeInDown');

    axios.post('/Promotion/CreatePromo/SaveNewPromo',
        newPromoApp.Promotion
    )
        .then(function (response) {
            if (!activatePromo) {
                $("#" + responseMessageId).show();
                $("#" + responseMessageId).addClass('animated fadeInDown');
            } else {
                activatePromoNow(newPromoApp.Promotion.Id);
            }

        })
        .catch(function (error) {
            //todo handle errors
        });
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
