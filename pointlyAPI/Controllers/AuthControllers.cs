using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pointly.Data;
using Pointly.DTOs;
using Pointly.Helpers;
using BCrypt.Net;

namespace Pointly.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtHelper = new JwtHelper(configuration);
        }

        /// <summary>
        /// Admin Login
        /// POST: api/auth/login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            // Find user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Only Admin can login for now
            if (user.Role != Models.UserRole.Admin)
            {
                return Unauthorized(new { message = "Access denied. Admin only." });
            }

            // Check if user is active
            if (!user.IsActive)
            {
                return Unauthorized(new { message = "Account is inactive" });
            }

            // Generate JWT token
            var token = _jwtHelper.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString()
            });
        }
    }
}