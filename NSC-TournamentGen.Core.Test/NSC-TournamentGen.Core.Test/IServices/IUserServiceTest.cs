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
            Mock<IUserService> serviceMock = null;

            Assert.Throws<NullReferenceException>(() => { return serviceMock.Object; });
        }

        [Fact]
        public void IUserService_GetAllUsers()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock
                .Setup(sevice => sevice.GetAllUsers())
                .Returns(new List<User>());
            Assert.NotNull(serviceMock.Object.GetAllUsers());

        }

        [Fact]
        public void IUserService_CreateUser_NotNull()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(x => x.CreateUser("Test", "password")).Returns(new User());
            var createdUser = serviceMock.Object.CreateUser("Test", "password");
            Assert.NotNull(createdUser);
        }

        [Fact]
        public void IUserService_GetUser_NotNull()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(sevice => sevice.GetUser(1)).Returns(new User());
            var foundUser = serviceMock.Object.GetUser(1);

            Assert.NotNull(foundUser);
        }

        [Fact]
        public void IUserService_DeleteUser_NotNull()
        {
            var serviceMock = new Mock<IUserService>();
            serviceMock.Setup(sevice => sevice.DeleteUser(1)).Returns(new User());
            var deletedUser = serviceMock.Object.DeleteUser(1);
            Assert.NotNull(deletedUser);
        }

        [Fact]
        public void IUserService_UpdateUser_NotNull()
        {
            var serviceMock = new Mock<IUserService>();
            var replacement = new User { Username = "Test", Password = "password" };
            serviceMock.Setup(sevice => sevice.UpdateUser(1, replacement)).Returns(new User());
            var updatedUser = serviceMock.Object.UpdateUser(1, replacement);
            Assert.NotNull(updatedUser);
        }
    }
}

