using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoorClaresAngular.Controllers;
using PoorClaresAngular.Models;
using PoorClaresAngular.Interfaces;
using Moq;

namespace PoorClaresAngular.UnitTests.Controllers
{
    [TestClass]
    public class PrayerRequestControllerUnitTests
    {
        private PrayerRequestController _controller;
        private Mock<IApplicationSettings> _applicationSettingsMock;
        private Mock<IMailer> _mailerMock;

        [TestInitialize]
        public void Setup()
        {
            _applicationSettingsMock = new Mock<IApplicationSettings>();
            _mailerMock = new Mock<IMailer>();

            _controller = new PrayerRequestController(_applicationSettingsMock.Object, _mailerMock.Object);
        }

        [TestMethod]
        public void SendPrayerRequest_should_return_a_JsonResult()
        {
            var result = _controller.Index(new PrayerRequest { email = "fds@fdsfs", prayFor = "pray for me" });

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void SendPrayerRequest_should_return_ModelState_error_messages()
        {
            // Arrange
            _controller.ModelState.AddModelError("TestError", "Something went wrong..."); // Force ModelState to not IsValid

            // Act
            var result = _controller.Index(new PrayerRequest { email = "fds@fdsfs", prayFor = "pray for me" });

            // Assert
            Assert.AreEqual(false, result.GetDataPropertyAs<bool>("success"));
            Assert.AreEqual("Sorry your email was not sent because: Something went wrong...", result.GetDataPropertyAs<string>("text"));
        }

        [TestMethod]
        public void SendPrayerRequest_should_return_success_message()
        {
            var result = _controller.Index(new PrayerRequest { email = "fds@fdsfs", prayFor = "pray for me" });

            Assert.AreEqual(true, result.GetDataPropertyAs<bool>("success"));
            Assert.AreEqual("Thanks for sending your prayer request - we will pray.", result.GetDataPropertyAs<string>("text"));
        }

        private string _smtpClientHost = "SmtpClientHost";
        private int _smtpClientPort = 999;
        private string _smtpUserName = "username";
        private string _smtpPassword = "password";
        private string _prayerRequestEmailAddress = "praying@poorclares";
        private string _prayerRequestEmailSubject = "request";
        private string _prayerResponseEmailSubject = "response";
        private string _filePathText = "text path";
        private string _filePathHtml = "HTML path";
        private string _text = "I'm text me";
        private string _html = "I'm HTML me";

        private void SendPrayerRequest_shared_setup()
        {
            _applicationSettingsMock.SetupGet(x => x.SmtpClientHost).Returns(_smtpClientHost);
            _applicationSettingsMock.SetupGet(x => x.SmtpClientPort).Returns(_smtpClientPort);
            _applicationSettingsMock.SetupGet(x => x.SmtpUserName).Returns(_smtpUserName);
            _applicationSettingsMock.SetupGet(x => x.SmtpPassword).Returns(_smtpPassword);
            _applicationSettingsMock.SetupGet(x => x.PrayerRequestEmailAddress).Returns(_prayerRequestEmailAddress);
            _applicationSettingsMock.SetupGet(x => x.PrayerRequestEmailSubject).Returns(_prayerRequestEmailSubject);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailSubject).Returns(_prayerResponseEmailSubject);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailFilePathText).Returns(_filePathText);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailFilePathHtml).Returns(_filePathHtml);

            _mailerMock.Setup(x => x.ReadTextFromFile(_filePathText)).Returns(_text);
            _mailerMock.Setup(x => x.ReadTextFromFile(_filePathHtml)).Returns(_html);
        }

        [TestMethod]
        public void SendPrayerRequest_should_send_2_emails_driven_by_application_settings()
        {
            // Arrange
            SendPrayerRequest_shared_setup();

            // Act
            var prayerRequest = new PrayerRequest { email = "fds@fdsfs", prayFor = "pray for me" };
            var result = _controller.Index(prayerRequest);

            // Assert
            _mailerMock.Verify(x => x.ReadTextFromFile(_filePathText));
            _mailerMock.Verify(x => x.ReadTextFromFile(_filePathHtml));
            _mailerMock.Verify(x =>
                x.SendMail(_smtpClientHost, _smtpClientPort, _smtpUserName, _smtpPassword, prayerRequest.email,
                           _prayerRequestEmailAddress, _prayerRequestEmailSubject, prayerRequest.prayFor, null)
                );
            _mailerMock.Verify(x =>
                x.SendMail(_smtpClientHost, _smtpClientPort, _smtpUserName, _smtpPassword, _prayerRequestEmailAddress,
                           prayerRequest.email, _prayerResponseEmailSubject, _text, _html)
                );
        }

        [TestMethod]
        public void SendPrayerRequest_should_encode_prayFor_to_prevent_XSS_attacks()
        {
            // Arrange
            SendPrayerRequest_shared_setup();

            // Act
            var prayerRequest = new PrayerRequest { email = "fds@fdsfs", prayFor = "<script>alert()</script>" };
            var result = _controller.Index(prayerRequest);

            // Assert
            _mailerMock.Verify(x =>
                x.SendMail(_smtpClientHost, _smtpClientPort, _smtpUserName, _smtpPassword, prayerRequest.email,
                           _prayerRequestEmailAddress, _prayerRequestEmailSubject, prayerRequest.prayFor, null),
                           Times.Never
                );
            _mailerMock.Verify(x =>
                x.SendMail(_smtpClientHost, _smtpClientPort, _smtpUserName, _smtpPassword, prayerRequest.email,
                           _prayerRequestEmailAddress, _prayerRequestEmailSubject,
                           System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(prayerRequest.prayFor, true), null),
                           Times.Once
                );
        }
    }
}
