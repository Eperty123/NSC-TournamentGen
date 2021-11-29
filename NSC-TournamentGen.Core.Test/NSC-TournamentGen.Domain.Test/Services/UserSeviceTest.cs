using NSC_TournamentGen.Core.Test.IServices;
using NSC_TournamentGen.Domain.Test.IRepositories;

namespace NSC_TournamentGen.Domain.Test.Services
{
    public class UserSeviceTest : IUserServiceTest
    {
        private readonly IUserRepositoryTest _userRepository;


        public UserSeviceTest(IUserRepositoryTest userRepository)
        {
            _userRepository = userRepository;
        }
        
        
        
    }
}