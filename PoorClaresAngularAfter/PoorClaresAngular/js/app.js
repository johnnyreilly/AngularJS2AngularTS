"use strict";
// Declare controllers / services modules
angular.module("poorClaresApp.controllers", []);
angular.module("poorClaresApp.services", []);

// Declare app
angular.module("poorClaresApp", [
    "ngRoute",
    "ngAnimate",
    "ui.bootstrap",
    "poorClaresApp.controllers",
    "poorClaresApp.services"
]).config([
    "$routeProvider",
    function ($routeProvider) {
        function getTheConventTemplateUrl(params) {
            var view = params.view || "home";
            return "partials/theConvent/" + view + ".html";
        }

        function getMainTemplateUrl(params) {
            var view = params.view || "home";
            return "partials/main/" + view + ".html";
        }

        $routeProvider.when("/", {
            templateUrl: "partials/home.html",
            controller: "MainController"
        }).when("/theConvent/:view", {
            templateUrl: getTheConventTemplateUrl,
            controller: "MainController",
            caseInsensitiveMatch: true
        }).when("/:view", {
            templateUrl: getMainTemplateUrl,
            controller: "MainController",
            caseInsensitiveMatch: true
        }).otherwise({
            redirectTo: "/"
        });
    }]);
//# sourceMappingURL=app.js.map
