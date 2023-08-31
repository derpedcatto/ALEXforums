using ALEXforums.Data;
using ALEXforums.Data.Entity;
using ALEXforums.Models;
using ALEXforums.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Web;
using System.Security.Claims;
using ALEXforums.Services.UriOperations;
using ALEXforums.Services.Hash;

namespace ALEXforums.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
		private readonly DataContext _dataContext;
		private readonly IHashService _hashService;
		private readonly IUriOperations _uriOperations;

		public HomeController(DataContext dataContext, IHashService hashService, IUriOperations uriOperations)
		{
			_dataContext = dataContext;
			_hashService = hashService;
			_uriOperations = uriOperations;
		}


        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
			return RedirectToAction(_uriOperations.EncodeUri(_dataContext.ForumCategories.FirstOrDefault()!.Name));
        }


        [HttpGet]
        [Route("{category}")]
        public IActionResult Category(string category)
		{
			HomeViewModel viewModel = new();
            string decodedCategory = _uriOperations.DecodeUri(category);

			CategoriesViewModel categoriesVM = new();
			categoriesVM.Categories = _dataContext.ForumCategories.ToList();
			categoriesVM.ChosenCategory = _dataContext.ForumCategories.First(c => c.Name == decodedCategory);

            ForumPostsViewModel postsVM = new();
            postsVM.ForumPosts = _dataContext.ForumPosts.Include(fp => fp.User)
                                                        .Include(fp => fp.Category)
                                                        .Include(fp => fp.Comments)
                                                        .Where(c => c.CategoryId == categoriesVM.ChosenCategory.Id)
                                                        .OrderByDescending(p => p.PublishDate)
                                                        .ToList();

            viewModel.CategoriesVM = categoriesVM;
            viewModel.ForumPostsVM = postsVM;

			return View("Index", viewModel);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/NewPost")]
        public IActionResult NewPost([FromForm]NewForumPostModel? model)
        {
            string decodedCategory = _uriOperations.DecodeUri(model!.Category);

            ForumPost newPost = new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid).Value),
                CategoryId = _dataContext.ForumCategories.Where(c => c.Name == decodedCategory)
                                                         .First()
                                                         .Id,
                Title = model.Name,
                BodyText = model.BodyText,
                PublishDate = DateTime.Now,
            };
            if (model.Attachment == null)
            {
                model.Attachment = null!;
            }
            else
            {
                string ext = Path.GetExtension(model.Attachment.FileName);
                string newName = Guid.NewGuid().ToString() + ext;
                model.Attachment.CopyTo(new FileStream($"wwwroot/uploads/{newName}", FileMode.Create));
                newPost.PictureAttachment = newName;
            }

            _dataContext.ForumPosts.Add(newPost);
            _dataContext.SaveChanges();

            return RedirectToAction(model.Category);
        }




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}