using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Services;
using Renting.MasterServices.Domain.Entities;
using StackExchange.Redis;
using System;

namespace Renting.MasterServices.Core.Test
{
    [TestClass]
    public class CacheServiceTest
    {
        private IDatabase rediscache;
        private ILog logger;
        private ICacheService cacheService;
        private IConnectionMultiplexer connectionMultiplexer;
        private int HoursToExpire;
        [TestInitialize]
        public void Init()
        {
            logger = Substitute.For<ILog>();
            connectionMultiplexer = Substitute.For<IConnectionMultiplexer>();
            rediscache = connectionMultiplexer.GetDatabase();
            HoursToExpire = 2;
            cacheService = new CacheService(connectionMultiplexer, logger, HoursToExpire);
        }

        [TestMethod]
        public void Find_WhenResultIsDefined()
        {
            //Arrange
            int processed = 0;
            var dummy = new {
                Name = "Test",
                Value = "Value"
            };

            rediscache.StringGet(Arg.Any<RedisKey>()).Returns((RedisValue)JsonConvert.SerializeObject(dummy));
            rediscache.When(fx => fx.StringGet(Arg.Any<RedisKey>())).Do(fx => ++processed);

            //Act
            var result = cacheService.Find("clave");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, processed);
            rediscache.Received(1).StringGet(Arg.Any<RedisKey>());

        }

        [TestMethod]
        public void FindGeneric_WhenResultIsNull()
        {
            //Arrange
            int processed = 0;
            var dummy = new Dummy
            {
                IntNumber = 1,
                StringVal = "value test"
            };

            rediscache.StringGet(Arg.Any<RedisKey>()).Returns((RedisValue)string.Empty);
            rediscache.When(fx => fx.StringGet(Arg.Any<RedisKey>())).Do(fx => ++processed);

            //Act
            var result = cacheService.Find("clave", () => dummy);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, processed);
            rediscache.Received(1).StringGet(Arg.Any<RedisKey>());
        }

        [TestMethod]
        public void FindGeneric_WhenResultIsDefined()
        {
            //Arrange
            int processed = 0;
            var dummy = new Dummy
            {
                IntNumber = 1,
                StringVal = "value test"
            };

            rediscache.StringGet(Arg.Any<RedisKey>()).Returns((RedisValue)JsonConvert.SerializeObject(dummy));
            rediscache.When(fx => fx.StringGet(Arg.Any<RedisKey>())).Do(fx => ++processed);

            //Act
            var result = cacheService.Find("clave", () => dummy);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, processed);
            rediscache.Received(1).StringGet(Arg.Any<RedisKey>());
        }

        [TestMethod]
        public void SetTest()
        {
            //Arrange
            int processed = 0;
            object dummy = new
            {
                IntNumber = 1,
                StringVal = "value test"
            };

            rediscache.StringSet(Arg.Any<RedisKey>(), Arg.Any<RedisValue>());
            rediscache.When(fx => fx.StringSet(Arg.Any<RedisKey>(), Arg.Any<RedisValue>(), Arg.Any<TimeSpan>())).Do(fx => ++processed);

            //Act
            cacheService.Set("clave", dummy);

            //Assert
            Assert.AreEqual(1, processed);
            rediscache.Received(1).StringSet(Arg.Any<RedisKey>(), Arg.Any<RedisValue>(), Arg.Any<TimeSpan>());
        }
    }
}
