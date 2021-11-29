using Moq;
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
            
            mockrepo.Verify( r => r.ReadAll(), Times.Once);
        }
        
        
        
    }
}