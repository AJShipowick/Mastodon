"use strict";

var dashboardApp = new Vue({
    el: '#dashboardApp',
    data: {
        Dashboard: []
    },
    created: function () {
        //get settings onLoad
        getUserSettings()
    },
    methods: {
        editOldPromo: function (promoId) {
            window.location.href = '/Promotion/CreatePromo/CreateNewPromo?promoId=' + promoId;
        },
        confirmStopActivePromo: function () {
            $("#stopPromoModal").modal('show');
        },
        stopPromoNow: function (promoId) {
            $("#activePromoData").hide();
            $("#inactivePromoData").hide();
            $('#ajaxLoading').show();
            $("#stopPromoModal").modal('hide');

            axios.get('/Dashboard/Dashboard/StopActivePromotion?promoId=' + promoId)
                .then(function (response) {
                    getUserSettings();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },
        viewPromoEntries: function (promoId, promoName) {
            window.location.href = '/Details/PromoDetails/PromoDetails?promoId=' + promoId + "&promoName=" + promoName;
        }
    }
});

function getUserSettings() {
    axios.get('/Dashboard/Dashboard/GetUserSettings')
        .then(function (response) {
            dashboardApp.Dashboard = response.data;
            $("#activePromoData").show();
            $("#inactivePromoData").show();
            $('#ajaxLoading').hide();
        })
        .catch(function (error) {
            //todo Handle errors
        });
}