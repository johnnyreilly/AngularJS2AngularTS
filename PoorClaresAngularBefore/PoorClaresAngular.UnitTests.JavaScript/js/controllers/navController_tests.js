describe("Controllers ->", function () {

    beforeEach(module("poorClaresApp"));

    describe("NavController ->", function () {

        var $scope, siteSectionService;

        beforeEach(inject(function ($rootScope, _$controller_) {

            $scope = $rootScope.$new();

            siteSectionService = {
                siteSection: "away",
                getSiteSection: function () { return siteSectionService.siteSection; }
            };

            _$controller_("NavController", {
                $scope: $scope,
                siteSectionService: siteSectionService
            });
        }));


        it("should set the default value of isCollapsed", function () {
            expect($scope.isCollapsed).toBe(true);
        });

        it("should set the default value of siteSection", function () {
            expect($scope.siteSection).toBe("away");
        });

        it("should watch for changes to the siteSection value", function () {

            var newSiteSection = "home";

            siteSectionService.siteSection = newSiteSection;

            $scope.$apply();

            expect($scope.siteSection).toBe(newSiteSection);
        });
    });
});
