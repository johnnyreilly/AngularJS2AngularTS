describe("Controllers ->", function () {

    beforeEach(module("poorClaresApp"));

    describe("MainController ->", function () {

        var $location, siteSectionService;

        function setUp(path) {
            inject(function (_$controller_, _$location_, _siteSectionService_) {

                $location = _$location_;
                siteSectionService = _siteSectionService_;

                spyOn($location, "path").and.returnValue(path);
                spyOn(siteSectionService, "determineSiteSection").and.callThrough();

                _$controller_("MainController", {
                    $location: $location,
                    siteSectionService: siteSectionService
                });
            })
        }

        it("should call siteSectionService.determineSiteSection with 'theConvent'", function () {

            setUp("/theConvent/");
            expect(siteSectionService.determineSiteSection).toHaveBeenCalledWith("/theConvent/");
        });
    });
});
