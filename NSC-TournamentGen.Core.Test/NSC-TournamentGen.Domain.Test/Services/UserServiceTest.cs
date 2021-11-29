using Moq;
using NSC_TournamentGen.Domain.IRepositories;
using NSC_TournamentGen.Domain.Services;
using Xunit;

namespace NSC_TournamentGen.Domain.Test.Services
{
    public class UserServiceTest
    {
        [Fact]
        public void UserService_GetAll()
        {
            var mockRepo = new Mock<IUserRepository>();
            var userService = new UserService(mockRepo.Object);

            userService.GetAllUsers();
            mockRepo.Verify(x=> x.ReadAll(), Times.AtLeastOnce);
        }
    }
}