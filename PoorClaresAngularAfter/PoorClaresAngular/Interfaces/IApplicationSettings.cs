using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoorClaresAngular.Interfaces
{
    public interface IApplicationSettings
    {
        string SmtpUserName { get; }
        string SmtpPassword { get; }
        string SmtpClientHost { get; }
        int SmtpClientPort { get; }
        string PrayerResponseEmailSubject { get; }
        string PrayerResponseEmailFilePathText { get; }
        string PrayerResponseEmailFilePathHtml { get; }
        string PrayerRequestEmailSubject { get; }
        string PrayerRequestEmailAddress { get; }
    }
}