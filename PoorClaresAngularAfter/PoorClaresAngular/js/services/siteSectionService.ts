"use strict";

interface ISiteSectionService {
    getSiteSection: () => string;
    determineSiteSection: (path: string) => void;
}

angular.module("poorClaresApp.services").factory(

    "siteSectionService",

    [ // No dependencies at present
    function (): ISiteSectionService {

        var siteSection = "home";

        function getSiteSection() {
            return siteSection;
        }

        function determineSiteSection(path: string) {
            var newSiteSection = "home";

            if (path.indexOf("/theConvent/") !== -1) {
                newSiteSection = "theConvent";
            }
            else if (path !== "/") {
                newSiteSection = "main";
            }

            siteSection = newSiteSection;
        }

        return {
            getSiteSection: getSiteSection,
            determineSiteSection: determineSiteSection
        };
    }]);
