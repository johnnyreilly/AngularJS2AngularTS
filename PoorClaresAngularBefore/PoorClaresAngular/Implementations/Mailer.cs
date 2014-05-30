using PoorClaresAngular.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Hosting;

namespace PoorClaresAngular.Implementations
{
    public class Mailer : IMailer
    {
        public void SendMail(string smtpClientHost, int smtpClientHostPort, string smtpUserName, string smtpPassword,
    string fromEmail, string toEmail, string subject, string text, string html = null)
        {
            var mailMsg = new MailMessage();
            mailMsg.To.Add(new MailAddress(toEmail, toEmail));

            // From
            mailMsg.From = new MailAddress(fromEmail, fromEmail);
            //mailMsg.Bcc.Add(new MailAddress(toEmail, toEmail));

            // Subject and multipart/alternative Body
            mailMsg.Subject = subject;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            if (!string.IsNullOrEmpty(html)) mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            var smtpClient = new SmtpClient(smtpClientHost, smtpClientHostPort);
            var credentials = new NetworkCredential(smtpUserName, smtpPassword);
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }

        public string ReadTextFromFile(string filePath)
        {
            return System.IO.File.ReadAllText(HostingEnvironment.MapPath(filePath));
        }

    }
}