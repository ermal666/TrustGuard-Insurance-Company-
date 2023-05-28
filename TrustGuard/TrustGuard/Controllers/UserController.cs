using Microsoft.AspNetCore.Mvc;
using TrustGuard.Domain.Models;
using TrustGuard.Service.IServices;

namespace TrustGuard.Application.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageIndex = 1, int pageSize = 20)
        {
            var users = await _userService.GetUsersPaged(pageIndex, pageSize);
            if (users is null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var isCreated = await _userService.CreateUser(user);
            if (isCreated)
            {
                return Ok(user);
            }
            return BadRequest();
        }

    }
}
