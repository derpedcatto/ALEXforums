using Microsoft.AspNetCore.Mvc;

namespace ALEXforums.Models.User
{
    public class UserSignUpModel
    {
        [FromForm(Name = "user-username")]
        public string Username { get; set; } = null!;

        [FromForm(Name = "user-password")]
        public string Password { get; set; } = null!;

        [FromForm(Name = "user-confirmPassword")]
        public string PasswordRepeat { get; set; } = null!;

        [FromForm(Name = "user-avatar")]
        public IFormFile AvatarFile { get; set; } = null!;
    }
}
