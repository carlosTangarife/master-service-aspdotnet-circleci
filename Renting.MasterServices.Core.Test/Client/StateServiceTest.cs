using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Test.Data;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Linq;

namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]
    public class StateServiceTest
    {
        private IStateRepository stateRepository;
        private ILog log;
        private IStateService stateService;


        [TestInitialize]
        public void Init()
        {
            stateRepository = Substitute.For<IStateRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            stateService = new StateService(stateRepository, mapper, log);
        }

        [TestMethod]
        public void GetStates_ReturnListWithThreeStates()
        {
            // Arrage
            int processed = 0;
            var parametersStates = new string[] { "420","421", "422" };
            var listState = StateServiceData.GetListStateWithThreeElements();

            stateRepository.GetStates(Arg.Any<string[]>()).Returns(listState);
            stateRepository.When(fx => fx.GetStates(Arg.Any<string[]>())).Do(fx => ++processed);

            // Act
            var listStateSpected = stateService.GetStates(parametersStates);

            // Assert
            Assert.IsTrue(listStateSpected.Any());
            Assert.AreEqual(3, listStateSpected.Count);
            Assert.AreEqual(1, processed);
            stateRepository.Received(1).GetStates(Arg.Any<string[]>());
        }

        [TestMethod]
        public void GetStates_ReturnEmptyListStates()
        {
            // Arrage
            int processed = 0;
            var parametersStates = new string[] { };
            var listStateEmpty = StateServiceData.GetListEmptyStates();

            stateRepository.GetStates(Arg.Any<string[]>()).Returns(listStateEmpty);
            stateRepository.When(fx => fx.GetStates(Arg.Any<string[]>())).Do(fx => ++processed);

            // Act
            var listStateSpected = stateService.GetStates(parametersStates);

            // Assert
            Assert.IsNotNull(listStateSpected);
            Assert.AreEqual(0, listStateSpected.Count);
            Assert.AreEqual(1, processed);
            stateRepository.Received(1).GetStates(Arg.Any<string[]>());
        }
    }
}
