"use strict";

//Places search
//https://developers.google.com/places/web-service/search
//Place ID search
//https://maps.googleapis.com/maps/api/place/details/json?placeid=ChIJN1t_tDeuEmsRUsoyG83frY4&key=YOUR_API_KEY
//https://maps.googleapis.com/maps/api/place/details/json?placeid=7686d1dc3c6989ea8e668123bd5021389c86c6ca&key=AIzaSyCj6LKEvnpOkMWIAPY5orojw4umlx247-Q


var analyser = new Vue({
    el: '#analyser',
    data: {
        BusinessSearchResults: [],
        PlacesIDs: [],
        PlacesResults: []
    },
    methods: {
        placeTest: function () {
            axios.get('https://maps.googleapis.com/maps/api/place/textsearch/json?query=plumber+Lincoln+NE&key=AIzaSyCj6LKEvnpOkMWIAPY5orojw4umlx247-Q')
                .then(function (response) {
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

            $.each(analyser.PlacesIDs, function (i, item) {
                axios.get('https://maps.googleapis.com/maps/api/place/details/json?placeid=' + item + '&key=AIzaSyCj6LKEvnpOkMWIAPY5orojw4umlx247-Q')
                    .then(function (response) {

                        let placeObject = [];
                        placeObject.BusinessName = response.data.result.name;
                        placeObject.Address = response.data.result.formatted_address;
                        placeObject.PhoneNumber = response.data.result.formatted_phone_number;
                        placeObject.Rating = response.data.result.rating;
                        placeObject.Website = response.data.result.website;

                        analyser.PlacesResults.push(placeObject);
                    })
                    .catch(function (error) {
                        //todo Handle errors
                    });
            });

            var x = "awesome!";

        }
    }
});