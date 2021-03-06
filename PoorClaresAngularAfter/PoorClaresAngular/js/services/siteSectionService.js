﻿"use strict";
angular.module("poorClaresApp.services").factory("siteSectionService", [function () {
        var siteSection = "home";

        function getSiteSection() {
            return siteSection;
        }

        function determineSiteSection(path) {
            var newSiteSection = "home";

            if (path.indexOf("/theConvent/") !== -1) {
                newSiteSection = "theConvent";
            } else if (path !== "/") {
                newSiteSection = "main";
            }

            siteSection = newSiteSection;
        }

        return {
            getSiteSection: getSiteSection,
            determineSiteSection: determineSiteSection
        };
    }]);
//# sourceMappingURL=siteSectionService.js.map
