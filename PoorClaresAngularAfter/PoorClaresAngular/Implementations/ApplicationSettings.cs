using PoorClaresAngular.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoorClaresAngular.Implementations
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly Properties.Settings _props = Properties.Settings.Default;

        public string SmtpUserName
        {
            get { return _props.SmtpUserName; }
        }

        public string SmtpPassword
        {
            get { return _props.SmtpPassword; }
        }

        public string SmtpClientHost
        {
            get { return _props.SmtpClientHost; }
        }

        public int SmtpClientPort
        {
            get { return _props.SmtpClientPort; }
        }

        public string PrayerResponseEmailSubject
        {
            get { return _props.PrayerResponseEmailSubject; }
        }

        public string PrayerResponseEmailFilePathHtml
        {
            get { return _props.PrayerResponseEmailFilePathHtml; }
        }

        public string PrayerResponseEmailFilePathText
        {
            get { return _props.PrayerResponseEmailFilePathText; }
        }

        public string PrayerRequestEmailSubject
        {
            get { return _props.PrayerRequestEmailSubject; }
        }

        public string PrayerRequestEmailAddress
        {
            get { return _props.PrayerRequestEmailAddress; }
        }
    }
}