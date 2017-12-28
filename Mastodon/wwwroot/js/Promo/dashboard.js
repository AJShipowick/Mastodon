"use strict";

var dashboardApp = new Vue({
    el: '#dashboardApp',
    data: {
        Dashboard: []
    },
    mounted: function () {
        //get settings onLoad
        this.getUserSettings();

        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(this.drawChart);
    },
    methods: {
        editOldPromo: function (promoId) {
            window.location.href = '/Promotion/CreatePromo/CreateNewPromo?promoId=' + promoId;
        },

        drawChart: function () {
            // Define the chart to be drawn.
            let data = new google.visualization.DataTable();
            data.addColumn('string', 'Element');
            data.addColumn('number', 'Percentage');
            data.addRows([
                ['Nitrogen', 0.78],
                ['Oxygen', 0.21],
                ['Other', 0.01]
            ]);

            let options = {
                'title': 'How Much Pizza I Ate Last Night',
                'height': 350,
                'width': 500
            };

            // Instantiate and draw the chart.
            var chart = new google.visualization.PieChart(document.getElementById('myPieChart'));
            chart.draw(data, options);
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

        getUserSettings: function () {
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
        },

        showUserScriptInfo: function (userScript) {
            $("#userScriptModal").modal('show');
        }
    }
});

//$(function () {
//    google.charts.load('current', { packages: ['corechart'] });
//    google.charts.setOnLoadCallback(drawChart);
//});

//function drawChart() {
//    // Define the chart to be drawn.
//    let data = new google.visualization.DataTable();
//    data.addColumn('string', 'Element');
//    data.addColumn('number', 'Percentage');
//    data.addRows([
//        ['Nitrogen', 0.78],
//        ['Oxygen', 0.21],
//        ['Other', 0.01]
//    ]);

//    let options = {
//        'title': 'How Much Pizza I Ate Last Night',
//        'height': 350,
//        'width': 500
//    };

//    // Instantiate and draw the chart.
//    var chart = new google.visualization.PieChart(document.getElementById('myPieChart'));
//    chart.draw(data, options);
//}
