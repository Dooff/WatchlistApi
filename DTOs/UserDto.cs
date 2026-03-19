using System.ComponentModel.DataAnnotations;

namespace WatchlistApi.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}