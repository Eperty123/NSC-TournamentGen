using System.Collections.Generic;
using NSC_TournamentGen.Core.IServices;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;

namespace NSC_TournamentGen.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.ReadAll();
        }

        public User CreateUser(string username, string password)
        {
            return _userRepository.CreateUser(username, password);
        }

        public User GetUser(int id)
        {
            return _userRepository.ReadUser(id);
        }

        public User DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public User UpdateUser(int id, User user)
        {
            return _userRepository.UpdateUser(id, user);
        }
    }
}