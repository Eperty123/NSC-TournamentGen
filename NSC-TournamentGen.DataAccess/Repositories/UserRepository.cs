using System.Collections.Generic;
using System.Linq;
using NSC_TournamentGen.Core.Models;
using NSC_TournamentGen.Domain.IRepositories;

namespace NSC_TournamentGen.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainDbContext _ctx;

        public UserRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public User CreateUser(string username, string password)
        {
            // Create the user only if username and password are not empty.
            // We can't find a user without a username after all.
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Create the user in the database.
                _ctx.Users.Add(new Entities.UserEntity { Username = username, Password = password });
                _ctx.SaveChanges();

                // Return a new instance of the created user in User form.
                return new User { Username = username, Password = password };
            }
            return null;
        }

        public User ReadUser(int id)
        {
            return _ctx.Users.Select(u => new User
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<User> ReadAll()
        {
            return _ctx.Users.Select(u => new User
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password
            }).ToList();
        }

        public User DeleteUser(int id)
        {
            var foundUser = _ctx.Users.FirstOrDefault(x => x.Id == id);

            if (foundUser != null)
            {
                // Remove from the database.
                _ctx.Users.Remove(foundUser);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* User instance from the found user.
                return new User { Id = foundUser.Id, Username = foundUser.Username, Password = foundUser.Password };
            }

            // None found, return null.
            return null;
        }

        public User UpdateUser(int id, User user)
        {
            var foundUser = _ctx.Users.FirstOrDefault(x => x.Id == id);

            if (foundUser != null)
            {
                // Make changes to the found user.
                foundUser.Username = user.Username;
                foundUser.Password = user.Password;

                // Update the found user in the database.
                _ctx.Users.Update(foundUser);

                // Save changes to the database.
                _ctx.SaveChanges();

                // Return a *new* User instance from the updated user.
                return new User { Id = foundUser.Id, Username = foundUser.Username, Password = foundUser.Password };
            }

            // None found, return null.
            return null;
        }
    }
}