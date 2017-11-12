"use strict";

//Google API
//https://console.developers.google.com/apis/dashboard
//Places search example
//https://developers.google.com/places/web-service/search
//Place ID search example
//https://maps.googleapis.com/maps/api/place/details/json?placeid=ChIJN1t_tDeuEmsRUsoyG83frY4&key=YOUR_API_KEY

//Yelp API
//https://www.yelp.com/developers/documentation/v3
//Places example:
//https://api.yelp.com/v3/businesses/search?term=restaurant&location=boulder
//Header Name =     Authorization
//Value =           Bearer _TDZWcVlmco13_E6CKyo6agZp9_uT82FB45Y4L0_O-v98pAAMgqfa59LtXp1ySZgQKdUaEuona4E4-9mHDGsxFdMX9OeyVRBQXsurST3QPQ2GpAoPocap_NKFp7yWXYx

var analyser = new Vue({
    el: '#analyser',
    data: {
        BusinessSearchResults: [],
        PlacesIDs: [],
        PlacesResults: [],
        GoogleError: ""
    },
    created: function () {
        //get settings onLoad
        for (let item in localStorage) {
            let place = JSON.parse(localStorage.getItem(item));

            let placesItem = { "Name": place.Name, "Address": place.Address, "PhoneNumber": place.PhoneNumber, "Website": place.Website, "Rating": place.Rating};
            this.PlacesResults.push(placesItem)
        }        
    },
    methods: {

        inputsValid: function () {
            let valid = true;
            $("#cityError").hide();
            $("#stateError").hide();
            $("#businessTypeError").hide();

            if ($("#city").val().length === 0) {
                valid = false;
                $("#cityError").show();
            }
            if ($("#state").val().length === 0) {
                valid = false;
                $("#stateError").show();
            }
            if ($("#businessType").val().length === 0) {
                valid = false;
                $("#businessTypeError").show();
            }

            return valid;
        },

        lookupBusinesses: function () {

            if (!analyser.inputsValid()) { return; }

            let city = $("#city").val();
            let state = $("#state").val();
            let businessType = $("#businessType").val();

            axios.get('https://maps.googleapis.com/maps/api/place/textsearch/json?query=' + businessType + '+' + city + '+' + state + '&key=AIzaSyCj6LKEvnpOkMWIAPY5orojw4umlx247-Q')
                .then(function (response) {

                    analyser.GoogleError = "";
                    if (response.data.error_message) {
                        analyser.GoogleError = response.data.error_message;
                        $("#googleError").show();
                    }

                    analyser.BusinessSearchResults = [];
                    analyser.PlacesIDs = [];

                    analyser.BusinessSearchResults = response.data.results;
                    $.each(analyser.BusinessSearchResults, function (i, item) {
                        analyser.PlacesIDs.push(item.place_id);
                    });

                    analyser.getPlaceDetails();

                })
                .catch(function (error) {
                    //todo Handle errors
                });
        },

        getPlaceDetails: function () {

            analyser.PlacesResults = [];

            $.each(analyser.PlacesIDs, function (i, item) {
                axios.get('https://maps.googleapis.com/maps/api/place/details/json?placeid=' + item + '&key=AIzaSyCj6LKEvnpOkMWIAPY5orojw4umlx247-Q')
                    .then(function (response) {

                        let placeObject = [];
                        placeObject.Name = response.data.result.name;
                        placeObject.Address = response.data.result.formatted_address;
                        placeObject.PhoneNumber = response.data.result.formatted_phone_number;
                        placeObject.Website = response.data.result.website;
                        placeObject.Rating = response.data.result.rating;
                        placeObject.MapLocation = response.data.result.url;

                        analyser.PlacesResults.push(placeObject);
                    })
                    .catch(function (error) {
                        //todo Handle errors
                    });
            });
        },

        exportBusinesses: function () {

            let csvContent = "data:text/csv;charset=utf-8,";

            csvContent += "Name, ";
            csvContent += "Address, ";
            csvContent += "Phone Number, ";
            csvContent += "Website, ";
            csvContent += "Rating, ";
            csvContent += "Map Location \n";

            $.each(analyser.PlacesResults, function (i, item) {
                csvContent += item.Name + ", ";
                csvContent += item.Address + ", ";
                csvContent += item.PhoneNumber + ", ";
                csvContent += item.Website + ", ";
                csvContent += item.Rating + ", ";
                csvContent += item.MapLocation + "\n";

            });

            var encodedUri = encodeURI(csvContent);
            window.open(encodedUri);

        },

        saveProgress: function () {
            //save business data to local file

            localStorage.clear();

            let i = 0;
            $.each(analyser.PlacesResults, function (i, item) {
                let storageItem = { "Name": item.Name, "Address": item.Address, "PhoneNumber": item.PhoneNumber, "Website": item.Website, "Rating": item.Rating };

                localStorage.setItem(i, JSON.stringify(storageItem));
                i++;
            });           
        }

    }
});