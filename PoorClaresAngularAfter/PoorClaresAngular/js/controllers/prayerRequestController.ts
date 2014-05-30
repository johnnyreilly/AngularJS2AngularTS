"use strict";

module poorClaresApp.controllers {
    class PrayerRequestController {

        static $inject = ["$scope", "prayerRequestService"];
        constructor(
            private $scope: ng.IScope,
            private prayerRequestService: IPrayerRequestService) {
        }

        message: { success: boolean; text: string };

        send(prayerRequest: { email: string; prayFor: string }) {

            this.message = {
                success: true,
                text: "Sending..."
            };

            this.prayerRequestService.sendPrayerRequest(prayerRequest.email, prayerRequest.prayFor)
                .then((response) => {
                    this.message = {
                        success: response.success,
                        text: response.text
                    };
                })
                .then(null, (error) => { // IE 8 friendly alias for catch
                    this.message = {
                        success: false,
                        text: "Sorry your email was not sent"
                    };
                });
        }
    }

    angular
        .module("poorClaresApp.controllers")
        .controller("PrayerRequestController", PrayerRequestController);
}
