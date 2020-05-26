using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Test.Data;
using Renting.MasterServices.Domain.IRepository.Client;

namespace Renting.MasterServices.Core.Test.Client
{
    [TestClass]

    public class AttributeServiceTest
    {
        private IAttributeRepository attributeRepository;
        private ILog log;
        private IAttributeService attributeService;

        [TestInitialize]
        public void Init()
        {
            attributeRepository = Substitute.For<IAttributeRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            attributeService = new AttributeService(attributeRepository, mapper, log);
        }

        [TestMethod]
        public void GetListAttributes_ReturnListWithThreeAttributes()
        {
            // Arrage
            int processed = 0;
            var listAttributeDto = AttributeServiceData.GetListWithThreeAttributes();
            attributeRepository.GetAll().Returns(listAttributeDto);
            attributeRepository.When(fx => fx.GetAll()).Do(fx => ++processed);

            // Act
            var listAttributeDtoSpected =  attributeService.GetAttributes();

            // Assert
            Assert.IsNotNull(listAttributeDtoSpected);
            Assert.IsTrue(listAttributeDtoSpected.Count == 3);
            Assert.AreEqual(1, processed);
            attributeRepository.Received(1).GetAll();
        }

        [TestMethod]
        public void GetListAttributes_ReturnEmptyListAttributes()
        {
            // Arrage
            int processed = 0;
            var listAttributeDto = AttributeServiceData.GetListEmptyAttributes();
            attributeRepository.GetAll().Returns(listAttributeDto);
            attributeRepository.When(fx => fx.GetAll()).Do(fx => ++processed);

            // Act
            var listAttributeDtoSpected = attributeService.GetAttributes();

            // Assert
            Assert.IsTrue(listAttributeDtoSpected.Count == 0);
            Assert.AreEqual(1, processed);
            attributeRepository.Received(1).GetAll();
        }
    }
}
