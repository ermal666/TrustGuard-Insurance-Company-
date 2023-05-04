using Microsoft.AspNetCore.Mvc;

namespace PropertyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            if(users is null)
            {
                return NotFound();
            }
            return Ok(users);
        }

    }
}
