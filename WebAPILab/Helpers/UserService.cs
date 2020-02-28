using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
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
        public IList<UserModel> loadUsers()
        {
            IList<UserModel> _users = null;

            using (var context = new GARSupport2020Entities())
            {
                _users = context.Users
                    .Select(userItem => new UserModel()
                    {
                        Id = userItem.id,
                        Name = userItem.name,
                        FirstName = userItem.first_name,
                        SecondName = userItem.second_name,
                        Address = userItem.address,
                        Phone = userItem.phone,
                        SecondContact = userItem.second_contact,
                        Email = userItem.email,
                        Password = userItem.password

                    }).ToList<UserModel>();
                if (_users.Count == 0)
                {
                    return null;
                }
                return _users;
            }
        }
        
        //private List<UserModel> _users = new List<UserModel>
        //{
        //};

        public async Task<UserModel> Authenticate(string email, string password)
        {
            var user = await Task.Run(() => loadUsers().SingleOrDefault(x => x.Email == email && x.Password == password));

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
            return await Task.Run(() => loadUsers().Select(x => {
                x.Password = null;
                return x;
            }));
        }
    }
}