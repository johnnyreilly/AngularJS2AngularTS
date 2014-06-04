using PoorClaresAngular.Implementations;
using PoorClaresAngular.Interfaces;
using PoorClaresAngular.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoorClaresAngular.Controllers
{
    public class PrayerRequestController : Controller
    {
        private readonly IApplicationSettings _props;
        private readonly IMailer _mailer;

        public PrayerRequestController()
            : this(new ApplicationSettings(), new Mailer())
        {
        }

        public PrayerRequestController(IApplicationSettings props, IMailer mailer)
        {
            _props = props;
            _mailer = mailer;
        }

        //
        // POST: /PrayerRequest/
        [HttpPost]
        public JsonResult Index(PrayerRequest prayerRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(m => m.ErrorMessage);

                return Json(new
                {
                    success = false,
                    text = "Sorry your email was not sent because: " + string.Join(", ", errors)
                });
            }

            try
            {
                // Get email content
                var text = _mailer.ReadTextFromFile(_props.PrayerResponseEmailFilePathText);
                var html = _mailer.ReadTextFromFile(_props.PrayerResponseEmailFilePathHtml);

                // Send prayer request email
                _mailer.SendMail(_props.SmtpClientHost, _props.SmtpClientPort, _props.SmtpUserName, _props.SmtpPassword,
                    fromEmail: prayerRequest.email,
                    toEmail: _props.PrayerRequestEmailAddress,
                    subject: _props.PrayerRequestEmailSubject,
                    text: System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(prayerRequest.prayFor, true));

                // Send prayer response email
                _mailer.SendMail(_props.SmtpClientHost, _props.SmtpClientPort, _props.SmtpUserName, _props.SmtpPassword,
                    fromEmail: _props.PrayerRequestEmailAddress,
                    toEmail: prayerRequest.email,
                    subject: _props.PrayerResponseEmailSubject,
                    text: text,
                    html: html);
            }
            catch (Exception ex)
            {
                Trace.TraceError("{0}\r\n\r\nStack Trace:\r\n\r\n{1}", ex.Message, ex.StackTrace);

                return Json(new
                {
                    success = false,
                    text = "Your prayer request has not been sent - please try mailing: " + _props.PrayerRequestEmailAddress
                });
            }

            return Json(new
            {
                success = true,
                text = "Thanks for sending your prayer request - we will pray."
            });
        }
    }
}