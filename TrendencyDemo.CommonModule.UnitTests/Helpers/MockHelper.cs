using DrendencyDemo.Web.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.CommonModule.UnitTests.Helpers
{
    public class MockHelper
    {
        public static TrendencyDemoDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TrendencyDemoDbContext>()
                .UseInMemoryDatabase(databaseName: "TrendencyDemoContextDataBase")
                .Options;
            return new TrendencyDemoDbContext(options);
        }

        public static ICurrentUserService GetCurrentUserService()
        {
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(x => x.GetUserId()).Returns(1);
            currentUserServiceMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            return currentUserServiceMock.Object;
        }

        public static IDateInfoService GetDateInfoService()
        {
            var dateInfoServiceMock = new Mock<IDateInfoService>();
            dateInfoServiceMock.Setup(x => x.UtcNow).Returns(new DateTime(2020, 1, 1, 10, 30, 0));
            dateInfoServiceMock.Setup(x => x.UtcNow).Returns(new DateTime(2019, 1, 1, 10, 30, 0));
            return dateInfoServiceMock.Object;
        }
    }
}
