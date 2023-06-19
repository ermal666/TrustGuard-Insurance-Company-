using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TrustGuard.Application.Dtos;
using TrustGuard.Domain.Models;
using TrustGuard.Service.IServices;

namespace TrustGuard.Application.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

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
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {

            if (createUserDto == null)
                return BadRequest();

            var user = _mapper.Map<User>(createUserDto);
            var isCreated = await _userService.CreateUser(user);
            if (isCreated)
            {
                return Ok(user);
            }
            return BadRequest();
        }

    }
}
