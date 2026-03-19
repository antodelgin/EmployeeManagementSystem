using Backend.Dto;
using Backend.Model;
using Backend.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        //[HttpPost("login")]
        //public IActionResult Login(LoginDto model)
        //{
        //    //var user = _context.Users
        //    //    .FirstOrDefault(x => x.Email == model.Email &&
        //    //                         x.Password == model.Password);

        //    //if (user == null)
        //    //    return Unauthorized();

        //    var token = GenerateToken(user);

        //    return Ok(new { token });
        //}


        [HttpPost("validate")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var dbUser = await authService.ValidateUser(model.Email, model.Password);

            //var user = _context.Users
            //    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (dbUser == null)
                return Unauthorized();

            //HttpContext.Session.SetString("UserEmail", dbUser.Email);
            //HttpContext.Session.SetString("UserRole", dbUser.Role.ToString());

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, dbUser.Email),
                    new Claim(ClaimTypes.Role, dbUser.Role.ToString())
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //return Ok("Login successful");
            return Ok(new
            {
                message = "Login successful",
                role = dbUser.Role.ToString()
            });
        }

        //[HttpGet("check")]
        //public IActionResult CheckAuth()
        //{
        //    //return Ok(new { authenticated = true });
        //    if (User?.Identity?.IsAuthenticated == true)
        //    {
        //        return Ok(new { authenticated = true });
        //    }
        //    return Unauthorized(new { authenticated = false });
        //}

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out successfully" });
        }


        //[HttpPost("login")]
        //public IActionResult LoginValidate([FromBody] LoginDto dto )
        //{
        //    //if (user.Email == "anto@test.com" && user.Password == "anto04")
        //    //    return Ok(new { message = "Login successful"});
        //    //return Unauthorized(new { message = "Invalid credentials" });
        //    Console.WriteLine($"Email: {dto?.Email}");
        //    Console.WriteLine($"Password: {dto?.Password}");

        //    //var dbUser = authService.ValidateUser(user.Email, user.Password);
        //    var dbUser = await authService.ValidateUser(dto.Email, dto.Password);
        //    //Console.WriteLine(dbUser);

        //    if (dbUser == null)
        //    {
        //        return Unauthorized(new { message = "Invalid credentials" });
        //    }
        //    else
        //    {
        //        return Ok(new { message = "Login successful" });
        //    }
        //}

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody] SignUpDto dto)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password,
                Role = Enum.Parse<UserRole>(dto.Role, true),
                EmployeeId = dto.EmployeeId,
                IsActive = dto.IsActive
            };
            var result = authService.Create(user);
            return Ok(result);

        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (User?.Identity?.IsAuthenticated != true) return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var user = await authService.GetUserById(int.Parse(userId));

            if (user == null) return NotFound();

            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                role = user.Role.ToString(),
                employeeId = user.EmployeeId,
                isActive = user.IsActive
            });


        }



        [HttpGet("current-users")]
        public async Task<IActionResult> GetCurrentUsers()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await authService.GetUserById(int.Parse(userId));

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}