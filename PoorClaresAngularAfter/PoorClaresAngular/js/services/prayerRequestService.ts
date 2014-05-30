"use strict";

interface IPrayerRequestService {
    sendPrayerRequest: (email: string, prayFor: string) => ng.IPromise<{
        success: boolean;
        text: string;
    }>;
}

angular.module("poorClaresApp.services").factory(

    "prayerRequestService",

    ["$http",
    function ($http: ng.IHttpService): IPrayerRequestService {

        var url = "/PrayerRequest";

        function sendPrayerRequest(email: string, prayFor: string) {

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
