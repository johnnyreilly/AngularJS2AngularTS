describe("Services ->", function () {

    beforeEach(module("poorClaresApp"));

    describe("prayerRequestService ->", function () {

        var $httpBackend, prayerRequestService;

        beforeEach(function () {

            inject(function (_$httpBackend_, _prayerRequestService_) {
                $httpBackend = _$httpBackend_;
                prayerRequestService = _prayerRequestService_;
            });
        });

        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });

        it("should send http post", function () {

            var stubResponse = { success: true, text: "Done!" };

            $httpBackend
                .expectPOST("/PrayerRequest", { email: "johnny_reilly@hotmail.com", prayFor: "Me" })
                .respond(200, stubResponse);

            // make the call
            var promise = prayerRequestService.sendPrayerRequest("johnny_reilly@hotmail.com", "Me");

            // set up to collect the result
            var result;
            promise.then(function (response) {
                result = response;
            })

            // execute the request to do the expectPOST (which will populate result)
            $httpBackend.flush();

            expect(result.success).toBe(stubResponse.success);
            expect(result.text).toBe(stubResponse.text);
        });
    });
});
