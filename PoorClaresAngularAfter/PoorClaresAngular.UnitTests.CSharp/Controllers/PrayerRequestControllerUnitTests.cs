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

        [TestMethod]
        public void SendPrayerRequest_should_send_2_emails_driven_by_application_settings()
        {
            // Arrange
            var smtpClientHost = "SmtpClientHost";
            var smtpClientPort = 999;
            var smtpUserName = "username";
            var smtpPassword = "password";
            var prayerRequestEmailAddress = "praying@poorclares";
            var prayerRequestEmailSubject = "request";
            var prayerResponseEmailSubject = "response";
            var filePathText = "text path";
            var filePathHtml = "HTML path";
            var text = "I'm text me";
            var html = "I'm HTML me";

            _applicationSettingsMock.SetupGet(x => x.SmtpClientHost).Returns(smtpClientHost);
            _applicationSettingsMock.SetupGet(x => x.SmtpClientPort).Returns(smtpClientPort);
            _applicationSettingsMock.SetupGet(x => x.SmtpUserName).Returns(smtpUserName);
            _applicationSettingsMock.SetupGet(x => x.SmtpPassword).Returns(smtpPassword);
            _applicationSettingsMock.SetupGet(x => x.PrayerRequestEmailAddress).Returns(prayerRequestEmailAddress);
            _applicationSettingsMock.SetupGet(x => x.PrayerRequestEmailSubject).Returns(prayerRequestEmailSubject);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailSubject).Returns(prayerResponseEmailSubject);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailFilePathText).Returns(filePathText);
            _applicationSettingsMock.SetupGet(x => x.PrayerResponseEmailFilePathHtml).Returns(filePathHtml);

            _mailerMock.Setup(x => x.ReadTextFromFile(filePathText)).Returns(text);
            _mailerMock.Setup(x => x.ReadTextFromFile(filePathHtml)).Returns(html);

            // Act
            var prayerRequest = new PrayerRequest { email = "fds@fdsfs", prayFor = "pray for me" };
            var result = _controller.Index(prayerRequest);

            // Assert
            _mailerMock.Verify(x => x.ReadTextFromFile(filePathText));
            _mailerMock.Verify(x => x.ReadTextFromFile(filePathHtml));
            _mailerMock.Verify(x =>
                x.SendMail(smtpClientHost, smtpClientPort, smtpUserName, smtpPassword, prayerRequest.email,
                           prayerRequestEmailAddress, prayerRequestEmailSubject, prayerRequest.prayFor, null)
                );
            _mailerMock.Verify(x =>
                x.SendMail(smtpClientHost, smtpClientPort, smtpUserName, smtpPassword, prayerRequestEmailAddress,
                           prayerRequest.email, prayerResponseEmailSubject, text, html)
                );
        }
    }
}
