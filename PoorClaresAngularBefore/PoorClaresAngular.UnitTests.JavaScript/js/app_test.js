describe("Routes ->", function () {

    beforeEach(module("poorClaresApp"));

    describe("$routeProvider ->", function () {

        var $route, $location, $rootScope;

        beforeEach(function () {

            inject(function (_$route_, _$location_, _$rootScope_) {
                $route = _$route_;
                $location = _$location_;
                $rootScope = _$rootScope_;
            });
        });

        it("should map the root url to the home screen", function () {

            expect($route.routes["/"].controller).toBe("MainController");
            expect($route.routes["/"].templateUrl).toBe("partials/home.html");
        });

        it("should map urls that start '/theConvent/' to 'the Convent' site section", function () {

            expect($route.routes["/theConvent/:view"].controller).toBe("MainController");
            expect($route.routes["/theConvent/:view"].templateUrl({})).toBe("partials/theConvent/home.html");
        });

        it("should map urls to the main site section", function () {

            expect($route.routes["/:view"].controller).toBe("MainController");
            expect($route.routes["/:view"].templateUrl({})).toBe("partials/main/home.html");
        });

        it("should redirect other urls to the home screen", function () {

            expect($route.routes[null].redirectTo).toBe("/");
        });
    });
});
