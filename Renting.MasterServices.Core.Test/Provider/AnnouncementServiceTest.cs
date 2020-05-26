using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Renting.MasterServices.Core.Dtos;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Core.Services;
using Renting.MasterServices.Core.Services.Provider;
using Renting.MasterServices.Domain;
using Renting.MasterServices.Domain.Entities.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;
using Renting.MasterServices.Infraestructure.CustomExceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Test.Provider
{
    [TestClass]
    public class AnnouncementServiceTest
    {
        private IAnnouncementRepository announcementRepository;
        private IBlobStorageService blobStorageService;
        private IMapper mapper;
        private ILog log;
        private IConfigProvider config;
        private IFileProcesingService fileProcesingService;
        private IAnnouncementService announcementService;
        private AnnouncementDto announcementDto;
        private Announcement announcement;
        private IFormFile file;

        [TestInitialize]
        public void Init()
        {
            announcementRepository = Substitute.For<IAnnouncementRepository>();
            var mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            log = Substitute.For<ILog>();
            config = Substitute.For<IConfigProvider>();
            blobStorageService = Substitute.For<IBlobStorageService>();
            fileProcesingService = Substitute.For<IFileProcesingService>();
            announcementService = new AnnouncementService(announcementRepository, blobStorageService, fileProcesingService, config, mapper, log);
            file = Substitute.For<IFormFile>();

            var content = "pruebaimagen";
            var fileName = "test.png";
            var memoryStreamFile = new MemoryStream();
            var writer = new StreamWriter(memoryStreamFile);
            writer.Write(content);
            writer.Flush();
            memoryStreamFile.Position = 0;

            file.OpenReadStream().Returns(memoryStreamFile);
            file.FileName.Returns(fileName);

            announcementDto = new AnnouncementDto
            {
                IdAnnouncement = 0,
                Description = "Auncio prueba",
                DescriptionCallToAction = "",
                ImageId = "",
                Order = 0,
                State = true,
                ThumbnailImageId = "",
                Title = "Auncio prueba",
                UrlCallToAction = "",
                UrlImage = "",
                UrlThumbnailImage = ""
            };

            announcement = new Announcement
            {
                IdAnnouncement = 0,
                Description = "Auncio prueba",
                DescriptionCallToAction = "",
                ImageId = "",
                Order = 0,
                State = true,
                ThumbnailImageId = "",
                Title = "Auncio prueba",
                UrlCallToAction = "",
                UrlImage = "",
                UrlThumbnailImage = ""
            };
        }

        [TestMethod]
        public void CreateAnnouncementAsync_GivenAnnouncementAndFileValid()
        {
            //Arrange
            int processed = 0;
            var bytes = new byte[2540];
            string urlBlob = $"http://blobstorage.com/{file.FileName}";

            fileProcesingService.ValidateFileExtension(file).Returns(true);
            fileProcesingService.ValidateFileSize(file).Returns(true);
            fileProcesingService.ValidateImageDimenensions(file).Returns(true);
            fileProcesingService.GetFileName(file).Returns(file.FileName);
            fileProcesingService.GetFileByteArrayFromStreamImage(Arg.Any<MemoryStream>()).Returns(bytes);

            blobStorageService.SaveFileAsync(Arg.Any<UploadFile>()).Returns(urlBlob);
            blobStorageService.When(fx => fx.SaveFileAsync(Arg.Any<UploadFile>())).Do(fx => ++processed);

            //Act
            var announcementSpectedTask = announcementService.CreateAnnouncementAsync(announcementDto, file);
            announcementSpectedTask.Wait();

            //Assert
            Assert.IsTrue(announcementSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.AreEqual(2, processed);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFileExtensionException))]
        public async Task CreateAnnouncementAsync_GivenAnnouncementAndFileExtensionNotValid()
        {
            //Arrange
            fileProcesingService.ValidateFileExtension(Arg.Any<IFormFile>()).Returns(false);

            //Act
            await announcementService.CreateAnnouncementAsync(announcementDto, file).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFileSizeException))]
        public async Task CreateAnnouncementAsync_GivenAnnouncementAndFileSizeNotValid()
        {
            //Arrange
            fileProcesingService.ValidateFileExtension(Arg.Any<IFormFile>()).Returns(true);
            fileProcesingService.ValidateFileSize(Arg.Any<IFormFile>()).Returns(false);

            //Act
            await announcementService.CreateAnnouncementAsync(announcementDto, file).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFileDimensionException))]
        public async Task CreateAnnouncementAsync_GivenAnnouncementAndImageDimensionsNotValid()
        {
            //Arrange
            fileProcesingService.ValidateFileExtension(Arg.Any<IFormFile>()).Returns(true);
            fileProcesingService.ValidateFileSize(Arg.Any<IFormFile>()).Returns(true);
            fileProcesingService.ValidateImageDimenensions(Arg.Any<IFormFile>()).Returns(false);

            //Act
            await announcementService.CreateAnnouncementAsync(announcementDto, file).ConfigureAwait(false);
        }

        [TestMethod]
        public void UpdateAnnouncementAsync_GivenAnnouncementAndFileValid()
        {
            //Arrange
            int processedSave = 0;
            int processedDelete = 0;
            var bytes = new byte[2540];
            string urlBlob = $"http://blobstorage.com/{file.FileName}";

            fileProcesingService.ValidateFileExtension(file).Returns(true);
            fileProcesingService.ValidateFileSize(file).Returns(true);
            fileProcesingService.ValidateImageDimenensions(file).Returns(true);
            fileProcesingService.GetFileName(file).Returns(file.FileName);
            fileProcesingService.GetFileByteArrayFromStreamImage(Arg.Any<MemoryStream>()).Returns(bytes);

            blobStorageService.DeleteFileAsync(Arg.Any<string>()).Returns(true);
            blobStorageService.When(fx => fx.DeleteFileAsync(Arg.Any<string>())).Do(fx => ++processedDelete);

            blobStorageService.SaveFileAsync(Arg.Any<UploadFile>()).Returns(urlBlob);
            blobStorageService.When(fx => fx.SaveFileAsync(Arg.Any<UploadFile>())).Do(fx => ++processedSave);

            //Act
            var announcementSpectedTask = announcementService.UpdateAnnouncementAsync(announcementDto, file);
            announcementSpectedTask.Wait();

            //Assert
            Assert.IsTrue(announcementSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.AreEqual(2, processedSave);
            Assert.AreEqual(2, processedDelete);
        }

        [TestMethod]
        public void UpdateAnnouncementStateAsync_GivenIdValidAndState()
        {
            //Arrange
            int id = 20;
            bool state = false;
            int processed = 0;

            announcementRepository.FindById(id).Returns(announcement);
            announcementRepository.When(fx => fx.FindById(Arg.Any<int>())).Do(fx => ++processed);

            //Act
            var announcementSpectedTask = announcementService.UpdateAnnouncementStateAsync(id, state);
            announcementSpectedTask.Wait();

            //Assert
            Assert.IsTrue(announcementSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.AreEqual(1, processed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UpdateAnnouncementStateAsync_GivenIdInValidAndState()
        {
            //Arrange
            int id = 100;
            bool state = true;

            announcement = null;
            announcementRepository.FindById(id).Returns(announcement);

            //Act
            await announcementService.UpdateAnnouncementStateAsync(id, state).ConfigureAwait(false);
        }


        [TestMethod]
        public void DeleteAnnouncementAsync_GivenId()
        {
            //Arrange
            int id = 20;
            int processed = 0;

            announcementRepository.FindById(id).Returns(announcement);
            announcementRepository.When(fx => fx.FindById(Arg.Any<int>())).Do(fx => ++processed);

            //Act
            var announcementSpectedTask = announcementService.DeleteAnnouncementAsync(id);
            announcementSpectedTask.Wait();

            //Assert
            Assert.IsTrue(announcementSpectedTask.Status == TaskStatus.RanToCompletion);
            Assert.AreEqual(1, processed);
        }
    }
}
