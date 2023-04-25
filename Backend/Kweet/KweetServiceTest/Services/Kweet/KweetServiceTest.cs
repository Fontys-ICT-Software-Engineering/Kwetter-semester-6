using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kweet.Services.Kweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kweet.Data;
using Moq;
using Kweet.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using KweetServiceTest.Services.TestHelper;
using MockQueryable.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Bogus;
using AutoMapper.Configuration.Conventions;

namespace Kweet.Services.Kweet.Tests
{
    [TestClass()]
    public class KweetServiceTest
    {
        private KweetService _kweetService;
        private Mock<DataContext> _dataContextMock;
        private Mock<DbSet<KweetModel>> _mockSet;

        [TestInitialize]
        public void Initialize()
        {
            _dataContextMock = new Mock<DataContext>();
            _mockSet = new Mock<DbSet<KweetModel>>();
        }

        [TestMethod()]
        public async Task getAllKweetsTest()
        {
            _dataContextMock.Setup(x => x.Kweets).ReturnsDbSet(TestHelperClass.GetFakeKweetsList());
            _dataContextMock.Setup(x => x.Likes).ReturnsDbSet(TestHelperClass.GetFakeLikeList());
            _kweetService = new KweetService(_dataContextMock.Object);

            // Arrange
            var userId = "user123";

            // Act
            var result = await _kweetService.getAllKweets(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }
    }
}
