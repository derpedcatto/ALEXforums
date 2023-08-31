using ALEXforums.Data;
using ALEXforums.Data.Entity;
using ALEXforums.Models.User;
using ALEXforums.Services.Hash;
using ALEXforums.Services.UriOperations;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ALEXforums.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IHashService _hashService;
        private readonly IUriOperations _uriOperations;

		public UserController(DataContext dataContext, IHashService hashService, IUriOperations uriOperations)
		{
			_dataContext = dataContext;
			_hashService = hashService;
			_uriOperations = uriOperations;
		}

		public IActionResult SignUp(UserSignUpModel? model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                ViewData["formData"] = ValidateSignUpModel(model);
            }

            return View(model);
        }

        public IActionResult SignIn(UserSignInModel? model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                ViewData["formData"] = ValidateSignInModel(model);
            }

            return View(model);
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("AuthUserId");
            HttpContext.User = new System.Security.Claims.ClaimsPrincipal();

            return RedirectToAction("Index", "Home");
        }

        private string ValidateSignInModel(UserSignInModel? model)
        {
            string errorMsg = String.Empty;

            if (model == null) { return "Дані не передані!"; }

            ALEXforums.Data.Entity.User? user = _dataContext.Users.FirstOrDefault(x => x.Username == model.Username);

            if (user != null)
            {
                if (user.PasswordHash == _hashService.HashString(model.Password))
                {
                    HttpContext.Session.SetString("AuthUserId", user.Id.ToString());
                    return String.Empty;
                }
                else
                {
                    return errorMsg += "Невірний пароль.;";
                }
            }

            return errorMsg += "Користувача з цим логіном не існує.;";
        }

        private string ValidateSignUpModel(UserSignUpModel? model)
        {
            string errorMsg = String.Empty;

            if (model == null) { return "Дані не передані!"; }

            // Username
            if (string.IsNullOrEmpty(model.Username)) 
            {
                errorMsg += "Ім'я користувача порожнє.;";
            }
            if (! (model.Username.Length >= 3 && model.Username.Length <= 12))
            { 
                errorMsg += "Ім'я користувача має містити 3-12 символів.;"; 
            }
            if (! Regex.IsMatch(model.Username, "^[A-Za-zА-Яа-я][A-Za-z0-9_А-Яа-я]*$")) 
            {
                errorMsg += "Ім'я користувача повинно починатися з літери та не містити невалідних символів.;";
            }
            if (_dataContext.Users.Any(row => row.Username == model.Username))
            {
                errorMsg += "Користувач з цим логіном вже зареєстрований.;";
            }

            // Password
            if (string.IsNullOrEmpty(model.Password))
            {
                return errorMsg += "Поле паролю порожнє.;";
            }
            if (model.Password != model.PasswordRepeat)
            {
                errorMsg += "Пароль не підтверджен.;";
            }
            if (! Regex.IsMatch(model.Password, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]).{8,}$"))
            {
                errorMsg += "Пароль як мінімум має містити 8 знаків, 1 велику літеру, 1 цифру, 1 спеціальний символ.;";
            }

            // Avatar
            string? newName = null;
            if (model.AvatarFile != null)
            {
                string ext = Path.GetExtension(model.AvatarFile.FileName);
                if (ext != ".png" && ext != ".jpg")
                {
                    errorMsg += "Аватар повинен бути у форматі .png або .jpg.;";
                }

                newName = Guid.NewGuid().ToString() + ext;
                model.AvatarFile.CopyTo(new FileStream($"wwwroot/uploads/{newName}", FileMode.Create));
            }
            else
            {
                newName = "default.png";
            }


            if (errorMsg != String.Empty)
            {
                return errorMsg;
            }

            _dataContext.Users.Add(new Data.Entity.User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                PasswordHash = _hashService.HashString(model.Password),
                Avatar = newName,
                RegisterDate = DateTime.Now,
            });
            _dataContext.SaveChanges();

            return String.Empty;
        }
    }
}
