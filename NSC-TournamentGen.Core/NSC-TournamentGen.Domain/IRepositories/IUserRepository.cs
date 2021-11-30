using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Domain.IRepositories
{
    public interface IUserRepository
    {
        List<User> ReadAll();
        User CreateUser(string username, string password);
        User ReadUser(int id);
        User DeleteUser(int id);
        User UpdateUser(int id, User user);
    }
}