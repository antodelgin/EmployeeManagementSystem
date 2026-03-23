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

        [HttpPost("validate")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var dbUser = await authService.ValidateUser(model.Email, model.Password);

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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out successfully" });
        }


        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
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

            var result = await authService.Create(user);

            var responseDto = new SignUpDto
            {
                Email = result.Email,
                Password = result.Password,
                Role = result.Role.ToString(),
                EmployeeId = result.EmployeeId,
                IsActive = result.IsActive
            };

            return Ok(responseDto);

            //return Ok(new
            //{
            //    message = "User created successfully",
            //    data = responseDto
            //});
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
    }
}