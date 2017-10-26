"use strict";

var promoDetailsApp = new Vue({
    el: '#promoDetailsApp',
    data: {
        PromoDetails: []
    },
    created: function () {
        //get settings onLoad
        axios.get('/Details/PromoDetails/GetPromoDetails')
            .then(function (response) {
                promoDetailsApp.PromoDetails = response.data;
                $('#ajaxLoading').hide();
                $('#promoDetailsApp').show();
            })
            .catch(function (error) {
                //todo Handle errors
            });
    },
    methods: {
        exportEntries: function (promoId) {
            //export spinner??
            var url = "/Details/PromoDetails/ExportPromoUsers?promoId=" + promoId;
            window.location = url;
        }
    }
});