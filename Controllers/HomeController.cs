using ALEXforums.Data;
using ALEXforums.Data.Entity;
using ALEXforums.Models;
using ALEXforums.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Web;
using ALEXforums.Utility.UriOperations;
using System.Security.Claims;

namespace ALEXforums.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [Route("/")]
		public IActionResult Index()
        {
			return RedirectToAction(UriOperationsCyrillic.EncodeUri(_dataContext.ForumCategories.FirstOrDefault()!.Name));
        }

		[Route("{category}")]
		public IActionResult Category(string category)
		{
			HomeViewModel viewModel = new();
            string decodedCategory = HttpUtility.UrlDecode(category);

			CategoriesViewModel categoriesVM = new();
			categoriesVM.Categories = _dataContext.ForumCategories.ToList();
			categoriesVM.ChosenCategory = _dataContext.ForumCategories.First(c => c.Name == decodedCategory);

            ForumPostsViewModel postsVM = new();
            postsVM.ForumPosts = _dataContext.ForumPosts.Include(fp => fp.User)
                                                        .Where(c => c.CategoryId == categoriesVM.ChosenCategory.Id)
                                                        .OrderByDescending(p => p.PublishDate)
                                                        .ToList();

            viewModel.CategoriesVM = categoriesVM;
            viewModel.ForumPostsVM = postsVM;

			return View("Index", viewModel);
		}

        [HttpPost]
        [Route("/NewPost")]
        public IActionResult NewPost([FromForm]NewForumPostModel? model)
        {
            string decodedCategory = HttpUtility.UrlDecode(model!.Category);

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

		/*
		public PartialViewResult Categories()
        {
            return PartialView("Categories");
        }
		
		public PartialViewResult FeedPosts()
        {
            return PartialView("FeedPosts");
        }
        */

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}