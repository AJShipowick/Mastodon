"use strict";

var socialSharing = new Vue({
    el: '#socialSharing',
    data: {
        SocialModel: [],
        FacebookImages: [],
        TwitterImages: [],
        InstagramImages: [],
        LinkedinImages: [],
        PinterestImages: [],
        imageType: ""
    },
    mounted: function () {
        //get settings onLoad
        this.getSocialData();
    },
    methods: {
        getSocialData: function () {
            axios.get('/Promotion/CreatePromo/GetSocialData')
                .then(function (response) {
                    socialSharing.SocialModel = response.data;
                })
                .catch(function (error) {
                    //todo Handle errors
                });

        },

        getImagePaths: function (socialImageType, event) {
            axios.get('/Promotion/CreatePromo/GetSocialImages' + "?socialImageType=" + socialImageType)
                .then(function (response) {

                    if (!event.target.checked) {
                        return;
                    }

                    socialSharing.imageType = socialImageType;

                    switch (socialImageType) {
                        case "facebook":
                            socialSharing.FacebookImages = response.data;
                            break;
                        case "twitter":
                            socialSharing.TwitterImages = response.data;
                            break;
                        case "instagram":
                            socialSharing.InstagramImages = response.data;
                            break;
                        case "linkedin":
                            socialSharing.LinkedinImages = response.data;
                            break;
                        case "pinterest":
                            socialSharing.PinterestImages = response.data;
                            break;
                    }
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        getSocialImagePath: function (name) {
            return window.location.origin + "/images/Social/" + socialSharing.imageType + "/" + name;
        },

        saveSocialPromo: function () {
            if (this.validForm()) {
                this.saveSocial();
            }
        },

        validForm: function () {
            this.hideErrors();
            let validForm = true;

            if (!socialSharing.SocialModel.title) {
                $("#socialTitleMissing").show();
                validForm = false;
            }

            return validForm;
        },

        hideErrors: function () {
            $("#socialTitleMissing").hide();
        },

        saveSocial: function () {
            axios.post('/Promotion/CreatePromo/SaveSocial', socialSharing.SocialModel)
                .then(function (response) {
                    window.location.href = '/Dashboard';
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        }
    }
});