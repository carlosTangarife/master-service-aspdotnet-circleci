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
    public class ClientUserServiceTest
    {
        private IClientUserRepository clientUserRepository;
        private ILog log;
        private IClientUserService clientUserService;
        private ICacheService cache;

        [TestInitialize]
        public void Init()
        {
            clientUserRepository = Substitute.For<IClientUserRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            cache = Substitute.For<ICacheService>();
            clientUserService = new ClientUserService(clientUserRepository, mapper, log, cache);
        }

        [TestMethod]
        public void GetListClientUsers_GivenUserAsAdmin_ReturnListWithThreeClientUsers() 
        {
            // Arrange
            int processed = 0;
            var userId = "810";
            var isAdmin = true;
            var economicGroupId = 1;

            var listClientUserSpected = ClientUserServiceData.GetListClientUserWithThreeElements();

            clientUserRepository.GetClientsByUserIdAsync(economicGroupId: Arg.Any<int>()).Returns(listClientUserSpected);
            clientUserRepository.When(fx => fx.GetClientsByUserIdAsync(economicGroupId: Arg.Any<int>())).Do(fx => ++processed);

            // Act
            var clientUserSpectedTask = clientUserService.GetClientsByUserIdAsync(userId, isAdmin, economicGroupId);
            clientUserSpectedTask.Wait();

            // Assert
            Assert.IsTrue(clientUserSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(clientUserSpectedTask.Result);
            Assert.AreEqual(3, clientUserSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            clientUserRepository.Received(1).GetClientsByUserIdAsync(economicGroupId: Arg.Any<int>());
        }

        [TestMethod]
        public void GetListClientUsers_GivenUserAsNoAdmin_ReturnListWithThreeClientUsers()
        {
            // Arrange
            int processed = 0;
            var userId = "810";
            var isAdmin = false;
            var economicGroupId = 1;

            var listClientUserSpected = ClientUserServiceData.GetListClientUserWithThreeElements();

            clientUserRepository.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>()).Returns(listClientUserSpected);
            clientUserRepository.When(fx => fx.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>())).Do(fx => ++processed);

            // Act
            var clientUserSpectedTask = clientUserService.GetClientsByUserIdAsync(userId, isAdmin, economicGroupId);
            clientUserSpectedTask.Wait();

            // Assert
            Assert.IsTrue(clientUserSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(clientUserSpectedTask.Result);
            Assert.AreEqual(3, clientUserSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            clientUserRepository.Received(1).GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>());
        }


        [TestMethod]
        public void GetListClientUsers_GivenUserAsNoAdmin_ReturnListEmptyClientUsers()
        {
            // Arrange
            int processed = 0;
            var userId = "810";
            var isAdmin = false;
            var economicGroupId = 1;

            var listClientUserSpected = ClientUserServiceData.GetListEmptyClientUser();

            clientUserRepository.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>()).Returns(listClientUserSpected);
            clientUserRepository.When(fx => fx.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>())).Do(fx => ++processed);

            // Act
            var clientUserSpectedTask = clientUserService.GetClientsByUserIdAsync(userId, isAdmin, economicGroupId);
            clientUserSpectedTask.Wait();

            // Assert
            Assert.IsTrue(clientUserSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(clientUserSpectedTask.Result);
            Assert.AreEqual(0, clientUserSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            clientUserRepository.Received(1).GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>());
        }

        [TestMethod]
        public void GetListClientUsers_GivenUserAsAdmin_ReturnListEmptyClientUsers()
        {
            // Arrange
            int processed = 0;
            var userId = "810";
            var isAdmin = true;
            var economicGroupId = 1;

            var listClientUserSpected = ClientUserServiceData.GetListEmptyClientUser();

            clientUserRepository.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>()).Returns(listClientUserSpected);
            clientUserRepository.When(fx => fx.GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>())).Do(fx => ++processed);

            // Act
            var clientUserSpectedTask = clientUserService.GetClientsByUserIdAsync(userId, isAdmin, economicGroupId);
            clientUserSpectedTask.Wait();

            // Assert
            Assert.IsTrue(clientUserSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(clientUserSpectedTask.Result);
            Assert.AreEqual(0, clientUserSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            clientUserRepository.Received(1).GetClientsByUserIdAsync(Arg.Any<string>(), Arg.Any<int>());
        }
    }
}
