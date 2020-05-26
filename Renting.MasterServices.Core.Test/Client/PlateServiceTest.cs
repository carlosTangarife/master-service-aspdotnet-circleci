using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Test.Data;
using Renting.MasterServices.Domain.Entities.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]
    public class PlateServiceTest
    {
        private IPlateRepository plateByClientRepository;
        private ILog log;
        private IPlateService plateService;

        [TestInitialize]
        public void Init()
        {
            plateByClientRepository = Substitute.For<IPlateRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            plateService = new PlateService(plateByClientRepository, mapper, log);
        }

        [TestMethod]
        public void GetPlates_ReturnPlatesFound()
        {
            // Arrage
            int processed = 0;
            var clientId = 810;
            var plateSpected = PlateServiceData.GetListPlateWithThreeElements();

            plateByClientRepository.GetPlatesByClient(Arg.Any<int>()).Returns(plateSpected);
            plateByClientRepository.When(fx => fx.GetPlatesByClient(Arg.Any<int>())).Do(fx => ++processed);

            // Act
            var plateSpectedTask = plateService.GetPlatesByClientAsync(clientId);
            plateSpectedTask.Wait();

            // Assert
            Assert.IsNotNull(plateSpectedTask);
            Assert.IsTrue(plateSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(plateSpectedTask.Result);
            Assert.AreEqual(3, plateSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
        }

        [TestMethod]
        public void UpdatePlateKm_GivenPlateKmAndUserEmailOk()
        {
            // Arrage
            int processed = 0;
            var userEmail = "test@ceiba.com.co";
            var plateKmRequestDto = PlateServiceData.GetPlateKmRequestDto();
            plateByClientRepository.UpdatePlateKm(Arg.Any<PlateKmRequest>(), Arg.Any<string>());
            plateByClientRepository.When(fx => fx.UpdatePlateKm(Arg.Any<PlateKmRequest>(), Arg.Any<string>())).Do(fx => ++processed);

            // Act
            var plateSpectedTask = plateService.UpdatePlateKmAsync(plateKmRequestDto, userEmail);
            plateSpectedTask.Wait();

            // Assert
            Assert.IsNotNull(plateSpectedTask);
            Assert.IsTrue(plateSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.AreEqual(1, processed);
            plateByClientRepository.Received(1).UpdatePlateKm(Arg.Any<PlateKmRequest>(), Arg.Any<string>());
        }
    }
}