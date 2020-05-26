using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Domain.IRepository.Client;
using System.Threading.Tasks;
using Renting.MasterServices.Core.Test.Data;
namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]
    public class ParameterServiceTest
    {
        private IParameterRepository parameterRepository;
        private ILog log;
        private IParameterService parameterService;

        [TestInitialize]
        public void Init()
        {
            parameterRepository = Substitute.For<IParameterRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            parameterService = new ParameterService(parameterRepository, mapper, log);
        }

        [TestMethod]
        public void GetParameters_ReturnParametersFound()
        {
            // Arrage
            int processed = 0;
            var parameterName = "UrlPdfActaPreoperativo";
            var parameterSpected = ParameterServiceData.GetParameterDefined();

            parameterRepository.GetParameterByName(Arg.Any<string>(), Arg.Any<string>()).Returns(parameterSpected);
            parameterRepository.When(fx => fx.GetParameterByName(Arg.Any<string>(), Arg.Any<string>())).Do(fx => ++processed);

            // Act
            var parameterSpectedTask = parameterService.GetParameterByNameAsync(parameterName);
            parameterSpectedTask.Wait();

            // Assert
            Assert.IsTrue(parameterSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNotNull(parameterSpectedTask.Result);
            Assert.AreEqual(1, processed);
            parameterRepository.Received(1).GetParameterByName(Arg.Any<string>(), Arg.Any<string>());
        }

        [TestMethod]
        public void GetParameters_ReturnParametersNotFound()
        {
            // Arrage
            int processed = 0;
            var parameterName = string.Empty;
            var parameterSpected = ParameterServiceData.GetParameterUnDefined();

            parameterRepository.GetParameterByName(Arg.Any<string>(), Arg.Any<string>()).Returns(parameterSpected);
            parameterRepository.When(fx => fx.GetParameterByName(Arg.Any<string>(), Arg.Any<string>())).Do(fx => ++processed);

            // Act
            var parameterSpectedTask = parameterService.GetParameterByNameAsync(parameterName);
            parameterSpectedTask.Wait();

            // Assert
            Assert.IsTrue(parameterSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.IsNull(parameterSpectedTask.Result);
            Assert.AreEqual(1, processed);
            parameterRepository.Received(1).GetParameterByName(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
