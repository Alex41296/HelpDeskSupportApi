using System.Threading.Tasks;
using WebAPILab.Helpers;
using WebAPILab.Models;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

namespace WebAPILab.Controllers
{
     [Authorize]
    public class UserController : ApiController
    {
        private IUserService _userService;
     
        public UserController()
        {
            _userService = new UserService(); 
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> PostAuthenticate([FromBody]UserModel userParam)
        {
            var user = await _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }



        [AllowAnonymous]
        [Route("api/user/support/")]
        public IHttpActionResult GetAllSupports()
        {
            IList<UserModel> user = null;
            IList<UsersRoleModel> usersRole = null;


            using (var context = new GARSupport2020Entities())
            {
                usersRole = context.User_Role
                    .Where(usersRoleItem => usersRoleItem.id_role == 2)
                    .Select(usersRoleItem => new UsersRoleModel()
                    {
                        Id_Users = usersRoleItem.id_users
                    }).ToList<UsersRoleModel>();
            }

            using (var context = new GARSupport2020Entities())
            {
                foreach (var id_users in usersRole)
                {

                    user = context.Users
                   .Where(userItem => userItem.id == id_users.Id_Users)
                  .Select(userItem => new UserModel()
                  {
                      Id = userItem.id,
                      Name = userItem.name,
                      SecondName = userItem.second_name,
                      FirstName = userItem.first_name,
                      Address = userItem.address,
                      Phone = userItem.phone,
                      SecondContact = userItem.second_contact,
                      Email = userItem.email,
                      Password = userItem.password

                  }).ToList<UserModel>();

                }
              
            }
            if (user.Count == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [Route("api/user/all/")]
        public IHttpActionResult GetAllUsers()
        {
            IList<UserModel> user = null;
            

            using (var context = new GARSupport2020Entities())
            {
                    user = context.Users
                  .Select(userItem => new UserModel()
                  {
                      Id = userItem.id,
                      Name = userItem.name,
                      SecondName = userItem.second_name,
                      FirstName = userItem.first_name,
                      Address = userItem.address,
                      Phone = userItem.phone,
                      SecondContact = userItem.second_contact,
                      Email = userItem.email,
                      Password = userItem.password

                  }).ToList<UserModel>();

                }
            
            if (user.Count == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}