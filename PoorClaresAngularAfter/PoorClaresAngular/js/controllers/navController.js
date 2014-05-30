"use strict";
var poorClaresApp;
(function (poorClaresApp) {
    (function (controllers) {
        var NavController = (function () {
            function NavController($scope, siteSectionService) {
                this.$scope = $scope;
                this.siteSectionService = siteSectionService;
                $scope.isCollapsed = true;
                $scope.siteSection = siteSectionService.getSiteSection();

                $scope.$watch(siteSectionService.getSiteSection, function (newValue, oldValue) {
                    $scope.siteSection = newValue;
                });
            }
            NavController.$inject = ["$scope", "siteSectionService"];
            return NavController;
        })();

        angular.module("poorClaresApp.controllers").controller("NavController", NavController);
    })(poorClaresApp.controllers || (poorClaresApp.controllers = {}));
    var controllers = poorClaresApp.controllers;
})(poorClaresApp || (poorClaresApp = {}));
//# sourceMappingURL=navController.js.map
