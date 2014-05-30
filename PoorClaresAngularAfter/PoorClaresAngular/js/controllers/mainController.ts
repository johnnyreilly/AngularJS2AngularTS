"use strict";

module poorClaresApp.controllers {
    class MainController {

        static $inject = ["$location", "siteSectionService"];
        constructor(
            private $location: ng.ILocationService,
            private siteSectionService: ISiteSectionService) {

            siteSectionService.determineSiteSection($location.path());
        }
    }

    angular.module("poorClaresApp.controllers").controller("MainController", MainController);
}
