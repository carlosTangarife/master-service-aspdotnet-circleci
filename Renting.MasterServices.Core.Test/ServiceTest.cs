using AutoMapper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Dtos;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Services;
using Renting.MasterServices.Domain.Entities;
using Renting.MasterServices.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Renting.MasterServices.Core.Test
{
    [TestClass]
    public class ServiceTest
    {
        private IERepository<Dummy> _Repository;
        private IService<Dummy, DummyDto> _Service;
        private List<Dummy> _List;
        private DummyDto dummyDto;
        private int dummyId;

        [TestInitialize]
        public void Init()
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
            _Repository = Substitute.For<IERepository<Dummy>>();
            var _Logger = Substitute.For<ILog>();
            _Service = new Service<Dummy, DummyDto>(_Repository, _Logger, mapper);

            _List = new List<Dummy>
            {
                new Dummy
                {
                    IntNumber = 1, StringVal = "abc"
                },
                new Dummy
                {
                    IntNumber = 2, StringVal = "bcd"
                }
            };
            dummyDto = mapper.Map<DummyDto>(_List[0]);
            dummyId = 1;
        }

        [TestMethod]
        public void ServiceGetAllWithAllParametersTest()
        {
            // Arrange
            _Repository.GetAll(Arg.Any<Expression<Func<Dummy, bool>>>(),
               Arg.Any<Func<IQueryable<Dummy>, IOrderedQueryable<Dummy>>>(),
               Arg.Any<string>()).Returns(_List.AsQueryable());
            // Act
            var result = _Service.GetAll();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ServiceGetAllWithFilterTest()
        {
            // Arrange
            _Repository.GetAll(Arg.Any<Expression<Func<Dummy, bool>>>(),
           Arg.Any<Func<IQueryable<Dummy>, IOrderedQueryable<Dummy>>>(),
           Arg.Any<string>()).Returns(_List.AsQueryable());

            // Act
            var result = _Service.GetAll(filter: _filter => _filter.StringVal == "1");
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ServiceGetAllWithFilterAndOrderByTest()
        {
            // Arrange
            _Repository.GetAll(Arg.Any<Expression<Func<Dummy, bool>>>(),
               Arg.Any<Func<IQueryable<Dummy>, IOrderedQueryable<Dummy>>>(),
               Arg.Any<string>()).Returns(_List.AsQueryable());
            // Act
            var result = _Service.GetAll(filter: _filter => _filter.StringVal == "1", orderBy: _orderBy => _orderBy.OrderBy(x => x.StringVal));
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ServiceGetByIdTest()
        {
            // Arrange
            _Repository.FindById(Arg.Any<int>()).Returns(_List[0]);
            // Act
            var result = _Service.FindById(dummyId);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IntNumber == dummyId);
        }

        [TestMethod]
        public void ServiceAddTest()
        {
            // Arrange
            int dummyAdded = 0;
            _Repository.When(fx => fx.Add(Arg.Any<Dummy>())).Do(fx => ++dummyAdded);
            // Act
            _Service.Add(dummyDto);
            // Assert
            Assert.IsTrue(dummyAdded > 0);
        }

        [TestMethod]
        public void ServiceAddAsyncTest()
        {
            // Arrange
            int dummyAddedAsync = 0;
            _Repository.When(fx => fx.AddAsync(Arg.Any<Dummy>())).Do(fx => ++dummyAddedAsync);
            // Act
            var dummyAddTask = _Service.AddAsync(dummyDto);
            dummyAddTask.Wait();
            // Assert
            Assert.IsTrue(dummyAddTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion);
            Assert.AreEqual(1, dummyAddedAsync);
        }

        [TestMethod]
        public void ServiceDeleteTest()
        {
            int dummyDeleted = 0;
            _Repository.When(fx => fx.Delete(Arg.Any<Dummy>())).Do(fx => ++dummyDeleted);
            _Service.Delete(dummyDto);
            Assert.AreEqual(1, dummyDeleted);
        }

        [TestMethod]
        public void ServiceDeleteAsyncTest()
        {
            int dummyDeletedAsync = 0;
            _Repository.When(fx => fx.DeleteAsync(Arg.Any<Dummy>())).Do(fx => ++dummyDeletedAsync);
            var addClientTask = _Service.DeleteAsync(dummyDto);
            addClientTask.Wait();
            Assert.IsTrue(addClientTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion);
            Assert.AreEqual(1, dummyDeletedAsync);
        }

        [TestMethod]
        public void ServiceDeleteByIdAsyncTest()
        {
            int dummyDeletedAsync = 0;
            _Repository.When(fx => fx.FindById(Arg.Any<int>())).Do(fx => ++dummyDeletedAsync);
            _Repository.When(fx => fx.DeleteAsync(Arg.Any<Dummy>())).Do(fx => ++dummyDeletedAsync);
            var addClientTask = _Service.DeleteAsync(dummyId);
            addClientTask.Wait();
            Assert.IsTrue(addClientTask.Status == System.Threading.Tasks.TaskStatus.RanToCompletion);
            Assert.AreEqual(2, dummyDeletedAsync);
        }

        [TestMethod]
        public void ServiceDeleteByIdTest()
        {
            int dummyDeletedById = 0;
            _Repository.When(fx => fx.FindById(Arg.Any<int>())).Do(fx => ++dummyDeletedById);
            _Repository.When(fx => fx.Delete(Arg.Any<Dummy>())).Do(fx => ++dummyDeletedById);
            _Service.Delete(dummyId);
            Assert.AreEqual(2, dummyDeletedById);
        }

        [TestMethod]
        public void ServiceUpdateTest()
        {
            int dummyUpdated = 0;
            _Repository.When(fx => fx.Update(Arg.Any<Dummy>())).Do(fx => ++dummyUpdated);
            _Service.Update(dummyDto);
            Assert.AreEqual(1, dummyUpdated);
        }

        [TestMethod]
        public void ServiceUpdateAsyncTest()
        {
            int dummyUpdatedAsync = 0;
            _Repository.When(fx => fx.UpdateAsync(Arg.Any<Dummy>())).Do(fx => ++dummyUpdatedAsync);
            var dummyUpdatedTask = _Service.UpdateAsync(dummyDto);
            dummyUpdatedTask.Wait();
            Assert.AreEqual(dummyUpdatedTask.Status, System.Threading.Tasks.TaskStatus.RanToCompletion);
            Assert.AreEqual(1, dummyUpdatedAsync);
        }
    }
}
