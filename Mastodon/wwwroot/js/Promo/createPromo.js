"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        PromoModel: [],
        PromoImages: [],
        currentImageID: '',
        imageType: ''
    },
    mounted: function () {
        //get settings onLoad
        this.getPromoData();
    },
    methods: {
        getPromoData: function () {
            axios.get('/Promotion/CreatePromo/GetPromoData')
                .then(function (response) {
                    newPromoApp.PromoModel = response.data;

                    if (newPromoApp.PromoModel.id) {
                        $("#deletePromoBtn").show();
                        $("#slickContactForm").css({ "background-color": newPromoApp.PromoModel.backgroundColor });
                        $("#sliderButton").css({ "background-color": newPromoApp.PromoModel.buttonColor });
                        if (newPromoApp.PromoModel.showCouponBorder) { $("#slickContactForm").css({ "border": "4px dashed #ccc" }); }

                        //get slider image, then set it....
                        newPromoApp.imageType = newPromoApp.PromoModel.imageType;
                        $("#slickImage").attr("src", newPromoApp.getImagePath(newPromoApp.PromoModel.imageName));
                    } else {
                        //New promo, no slider selected, select 1st image for user and colors
                        newPromoApp.PromoModel.imageName = "promo1";
                        newPromoApp.PromoModel.backgroundColor = "#ffffff";
                        newPromoApp.PromoModel.buttonColor = "#4CAF50";
                    }

                    showCustomSliderImage();
                    closeSlickSlider();

                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },
        saveCustomPromo: function () {
            if (this.validForm()) {
                this.savePromo();
            }
        },
        savePromo: function () {
            axios.post('/Promotion/CreatePromo/SaveNewPromo', newPromoApp.PromoModel)
                .then(function (response) {
                    window.location.href = '/Dashboard';
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },
        getPromoImages: function (imageType) {
            newPromoApp.PromoImages = [];
            $("#ajaxLoading").show();

            axios.get('/Promotion/CreatePromo/GetPromoImages' + '?imageType=' + imageType)
                .then(function (response) {
                    newPromoApp.imageType = imageType;
                    newPromoApp.PromoImages = response.data;
                    $("#ajaxLoading").hide();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },
        getImagePath: function (name) {
            return window.location.origin + "/images/Slider/" + newPromoApp.imageType + "/" + name;
        },

        setPromoImage: function (name, id) {

            if (newPromoApp.currentImageID == "") {
                newPromoApp.currentImageID = id;
            } else {
                $("#" + newPromoApp.currentImageID).removeClass('promo-border');
                newPromoApp.currentImageID = id;
            }

            $("#" + id).addClass('promo-border');
            newPromoApp.PromoModel.imageName = name;
            newPromoApp.PromoModel.imageType = newPromoApp.imageType;

            $("#slickImage").attr("src", newPromoApp.getImagePath(name));
        },

        showCouponBorder: function () {
            if (newPromoApp.PromoModel.showCouponBorder) {
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

            if (!newPromoApp.PromoModel.title) {
                $("#titleMissing").show();
                validForm = false;
            }

            if (!newPromoApp.PromoModel.endDate) {
                $("#dateMissing").show();
                validForm = false;
            }

            if (!newPromoApp.PromoModel.code) {
                $("#codeMissing").show();
                validForm = false;
            }

            if (!newPromoApp.PromoModel.discount) {
                $("#discountMissing").show();
                validForm = false;
            }

            return validForm;
        },
        hideErrors: function () {
            $("#titleMissing").hide();
            $("#dateMissing").hide();
            $("#codeMissing").hide();
            $("#discountMissing").hide();
        },
        deletePromo: function () {
            $("#deletePromoModal").modal('show');
        },
        confirmDeletePromo: function () {
            axios.get('/Promotion/CreatePromo/DeletePromo?promoId=' + newPromoApp.PromoModel.id)
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