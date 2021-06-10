using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public abstract class BudgetModelsTests<T> : WebApiRepositoryTests<T> where T : BudgetModel, new()
    {
        protected abstract override BudgetModels<T> Repository { get; }

        [Fact]
        public void Delete_CallMethod_ClientDeleteCalledOnce()
        {
            ClientMock
                .Setup(c => c.Delete(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(() => AccountingApiResult.Ok);

            Repository.Delete(Guid.NewGuid()).Wait();

            ClientMock.Verify(
                c => c.Delete(It.IsAny<string>(), It.IsAny<Guid>()),
                Times.Once
            );
        }

        [Fact]
        public void Update_CallMethod_ClientUpdateCalledOnce()
        {
            ClientMock
                .Setup(c => c.Update(It.IsAny<string>(), It.IsAny<T>()))
                .ReturnsAsync(() => AccountingApiResult.Ok);

            Repository.Update(new T()).Wait();

            ClientMock.Verify(
                c => c.Update(It.IsAny<string>(), It.IsAny<T>()),
                Times.Once
            );
        }

        [Fact]
        public void Create_CallMethod_ClientCreateCalledOnce()
        {
            ClientMock
                .Setup(c => c.Post<T>(It.IsAny<string>(), It.IsAny<T>()))
                .ReturnsAsync(() => (new T(),AccountingApiResult.Ok));

            Repository.Create(new T()).Wait();

            ClientMock.Verify(
                c => c.Post<T>(It.IsAny<string>(), It.IsAny<T>()),
                Times.Once
            );
        }

        [Fact]
        public void Create_ModelIsCreatedByWebApi_ReturnsItsId()
        {
            var model = new T();
            model.Id = Guid.NewGuid();
            ClientMock
                .Setup(c => c.Post<T>(It.IsAny<string>(), It.IsAny<T>()))
                .ReturnsAsync(() => (model, AccountingApiResult.Ok));

            var (id, _) = Repository.Create(model).Result;

            Assert.Equal(model.Id, id);
        }

        [Fact]
        public void Create_ModelIsNotCreatedByWebApi_ReturnsGuidEmpty()
        {
            var model = new T();
            model.Id = Guid.NewGuid();
            ClientMock
                .Setup(c => c.Post<T>(It.IsAny<string>(), It.IsAny<T>()))
                .ReturnsAsync(() => (null, AccountingApiResult.Error));

            var (id, _) = Repository.Create(model).Result;

            Assert.Equal(Guid.Empty, id);
        }

        protected virtual void SetupMockGet()
        {
            ClientMock
                .Setup(c => c.Get<List<T>>(It.IsAny<string>()))
                .ReturnsAsync(() => (new List<T>(), AccountingApiResult.Ok));
        }

        protected virtual void MockVerifyGetCalledOnce()
        {
            ClientMock.Verify(
                c => c.Get<List<T>>(It.IsAny<string>()),
                Times.Once
            );
        }
    }
}
