"use strict";

var dashboardApp = new Vue({
    el: '#dashboardApp',
    data: {
        Dashboard: []
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

        editOldPromo: function (promoId, isActivePromo) {
            window.location.href = '/Promotion/CreatePromo/CreateNewPromo?promoId=' + promoId;
        },

        drawDashboardPieChart: function () {

            if (dashboardApp.Dashboard.ActivePromoViews === 0) { return;}

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

        activatePromoNow: function (promoId) {
            $("#activePromoData").hide();
            $("#inactivePromoData").hide();
            $('#ajaxLoading').show();

            axios.get('/Dashboard/Dashboard/ActivatePromo?promoId=' + promoId)
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

        stopPromoNow: function (promoId) {
            $("#activePromoData").hide();
            $("#inactivePromoData").hide();
            $('#ajaxLoading').show();
            $("#stopPromoModal").modal('hide');

            axios.get('/Dashboard/Dashboard/StopActivePromotion?promoId=' + promoId)
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

        showUserScriptInfo: function (userScript) {
            $("#userScriptModal").modal('show');
        }
    }
});