"use strict";

module poorClaresApp.controllers {
    interface INavControllerScope extends ng.IScope {
        isCollapsed: boolean;
        siteSection: string;
    }

    class NavController {

        static $inject = ["$scope", "siteSectionService"];
        constructor(
            private $scope: INavControllerScope,
            private siteSectionService: ISiteSectionService) {

            $scope.isCollapsed = true;
            $scope.siteSection = siteSectionService.getSiteSection();

            $scope.$watch(siteSectionService.getSiteSection, function (newValue, oldValue) {
                $scope.siteSection = newValue;
            });
        }
    }

    angular.module("poorClaresApp.controllers").controller("NavController", NavController);
}
