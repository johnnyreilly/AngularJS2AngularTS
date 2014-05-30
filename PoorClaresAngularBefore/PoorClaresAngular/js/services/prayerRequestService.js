"use strict";

angular.module("poorClaresApp.services").factory(

    "prayerRequestService",

    ["$http",
    function ($http) {

        var url = "/PrayerRequest";

        function sendPrayerRequest(email, prayFor) {

            var params = { email: email, prayFor: prayFor };

            return $http.post(url, params)
                .then(function (response) {
                    return {
                        success: response.data.success,
                        text: response.data.text
                    };
                });
        }

        return {
            sendPrayerRequest: sendPrayerRequest
        };
    }]);
