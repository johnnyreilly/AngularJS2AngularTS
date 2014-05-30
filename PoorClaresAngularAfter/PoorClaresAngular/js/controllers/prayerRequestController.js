"use strict";
var poorClaresApp;
(function (poorClaresApp) {
    (function (controllers) {
        var PrayerRequestController = (function () {
            function PrayerRequestController($scope, prayerRequestService) {
                this.$scope = $scope;
                this.prayerRequestService = prayerRequestService;
            }
            PrayerRequestController.prototype.send = function (prayerRequest) {
                var _this = this;
                this.message = {
                    success: true,
                    text: "Sending..."
                };

                this.prayerRequestService.sendPrayerRequest(prayerRequest.email, prayerRequest.prayFor).then(function (response) {
                    _this.message = {
                        success: response.success,
                        text: response.text
                    };
                }).then(null, function (error) {
                    _this.message = {
                        success: false,
                        text: "Sorry your email was not sent"
                    };
                });
            };
            PrayerRequestController.$inject = ["$scope", "prayerRequestService"];
            return PrayerRequestController;
        })();

        angular.module("poorClaresApp.controllers").controller("PrayerRequestController", PrayerRequestController);
    })(poorClaresApp.controllers || (poorClaresApp.controllers = {}));
    var controllers = poorClaresApp.controllers;
})(poorClaresApp || (poorClaresApp = {}));
//# sourceMappingURL=prayerRequestController.js.map
