using Microsoft.AspNetCore.Mvc;

namespace ALEXforums.Models.User
{
    public class UserSignInModel
    {
        [FromForm(Name = "user-username")]
        public string Username { get; set; } = null!;

        [FromForm(Name = "user-password")]
        public string Password { get; set; } = null!;
    }
}
