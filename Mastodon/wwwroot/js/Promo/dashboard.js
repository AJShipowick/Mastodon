"use strict";

var dashboardApp = new Vue({
    el: '#dashboardApp',
    data: {
        Dashboard: [],
        InactivePromoCount: 0
    },
    created: function () {
        //get settings onLoad
        this.getUserSettings();
    },
    methods: {
        getUserSettings: function () {
            axios.get('/Dashboard/Dashboard/GetUserSettings')
                .then(function (response) {
                    dashboardApp.Dashboard = response.data;
                    dashboardApp.InactivePromoCount = response.data.InactivePromos ? response.data.InactivePromos.length : 0;

                    $("#activePromoData").show();
                    $("#inactivePromoData").show();
                    $('#ajaxLoading').hide();

                    google.charts.load('current', { packages: ['corechart'] });
                    google.charts.setOnLoadCallback(dashboardApp.drawDashboardPieChart);
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        editOldPromo: function (promoId, promoType) {
            window.location.href = '/Promotion/CreatePromo/CreateNewPromo?promoId=' + promoId + "&promoType=" + promoType;
        },

        drawDashboardPieChart: function () {

            if (dashboardApp.Dashboard.ActivePromoViews === 0) { return; }

            // Define the chart to be drawn.
            let data = new google.visualization.DataTable();
            data.addColumn('string', 'Element');
            data.addColumn('number', 'Percentage');
            data.addRows([
                ['Claimed Entries', dashboardApp.Dashboard.ActivePromoClaimedEntries],
                ['Total Views', dashboardApp.Dashboard.ActivePromoViews]
            ]);

            let options = {
                height: 350,
                width: 500,
                is3D: true,
                backgroundColor: '#f7f8fa',
                fontSize: 15
            };

            // Instantiate and draw the chart.
            var chart = new google.visualization.PieChart(document.getElementById('pieChartStats'));
            chart.draw(data, options);
        },

        activatePromoNow: function (promoId, promoTypeToActivate, promoTypeToStop) {
            $("#activePromoData").hide();
            $("#inactivePromoData").hide();
            $('#ajaxLoading').show();

            axios.get('/Dashboard/Dashboard/ActivatePromo?promoId=' + promoId + "&promoTypeToActivate=" + promoTypeToActivate + "&promoTypeToStop=" + promoTypeToStop)
                .then(function (response) {
                    dashboardApp.getUserSettings();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        confirmStopActivePromo: function () {
            $("#stopPromoModal").modal('show');
        },

        stopPromoNow: function (promoId, promoType) {
            $("#activePromoData").hide();
            $("#inactivePromoData").hide();
            $('#ajaxLoading').show();
            $("#stopPromoModal").modal('hide');

            axios.get('/Dashboard/Dashboard/StopActivePromotion?promoId=' + promoId + "&promoType=" + promoType)
                .then(function (response) {
                    dashboardApp.getUserSettings();
                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        viewPromoEntries: function (promoId, promoName) {
            window.location.href = '/Details/PromoDetails/PromoDetails?promoId=' + promoId + "&promoName=" + promoName;
        },

        showUserScriptInfo: function () {
            $("#userScriptModal").modal('show');
            $('#copySuccess').hide();
        },

        copyToClipboard: function (element) {
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val($(element).text()).select();
            document.execCommand("copy");
            $temp.remove();

            $('#copySuccess').show();
        }
    }
});