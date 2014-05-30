describe("Controllers ->", function () {

    beforeEach(module("poorClaresApp"));

    describe("PrayerRequestController ->", function () {

        var prayerRequestController, prayerRequestService, $scope, deferred, prayerRequest;

        beforeEach(inject(function (_$rootScope_, $q, _$controller_, _prayerRequestService_) {

            $rootScope = _$rootScope_;

            $scope = $rootScope.$new();

            prayerRequestService = _prayerRequestService_;

            deferred = $q.defer();

            prayerRequestController = _$controller_("PrayerRequestController", {
                $scope: $scope,
                prayerRequestService: prayerRequestService
            });

            spyOn(prayerRequestService, "sendPrayerRequest").and.returnValue(deferred.promise);

            prayerRequest = { email: "johnny_reilly@hotmail.com", prayFor: "Me" };
        }));

        it("should set the message to 'Sending...'", function () {

            prayerRequestController.send(prayerRequest);

            var message = prayerRequestController.message;
            expect(message.success).toBe(true);
            expect(message.text).toBe("Sending...");
        });

        it("should call prayerRequestService.sendPrayerRequest with appropriate values", function () {

            prayerRequestController.send(prayerRequest);

            expect(prayerRequestService.sendPrayerRequest).toHaveBeenCalledWith(prayerRequest.email, prayerRequest.prayFor);
        });

        it("should set the message to be the resolved promise values", function () {

            prayerRequestController.send(prayerRequest);

            var stubResponse = { success: true, text: "worked!" };
            deferred.resolve(stubResponse);
            $rootScope.$digest(); // So Angular processes the resolved promise

            var message = prayerRequestController.message;
            expect(message.success).toBe(stubResponse.success);
            expect(message.text).toBe(stubResponse.text);
        });

        it("should set the message on promise rejection", function () {

            prayerRequestController.send(prayerRequest);

            deferred.reject("Go away");
            $rootScope.$digest(); // So Angular processes the resolved promise

            var message = prayerRequestController.message;
            expect(message.success).toBe(false);
            expect(message.text).toBe("Sorry your email was not sent");
        });
    });
});
