"use strict";

angular.module("poorClaresApp.controllers").controller(

    "PrayerRequestController",

    ["$scope", "prayerRequestService",
    function ($scope, prayerRequestService) {
        
        var vm = this;

        vm.send = function (prayerRequest) {

            vm.message = {
                success: true,
                text: "Sending..."
            };

            prayerRequestService.sendPrayerRequest(prayerRequest.email, prayerRequest.prayFor)
                .then(function (response) {
                    vm.message = {
                        success: response.success,
                        text: response.text
                    };
                })
                .then(null, function (error) { // IE 8 friendly alias for catch
                    vm.message = {
                        success: false,
                        text: "Sorry your email was not sent"
                    };
                });
        }
    }]);
