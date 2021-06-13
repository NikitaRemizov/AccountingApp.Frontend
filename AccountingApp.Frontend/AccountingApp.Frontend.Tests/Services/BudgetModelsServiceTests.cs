using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Models;
using Moq;
using System;
using Xunit;

namespace AccountingApp.Frontend.Tests.Services
{
    public abstract class BudgetModelsServiceTests<TServiceModel, TDataAccessModel> : 
        ServiceTests<IRepository<TDataAccessModel>, BudgetModelsService<TServiceModel, TDataAccessModel>> 
        where TServiceModel : BudgetModel, new() where TDataAccessModel : Shared.Models.BudgetModel
    {

        [Fact]
        public void Delete_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => DataAccessResult.Error);

            Service.Delete(Guid.Empty).Wait();

            RepositoryMock
                .Verify(r => r.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Delete_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => DataAccessResult.Error);

            var result = Service.Delete(Guid.Empty).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Delete_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => DataAccessResult.ServerUnreachable);

            var result = Service.Delete(Guid.Empty).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Delete_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => DataAccessResult.Unauthorized);

            var result = Service.Delete(Guid.Empty).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void Delete_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(() => DataAccessResult.Ok);

            var result = Service.Delete(Guid.Empty).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void Update_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => DataAccessResult.Error);

            Service.Update(new TServiceModel()).Wait();

            RepositoryMock
                .Verify(r => r.Update(It.IsAny<TDataAccessModel>()), Times.Once);
        }

        [Fact]
        public void Update_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => DataAccessResult.Error);

            var result = Service.Update(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Update_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => DataAccessResult.ServerUnreachable);

            var result = Service.Update(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Update_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => DataAccessResult.Unauthorized);

            var result = Service.Update(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void Update_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.Update(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => DataAccessResult.Ok);

            var result = Service.Update(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void Create_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (Guid.Empty, DataAccessResult.Error));

            Service.Create(new TServiceModel()).Wait();

            RepositoryMock
                .Verify(r => r.Create(It.IsAny<TDataAccessModel>()), Times.Once);
        }

        [Fact]
        public void Create_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (Guid.Empty, DataAccessResult.Error));

            var (_, result) = Service.Create(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Create_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (Guid.Empty, DataAccessResult.ServerUnreachable));

            var (_, result) = Service.Create(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void Create_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (Guid.Empty, DataAccessResult.Unauthorized));

            var (_, result) = Service.Create(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void Create_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (Guid.Empty, DataAccessResult.Ok));

            var (_, result) = Service.Create(new TServiceModel()).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void Create_RepositoryReturnsOk_ReturnsItemId()
        {
            var expectedId = Guid.NewGuid();
            RepositoryMock
                .Setup(r => r.Create(It.IsAny<TDataAccessModel>()))
                .ReturnsAsync(() => (expectedId, DataAccessResult.Ok));

            var (id, _) = Service.Create(new TServiceModel()).Result;

            Assert.Equal(expectedId, id);
        }
    }
}
