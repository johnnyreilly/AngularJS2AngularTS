"use strict";

angular.module("poorClaresApp.controllers").controller(

    "MainController",

    ["$location", "siteSectionService",
    function ($location, siteSectionService) {

        siteSectionService.determineSiteSection($location.path());
    }]);
