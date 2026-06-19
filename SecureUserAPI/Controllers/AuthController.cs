using Microsoft.AspNetCore.Mvc;
using SecureUserAPI.models;
using SecureUserAPI.services;
using SecureUserAPI.data;

namespace SecureUserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly DataContext _context;

        
        public AuthController(AuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("register")] 
        public IActionResult Register(UserRegisterDto request)
        {
           
            var user = _authService.Register(request);

            
            _context.Users.Add(user);
            _context.SaveChanges(); // Aquí se guarda físicamente en SQL Server

            return Ok(new { message = "¡Usuario registrado con éxito y clave encriptada! 🔥" });
        }
    }
}