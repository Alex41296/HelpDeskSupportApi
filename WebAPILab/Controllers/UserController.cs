using System.Threading.Tasks;
using WebAPILab.Helpers;
using WebAPILab.Models;
using System.Web.Http;

namespace WebAPILab.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserService _userService;

        public UsersController()
        {
            _userService = new UserService();
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> PostAuthenticate([System.Web.Http.FromBody]UserModel userParam)
        {
            var user = await _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        public IHttpActionResult Post(UserModel userCreate)
        {
            using (var context = new GARSupport2020Entities())
            {
                context.Users.Add(new Users()
                {
                    id = userCreate.Id,
                    name = userCreate.Name,
                    second_name = userCreate.SecondName,
                    first_name = userCreate.FirstName,
                    address = userCreate.Address,
                    phone = userCreate.Phone,
                    second_contact = userCreate.SecondContact,
                    email = userCreate.Email,
                    password = userCreate.Password,

                });
                context.SaveChanges();
            }
            return Ok();
        }
    }
}