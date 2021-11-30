using System.Collections.Generic;
using NSC_TournamentGen.Core.Models;

namespace NSC_TournamentGen.Core.IServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User CreateUser(string username, string password);
        User GetUser(int id);
        User DeleteUser(int id);
        object UpdateUser(int id, User user);
    }
}