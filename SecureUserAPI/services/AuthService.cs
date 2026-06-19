using SecureUserAPI.models;
using SecureUserAPI.models;
using BCrypt.Net;

namespace SecureUserAPI.services
{
    public class AuthService
    {
        // Asegúrate de que "User" empiece con U mayúscula
        public User Register(UserRegisterDto request)
        {
            // Encriptamos la contraseña
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Creamos el objeto final
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            return user;
        }
    }
}