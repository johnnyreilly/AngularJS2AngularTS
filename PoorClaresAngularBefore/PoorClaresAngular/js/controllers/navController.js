"use strict";

angular.module("poorClaresApp.controllers").controller(

    "NavController",

    ["$scope", "siteSectionService",
    function ($scope, siteSectionService) {

        $scope.isCollapsed = true;
        $scope.siteSection = siteSectionService.getSiteSection();

        $scope.$watch(siteSectionService.getSiteSection, function (newValue, oldValue) {
            $scope.siteSection = newValue;
        });
    }]);
