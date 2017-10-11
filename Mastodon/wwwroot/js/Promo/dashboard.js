var dashboardApp = new Vue({
    el: '#dashboardApp',
    data: {
        Dashboard: []
    },
    created: function () {
        //get settings onLoad
        getUserSettings()
    }
})

function getUserSettings() {
    axios.get('/Dashboard/Dashboard/GetUserSettings')
        .then(function (response) {
            dashboardApp.Dashboard = response.data;
            $("#activePromoData").show();
            $("#pastPromoData").show();
            $('#ajaxLoading').hide();
        })
        .catch(function (error) {
            //todo Handle errors
        });
}