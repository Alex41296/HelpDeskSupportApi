using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebAPILab.Models;

namespace WebAPILab.Helpers
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string username, string password);
        Task<IEnumerable<UserModel>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel { Id = 1, Name = "Rodri", FirstName = "Alejandro", SecondName = "Soto", Address = "50m oeste escuela Sta Lucía",
                Phone= "98769485", SecondContact="None", Email="rodrigoalesm@gmail.com", Password="rodri123" }
        };

        public async Task<UserModel> Authenticate(string email, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Email == email && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            // return users without passwords
            return await Task.Run(() => _users.Select(x => {
                x.Password = null;
                return x;
            }));
        }
    }
}