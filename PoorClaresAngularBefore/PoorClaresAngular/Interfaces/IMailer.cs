using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoorClaresAngular.Interfaces
{
    public interface IMailer
    {
        void SendMail(string smtpClientHost, int smtpClientHostPort, string smtpUserName, string smtpPassword,
            string fromEmail, string toEmail, string subject, string text, string html = null);

        string ReadTextFromFile(string filePath);
    }
}