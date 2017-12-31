"use strict";

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

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        Promotion: []
    },
    mounted: function () {
        //get settings onLoad
        this.getPromotionModel();
    },
    methods: {
        getPromotionModel: function () {
            axios.get('/Promotion/CreatePromo/GetPromoModel')
                .then(function (response) {
                    newPromoApp.Promotion = response.data;

                    if (newPromoApp.Promotion.Id) {
                        $("#deletePromoBtn").show();
                        $("#slickContactForm").css({ "background-color": newPromoApp.Promotion.BackgroundColor });
                        $("#sliderButton").css({ "background-color": newPromoApp.Promotion.ButtonColor });
                        if (newPromoApp.Promotion.ShowCouponBorder) { $("#slickContactForm").css({ "border": "4px dashed #ccc" }); }
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
        },

        showCouponBorder: function () {
            if (!newPromoApp.Promotion.ShowCouponBorder) {
                $("#slickContactForm").css({ "border": "4px dashed #ccc" });
            } else {
                $("#slickContactForm").css({ "border": "1px solid #d8d8d8" });
            }
        },

        setFormBackgroundColor: function () {
            let selectedColor = $("#backgroundColor").val();
            $("#slickContactForm").css({ "background-color": selectedColor });
        },

        setFormButtonColor: function () {
            let selectedColor = $("#buttonColor").val();
            $("#sliderButton").css({ "background-color": selectedColor });
        },

        submitSlider: function () {
            $("#submitSliderClick").modal('show');
        },

        saveCustomSettings: function (activatePromo, responseMessageId) {
            $("#saveSuccessMsg").hide();
            $("#saveSuccessMsg").removeClass('animated fadeInDown');
            $("#activateSuccessMsg").hide();
            $("#activateSuccessMsg").removeClass('animated fadeInDown');

            //if (!newPromoApp.validForm()) { return; }

            axios.post('/Promotion/CreatePromo/SaveNewPromo',
                newPromoApp.Promotion
            )
                .then(function (response) {
                    if (!activatePromo) {
                        $("#" + responseMessageId).show();
                        $("#" + responseMessageId).addClass('animated fadeInDown');
                    } else {
                        newPromoApp.activatePromoNow("");
                    }
                })
                .catch(function (error) {
                    //todo handle errors
                });
        },

        validForm: function () {
            newPromoApp.hideErrors();
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
        },

        hideErrors: function () {
            $("#titleMissing").hide()
            $("#dateMissing").hide()
            $("#codeMissing").hide()
            $("#discountMissing").hide()
        },

        activatePromoNow: function (promoId) {
            axios.get('/Promotion/CreatePromo/ActivatePromo?promoId=' + promoId)
                .then(function (response) {
                    $("#activateSuccessMsg").show();
                    $("#activateSuccessMsg").addClass('animated fadeInDown');
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        deletePromo: function () {
            $("#deletePromoModal").modal('show');
        },

        confirmDeletePromo: function () {
            axios.get('/Promotion/CreatePromo/DeletePromo?promoId=' + newPromoApp.Promotion.Id)
                .then(function (response) {
                    window.location.href = '/Dashboard';
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        showHelpStep1: function () {
            $("#promoHelpModalStep1").modal('show');
        },

        showHelpStep2: function () {
            $("#promoHelpModalStep2").modal('show');
            $("#promoHelpModalStep1").modal('hide');
        },

        closeHelpStep2: function () {
            $("#promoHelpModalStep2").modal('hide');
        }
    }
});