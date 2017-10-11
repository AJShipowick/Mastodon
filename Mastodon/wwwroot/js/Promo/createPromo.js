"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        Promotion: []
    },
    created: function () {
        //get settings onLoad
        getNewPromotionModel()
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

function getNewPromotionModel() {
    axios.get('/Promotion/CreatePromo/GetNewPromoModel')
        .then(function (response) {
            newPromoApp.Promotion = response.data;
        })
        .catch(function (error) {
            //todo handle errors
        });
}

function saveCustomSettings(activatePromo) {
    $("#saveSuccessMsg").hide();
    $("#saveSuccessMsg").removeClass('animated fadeInDown');

    axios.post('/Promotion/CreatePromo/SaveNewPromo',
        newPromoApp.Promotion
    )
        .then(function (response) {
            $("#saveSuccessMsg").show();
            $("#saveSuccessMsg").addClass('animated fadeInDown');
        })
        .catch(function (error) {
            //todo handle errors
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
