using Microsoft.AspNetCore.Mvc;
// 关键：添加 Data 命名空间引用
using TestProject.Data;
using TestProject.Models;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db) => _db = db;

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return BadRequest("用户名和密码不能为空");

            if (_db.Users.Any(u => u.UserName == user.UserName))
                return Conflict("用户名已被注册");

            _db.Users.Add(user);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user == null || user.Password != request.Password)
                return Unauthorized("用户名或密码错误");

            return Ok(new { user.Id, user.UserName, Role = "User" });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}