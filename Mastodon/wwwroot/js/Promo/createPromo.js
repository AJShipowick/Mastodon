"use strict";

var newPromoApp = new Vue({
    el: '#newPromoApp',
    data: {
        PromoModel: [],
        PromoImages: [],
        UserImages: [],
        currentImageID: '',
        imageType: '',
        FilesToUpload: [],
        fileError: ''
    },
    mounted: function () {
        //get settings onLoad
        this.getPromoData();
        this.getUserImages();
    },
    methods: {
        getPromoData: function () {
            axios.get('/Promotion/CreatePromo/GetPromoData')
                .then(function (response) {
                    newPromoApp.PromoModel = response.data;

                    $("#promoData").show();
                    $('#ajaxLoading').hide();

                    if (newPromoApp.PromoModel.id) {
                        $("#osoContactForm").css({ "background-color": newPromoApp.PromoModel.backgroundColor });
                        $("#osoButton").css({ "background-color": newPromoApp.PromoModel.buttonColor });
                        if (newPromoApp.PromoModel.showCouponBorder) {
                            $("#osoContactForm").css({ "border": "4px dashed #ccc" });
                        }
                        if (newPromoApp.PromoModel.showLargeImage) {
                            $("#osoImage").css({ "width": "96px" })
                        }

                        //get promo image, then set it....
                        newPromoApp.imageType = newPromoApp.PromoModel.imageType;
                    } else {
                        //New promo, no promo selected, set some defaults
                        newPromoApp.imageType = "coupon";
                    }

                    $("#osoImage").attr("src", newPromoApp.getDefaultImagePath(newPromoApp.PromoModel.imageName));

                    newPromoApp.setUserPromoProperties();
                    showCustomPromoImage();
                    closePromotion();

                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        setUserPromoProperties: function (sideOfScreen) {
            if (newPromoApp.PromoModel.sideOfScreen === "left") {
                $('#osoContactForm').css({ 'float': 'left' });
                //Remove right properties
                $('#osoImage').css({ 'right': '' });
                $('#osoContainer').css({ 'right': '' });
            } else {
                $('#osoContactForm').css({ 'float': 'right' });
                //Remove left properties
                $('#osoImage').css({ 'left': '' });
                $('#osoContainer').css({ 'left': '' });
            }

            closePromotion();
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
            $("#imagesLoading").show();

            axios.get('/Promotion/CreatePromo/GetPromoImages' + '?imageType=' + imageType)
                .then(function (response) {
                    newPromoApp.imageType = imageType;
                    newPromoApp.PromoImages = response.data;
                    $("#imagesLoading").hide();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        getUserImages: function () {
            axios.get('/Promotion/CreatePromo/GetUserImages')
                .then(function (response) {
                    newPromoApp.UserImages = response.data;
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        getDefaultImagePath: function (name) {
            return window.location.origin + "/images/Promo/" + newPromoApp.imageType + "/" + name;
        },

        getUserImagePath: function (name) {
            return window.location.origin + "/images/Promo/Users/" + name;
        },

        setPromoImage: function (name, id, userImage) {

            if (newPromoApp.currentImageID === "") {
                newPromoApp.currentImageID = id;
            } else {
                $("#" + newPromoApp.currentImageID).removeClass('promo-border');
                newPromoApp.currentImageID = id;
            }

            $("#" + id).addClass('promo-border');
            newPromoApp.PromoModel.imageName = name;

            if (userImage) {
                newPromoApp.PromoModel.imageType = "Users";
                $("#osoImage").attr("src", newPromoApp.getUserImagePath(name));
            } else {
                newPromoApp.PromoModel.imageType = newPromoApp.imageType;
                $("#osoImage").attr("src", newPromoApp.getDefaultImagePath(name));
            }
        },

        showCouponBorder: function () {
            if (newPromoApp.PromoModel.showCouponBorder === true) {
                $("#osoContactForm").css({ "border": "4px dashed #ccc" });
            } else {
                $("#osoContactForm").css({ "border": "1px solid #d8d8d8" });
            }
        },

        showLargeImage: function () {
            if (newPromoApp.PromoModel.showLargeImage === true) {
                $("#osoImage").css({ "width": "96px" });
            } else {
                $("#osoImage").css({ "width": "64px" });
            }
        },

        setFormBackgroundColor: function () {
            let selectedColor = $("#backgroundColor").val();
            $("#osoContactForm").css({ "background-color": selectedColor });
        },

        setFormButtonColor: function () {
            let selectedColor = $("#buttonColor").val();
            $("#osoButton").css({ "background-color": selectedColor });
        },

        submitSlider: function () {
            $("#submitSliderClick").modal('show');
        },

        validForm: function () {

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

            //if (!newPromoApp.PromoModel.discount) {
            //    $("#discountMissing").show();
            //    validForm = false;
            //}

            if (!newPromoApp.PromoModel.thankYouMessage) {
                $('#thankYouMissing').show();
                validForm = false;
            }

            return validForm;
        },

        hideErrors: function () {
            $("#titleMissing").hide();
            $("#dateMissing").hide();
            $("#codeMissing").hide();
            //$("#discountMissing").hide();
            $('#thankYouMissing').hide();
        },

        //deletePromo: function () {
        //    $("#deletePromoModal").modal('show');
        //},

        //confirmDeletePromo: function () {
        //    axios.get('/Promotion/CreatePromo/DeletePromo?promoId=' + newPromoApp.PromoModel.id)
        //        .then(function (response) {
        //            window.location.href = '/Dashboard';
        //        })
        //        .catch(function (error) {
        //            //todo Handle errors
        //        });
        //},

        fileChange: function (fileList) {
            newPromoApp.FilesToUpload = [];
            newPromoApp.fileError = '';

            let ValidImageTypes = ["image/gif", "image/jpeg", "image/png", "image/svg+xml"];
            for (let i = 0; i < fileList.length; i++) {
                newPromoApp.FilesToUpload.push(fileList[i].name);
                if (fileList[i].size > 1000000) {  //1MB, 1,000,000KB
                    newPromoApp.fileError = 'File size too big, max size 1MB';
                }
                if ($.inArray(fileList[i].type, ValidImageTypes) < 0) {
                    newPromoApp.fileError = 'Image must be (.gif, .jpeg, .png or .svg).';
                }

            }
        },

        formatDate: function () {
            let splitDate = newPromoApp.PromoModel.endDate.split("-");
            newPromoApp.PromoModel.displayEndDate = splitDate[1] + "/" + splitDate[2] + "/" + splitDate[0];
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
        },

        showFormSubmitHelp: function () {
            $("#formSubmitHelpModal").modal('show');
        }
    }
});