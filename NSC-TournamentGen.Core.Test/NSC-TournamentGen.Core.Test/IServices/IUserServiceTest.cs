using System;
using System.Collections.Generic;
using Moq;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using Xunit;

namespace NSC_TournamentGen.Core.Test.IServices
{
    public class IUserServiceTest
    {
        [Fact]
        public void IUserService_Exists()
        {
            var serviceMock = new Mock<IUserService>();
            Assert.NotNull(serviceMock.Object);
        }

        [Fact]
        public void IUserService_NullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                Mock<IUserService> mock = null;
                return mock.Object;
            });
        }

        [Fact]
        public void IUserService_GetAllUsers()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock
                .Setup(s => s.GetAllUsers())
                .Returns(new List<User>());
            Assert.NotNull(serviceMock.Object.GetAllUsers());
        }
    }
}