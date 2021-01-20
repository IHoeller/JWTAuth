using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Shared.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
