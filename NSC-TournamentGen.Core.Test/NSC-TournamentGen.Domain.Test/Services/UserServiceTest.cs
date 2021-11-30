using Moq;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Core.Test.IServices;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using Xunit;


namespace NSC_TournamentGen.Domain.Test.Services
{
    public class UserServiceTest : IUserServiceTest
    {



        [Fact]

        public void UserService_GetAll()
        {

            var mockrepo = new Mock<IUserRepository>();
            var service = new UserService(mockrepo.Object);

            service.GetAllUsers();

            mockrepo.Verify(r => r.ReadAll(), Times.Once);
        }

        [Fact]
        public void UserService_CreateUser_NotNull()
        {
            var mockrepo = new Mock<IUserRepository>();
            var service = new UserService(mockrepo.Object);

            mockrepo.Setup(x => x.CreateUser("Test", "password")).
                Returns(new User());

            var createdUser = service.CreateUser("Test", "password");
            mockrepo.Verify(r => r.CreateUser("Test", "password"), Times.Once);
            Assert.NotNull(createdUser);
        }

        [Fact]
        public void UserService_GetUser_NotNull()
        {
            var mockrepo = new Mock<IUserRepository>();
            var service = new UserService(mockrepo.Object);

            mockrepo.Setup(x => x.ReadUser(1)).
                Returns(new User());

            var foundUser = service.GetUser(1);
            mockrepo.Verify(r => r.ReadUser(1), Times.Once);
            Assert.NotNull(foundUser);
        }

        [Fact]
        public void UserService_DeleteUser_NotNull()
        {
            var mockrepo = new Mock<IUserRepository>();
            var service = new UserService(mockrepo.Object);

            mockrepo.Setup(x => x.DeleteUser(1)).
                Returns(new User());

            var deletedUser = service.DeleteUser(1);
            mockrepo.Verify(r => r.DeleteUser(1), Times.Once);
            Assert.NotNull(deletedUser);
        }

        [Fact]
        public void UserService_UpdateUser_NotNull()
        {
            var mockrepo = new Mock<IUserRepository>();
            var service = new UserService(mockrepo.Object);
            var replacement = new User { Username = "Test", Password = "password" };

            mockrepo.Setup(x => x.UpdateUser(1, replacement)).
                Returns(new User());

            var updatedUser = service.UpdateUser(1, replacement);
            mockrepo.Verify(r => r.UpdateUser(1, replacement), Times.Once);
            Assert.NotNull(updatedUser);
        }

    }
}