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

        facebook: 'facebook',
        twitter: 'twitter',
        instagram: 'instagram',
        linkedin: 'linkedin',
        pinterest: 'pinterest',

        currentFacebookImageId: '',
        currentTwitterImageId: '',
        currentInstagramImageId: '',
        currentLinkedinImageId: '',
        currentPinterestImageId: '',
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

                    $('#ajaxLoading').hide();
                    $("#promoData").show();

                    socialSharing.buildSavedSocialButtons();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        buildSavedSocialButtons: function () {
            $('#socialContainerId').css(socialSharing.SocialModel.sideOfScreen, 0);

            if (socialSharing.SocialModel.useFacebook) {
                socialSharing.getSocialImages(socialSharing.facebook);

                let fbPath = socialSharing.getSocialImagePath(socialSharing.facebook, socialSharing.SocialModel.facebookImageName);
                $('#facebookImage').attr("src", fbPath);
                $('#facebookImage').css("display", "block");
            }
            if (socialSharing.SocialModel.useTwitter) {
                socialSharing.getSocialImages(socialSharing.twitter);

                let twPath = socialSharing.getSocialImagePath(socialSharing.twitter, socialSharing.SocialModel.twitterImageName);
                $('#twitterImage').attr("src", twPath);
                $('#twitterImage').css("display", "block");
            }
            if (socialSharing.SocialModel.useInstagram) {
                socialSharing.getSocialImages(socialSharing.instagram);

                let insPath = socialSharing.getSocialImagePath(socialSharing.instagram, socialSharing.SocialModel.instagramImageName);
                $('#instagramImage').attr("src", insPath);
                $('#instagramImage').css("display", "block");
            }
            if (socialSharing.SocialModel.useLinkedin) {
                socialSharing.getSocialImages(socialSharing.linkedin);

                let linPath = socialSharing.getSocialImagePath(socialSharing.linkedin, socialSharing.SocialModel.linkedinImageName);
                $('#linkedinImage').attr("src", linPath);
                $('#linkedinImage').css("display", "block");
            }
            if (socialSharing.SocialModel.usePinterest) {
                socialSharing.getSocialImages(socialSharing.pinterest);

                let pinPath = socialSharing.getSocialImagePath(socialSharing.pinterest, socialSharing.SocialModel.pinterestImageName);
                $('#pinterestImage').attr("src", pinPath);
                $('#pinterestImage').css("display", "block");
            }
        },

        displaySocialImages: function (socialImageType, event) {
            if (event && event.target && event.target.checked) {
                socialSharing.getSocialImages(socialImageType);
            } else {
                //Un-check event, remove image from screen
                switch (socialImageType) {
                    case socialSharing.facebook:
                        $('#facebookImage').hide();
                        break;
                    case socialSharing.twitter:
                        $('#twitterImage').hide();
                        break;
                    case socialSharing.instagram:
                        $('#instagramImage').hide();
                        break;
                    case socialSharing.linkedin:
                        $('#linkedinImage').hide();
                        break;
                    case socialSharing.pinterest:
                        $('#pinterestImage').hide();
                        break;
                }
            }
        },

        getSocialImages: function (socialImageType) {
            axios.get('/Promotion/CreatePromo/GetSocialImages' + "?socialImageType=" + socialImageType)
                .then(function (response) {

                    socialSharing.imageType = socialImageType;

                    switch (socialImageType) {
                        case socialSharing.facebook:
                            socialSharing.FacebookImages = response.data;
                            break;
                        case socialSharing.twitter:
                            socialSharing.TwitterImages = response.data;
                            break;
                        case socialSharing.instagram:
                            socialSharing.InstagramImages = response.data;
                            break;
                        case socialSharing.linkedin:
                            socialSharing.LinkedinImages = response.data;
                            break;
                        case socialSharing.pinterest:
                            socialSharing.PinterestImages = response.data;
                            break;
                    }
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        getSocialImagePath: function (imageType, name) {
            return window.location.origin + "/images/Social/" + imageType + "/" + name;
        },

        setSocialImage: function (name, id, socialImageType) {
            switch (socialImageType) {
                case socialSharing.facebook:
                    if (socialSharing.currentFacebookImageId) {
                        $("#" + socialSharing.currentFacebookImageId).removeClass('promo-border');
                    }
                    $("#" + id).addClass('promo-border');
                    socialSharing.currentFacebookImageId = id;
                    socialSharing.SocialModel.facebookImageName = name;

                    $('#facebookImage').attr("src", $("#" + id)[0].currentSrc);
                    $('#facebookImage').css("display", "block");
                    break;
                case socialSharing.twitter:
                    if (socialSharing.currentTwitterImageId) {
                        $("#" + socialSharing.currentTwitterImageId).removeClass('promo-border');
                    }
                    $("#" + id).addClass('promo-border');
                    socialSharing.currentTwitterImageId = id;
                    socialSharing.SocialModel.twitterImageName = name;

                    $('#twitterImage').attr("src", $("#" + id)[0].currentSrc);
                    $('#twitterImage').css("display", "block");
                    break;
                case socialSharing.instagram:
                    if (socialSharing.currentInstagramImageId) {
                        $("#" + socialSharing.currentInstagramImageId).removeClass('promo-border');
                    }
                    $("#" + id).addClass('promo-border');
                    socialSharing.currentInstagramImageId = id;
                    socialSharing.SocialModel.instagramImageName = name;

                    $('#instagramImage').attr("src", $("#" + id)[0].currentSrc);
                    $('#instagramImage').css("display", "block");
                    break;
                case socialSharing.linkedin:
                    if (socialSharing.currentLinkedinImageId) {
                        $("#" + socialSharing.currentLinkedinImageId).removeClass('promo-border');
                    }
                    $("#" + id).addClass('promo-border');
                    socialSharing.currentLinkedinImageId = id;
                    socialSharing.SocialModel.linkedinImageName = name;

                    $('#linkedinImage').attr("src", $("#" + id)[0].currentSrc);
                    $('#linkedinImage').css("display", "block");
                    break;
                case socialSharing.pinterest:
                    if (socialSharing.currentPinterestImageId) {
                        $("#" + socialSharing.currentPinterestImageId).removeClass('promo-border');
                    }
                    $("#" + id).addClass('promo-border');
                    socialSharing.currentPinterestImageId = id;
                    socialSharing.SocialModel.pinterestImageName = name;

                    $('#pinterestImage').attr("src", $("#" + id)[0].currentSrc);
                    $('#pinterestImage').css("display", "block");
                    break;
            }
        },

        changeSideOfScreen: function () {
            if (socialSharing.SocialModel.sideOfScreen === "left") {
                $('#socialContainerId').css('right', '');
                $('#socialContainerId').css('left', 0);
            } else {
                $('#socialContainerId').css('left', '');
                $('#socialContainerId').css('right', 0);
            }
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