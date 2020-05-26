using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Test.Data;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]
    public class EconomicGroupServiceTest
    {
        private IEconomicGroupRepository economicGroupRepository;
        private ILog log;
        private IEconomicGroupService economicGroupService;
        private ICacheService cache;

        [TestInitialize]
        public void Init()
        {
            economicGroupRepository = Substitute.For<IEconomicGroupRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            cache = Substitute.For<ICacheService>();
            economicGroupService = new EconomicGroupService(economicGroupRepository, mapper, log, cache);
        }

        [TestMethod]
        public void GetListEconomicGroups_GivenUserAsAdmin_ReturnListWithThreeEconomicGroups() 
        {
            // Arrage
            int processed = 0;
            var userId = "810";
            var isAdmin = true;

            var listEconomicGroupSpected = EconomicGroupServiceData.GetListEconomicGroupWithThreeElements();

            economicGroupRepository.GetEconomicsGroupAsync().Returns(listEconomicGroupSpected);
            economicGroupRepository.When(fx => fx.GetEconomicsGroupAsync()).Do(fx => ++processed);

            // Act
            var economicGroupSpectedTask = economicGroupService.GetEconomicsGroupAsync(userId, isAdmin);
            economicGroupSpectedTask.Wait();

            // Assert
            Assert.IsTrue(economicGroupSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(economicGroupSpectedTask.Result);
            Assert.AreEqual(3, economicGroupSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            economicGroupRepository.Received(1).GetEconomicsGroupAsync();
        }

        [TestMethod]
        public void GetListEconomicGroups_GivenUserAsNoAdmin_ReturnListWithThreeEconomicGroups()
        {
            // Arrage
            int processed = 0;
            var userId = "810";
            var isAdmin = false;

            var listEconomicGroupSpected = EconomicGroupServiceData.GetListEconomicGroupWithThreeElements();

            economicGroupRepository.GetEconomicsGroupAsync(Arg.Any<string>()).Returns(listEconomicGroupSpected);
            economicGroupRepository.When(fx => fx.GetEconomicsGroupAsync(Arg.Any<string>())).Do(fx => ++processed);

            // Act
            var economicGroupSpectedTask = economicGroupService.GetEconomicsGroupAsync(userId, isAdmin);
            economicGroupSpectedTask.Wait();

            // Assert
            Assert.IsTrue(economicGroupSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(economicGroupSpectedTask.Result);
            Assert.AreEqual(3, economicGroupSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            economicGroupRepository.Received(1).GetEconomicsGroupAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void GetListEconomicGroups_GivenUserAsNoAdmin_ReturnListEmptyEconomicGroups()
        {
            // Arrage
            int processed = 0;
            var userId = "810";
            var isAdmin = false;

            var listEconomicGroupSpected = EconomicGroupServiceData.GetListEmptyEconomicGroup();

            economicGroupRepository.GetEconomicsGroupAsync(Arg.Any<string>()).Returns(listEconomicGroupSpected);
            economicGroupRepository.When(fx => fx.GetEconomicsGroupAsync(Arg.Any<string>())).Do(fx => ++processed);

            // Act
            var economicGroupSpectedTask = economicGroupService.GetEconomicsGroupAsync(userId, isAdmin);
            economicGroupSpectedTask.Wait();

            // Assert
            Assert.IsTrue(economicGroupSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(economicGroupSpectedTask.Result);
            Assert.AreEqual(0, economicGroupSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            economicGroupRepository.Received(1).GetEconomicsGroupAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void GetListEconomicGroups_GivenUserAsAdmin_ReturnListEmptyEconomicGroups()
        {
            // Arrage
            int processed = 0;
            var userId = "810";
            var isAdmin = true;

            var listEconomicGroupSpected = EconomicGroupServiceData.GetListEmptyEconomicGroup();

            economicGroupRepository.GetEconomicsGroupAsync().Returns(listEconomicGroupSpected);
            economicGroupRepository.When(fx => fx.GetEconomicsGroupAsync()).Do(fx => ++processed);

            // Act
            var economicGroupSpectedTask = economicGroupService.GetEconomicsGroupAsync(userId, isAdmin);
            economicGroupSpectedTask.Wait();

            // Assert
            Assert.IsTrue(economicGroupSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(economicGroupSpectedTask.Result);
            Assert.AreEqual(0, economicGroupSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            economicGroupRepository.Received(1).GetEconomicsGroupAsync();
        }
    }
}
