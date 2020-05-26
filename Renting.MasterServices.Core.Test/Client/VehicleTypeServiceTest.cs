using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Test.Data;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]
    public class VehicleTypeServiceTest
    {
        private IVehicleTypeRepository vehicleTypeRepository;
        private ILog log;
        private IVehicleTypeService vehicleTypeService;


        [TestInitialize]
        public void Init()
        {
            vehicleTypeRepository = Substitute.For<IVehicleTypeRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            vehicleTypeService = new VehicleTypeService(vehicleTypeRepository, mapper, log);
        }

        [TestMethod]
        public void GetListVehicleTypes_ReturnListWithThreeVehicleTypes() 
        {
            // Arrage
            int processed = 0;
            var listVehicleTypeSpected = VehicleTypeServiceData.GetListVehicleTypeWithThreeElements();

            vehicleTypeRepository.GetVehicleTypes().Returns(listVehicleTypeSpected);
            vehicleTypeRepository.When(fx => fx.GetVehicleTypes()).Do(fx => ++processed);

            // Act
            var vehicleTypeSpectedTask = vehicleTypeService.GetVehicleTypesAsync();
            vehicleTypeSpectedTask.Wait();

            // Assert
            Assert.IsTrue(vehicleTypeSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(vehicleTypeSpectedTask.Result);
            Assert.AreEqual(3, vehicleTypeSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            vehicleTypeRepository.Received(1).GetVehicleTypes();
        }

        [TestMethod]
        public void GetListVehicleTypes_ReturnEmptyListVehicleTypes()
        {
            // Arrage
            int processed = 0;
            var listVehicleTypeSpected = VehicleTypeServiceData.GetListVehicleTypeEmpty();

            vehicleTypeRepository.GetVehicleTypes().Returns(listVehicleTypeSpected);
            vehicleTypeRepository.When(fx => fx.GetVehicleTypes()).Do(fx => ++processed);

            // Act
            var vehicleTypeSpectedTask = vehicleTypeService.GetVehicleTypesAsync();
            vehicleTypeSpectedTask.Wait();

            // Assert
            Assert.IsTrue(vehicleTypeSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(vehicleTypeSpectedTask.Result);
            Assert.AreEqual(0, vehicleTypeSpectedTask.Result.Count);
            Assert.AreEqual(1, processed);
            vehicleTypeRepository.Received(1).GetVehicleTypes();
        }
    }
}
