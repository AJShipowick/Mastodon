"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        Promotion: []
    },
    mounted: function () {
        //get settings onLoad
        if ($("#promoId").val()) {
            $("#deletePromoBtn").show();
        }
    },
    methods: {
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

        validForm: function (e) {
            newPromoApp.hideErrors();
            let validForm = true;

            if (!$("#title").val()) {
                $("#titleMissing").show()
                validForm = false;
            }

            if (!$("#endDate").val()) {
                $("#dateMissing").show()
                validForm = false;
            }

            if (!$("#code").val()) {
                $("#codeMissing").show()
                validForm = false;
            }

            if (!$("#discount").val()) {
                $("#discountMissing").show()
                validForm = false;
            }

            if (!validForm) { e.preventDefault(); }  //Prevent form submission if form not valid

            return validForm;
        },

        hideErrors: function () {
            $("#titleMissing").hide()
            $("#dateMissing").hide()
            $("#codeMissing").hide()
            $("#discountMissing").hide()
        },

        deletePromo: function () {
            $("#deletePromoModal").modal('show');
        },

        confirmDeletePromo: function () {
            axios.get('/Promotion/CreatePromo/DeletePromo?promoId=' + $("#promoId").val())
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