using DrendencyDemo.Web.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendencyDemo.Common.Configs;
using TrendencyDemo.CommonModule.Aggregates;
using TrendencyDemo.CommonModule.Services;
using TrendencyDemo.CommonModule.UnitTests.Fakes;
using TrendencyDemo.CommonModule.UnitTests.Helpers;
using TrendencyDemo.Dal.Enums;

namespace TrendencyDemo.CommonModule.UnitTests
{
    [TestClass]
    public class EmailServiceTest
    {
        private readonly TrendencyDemoDbContext _context;
        private readonly EmailService _emailService;

        public EmailServiceTest()
        {
            _context = MockHelper.GetDbContext();
            ServiceBaseAggregate serviceBaseAggregateMock = new ServiceBaseAggregate(_context,
                MockHelper.GetDateInfoService(),
                MockHelper.GetCurrentUserService());

            var emailConfigs = new OptionsFake<EmailConfigs>(new EmailConfigs());
            var emailCreds = new OptionsFake<EmailCredentials>(new EmailCredentials());

            _emailService = new EmailService(serviceBaseAggregateMock,
                emailConfigs,
                emailCreds);
        }

        [TestMethod]
        public async Task AddEmail_ShouldCreateEmail()
        {
            // Arrange
            string subject = "email subject";
            string body = "email body";
            string address = "email address";

            //Act
            _emailService.AddEmail(address, subject, body);
            await _context.SaveChangesAsync();

            // Assert
            var createdEmail = _context.Emails.Single();

            Assert.IsTrue(createdEmail.Body == body);
            Assert.IsTrue(createdEmail.CreatedDate == new DateTime(2019, 1, 1, 10, 30, 0));
            Assert.IsTrue(createdEmail.EmailState == EmailState.Pending);
            Assert.IsTrue(createdEmail.LastError == null);
            Assert.IsTrue(createdEmail.LastTriedDate == null);
            Assert.IsTrue(createdEmail.Subject == subject);
            Assert.IsTrue(createdEmail.To == address);
            Assert.IsTrue(createdEmail.TryCount == 0);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
