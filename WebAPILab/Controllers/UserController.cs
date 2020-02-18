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
            var user = await _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
    }
}