using ALEXforums.Data;
using ALEXforums.Data.Entity;
using ALEXforums.Models.ForumPost;
using ALEXforums.Services.Hash;
using ALEXforums.Services.UriOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ALEXforums.Controllers
{
    [Route("[controller]")]
    public class ForumPostController : Controller
    {
		private readonly DataContext _dataContext;
		private readonly IHashService _hashService;
		private readonly IUriOperations _uriOperations;

		public ForumPostController(DataContext dataContext, IHashService hashService, IUriOperations uriOperations)
		{
			_dataContext = dataContext;
			_hashService = hashService;
			_uriOperations = uriOperations;
		}

		[Route("{postid:guid}")]
        public IActionResult Index(string postId)
        {
            ForumPostViewModel viewModel = new();

            PostViewModel postVM = new();
            postVM.ForumPost = _dataContext.ForumPosts.Include(fp => fp.User)
                                                      .Include(fp => fp.Category)
                                                      .Include(fp => fp.Comments)
                                                      .Where(p => p.Id.ToString() == postId)
                                                      .FirstOrDefault()!;

            PostCommentsViewModel commentsVM = new();
            commentsVM.Comments = _dataContext.ForumPostComments.Include(fp => fp.User)
                                                                .Include(fp => fp.Post)
                                                                .Where(c => c.PostId.ToString() == postId)
                                                                .OrderByDescending(c => c.PublishDate)
                                                                .ToList();

            viewModel.PostVM = postVM;
            viewModel.CommentsVM = commentsVM;

            return View(viewModel);
        }

        [HttpPost]
        [Route("{postid:guid}-NewComment")]
        public IActionResult NewComment(string postid, [FromForm]NewCommentViewModel? model)
        {
            ForumPostComment newComment = new()
            { 
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.Sid).Value),
                PostId = Guid.Parse(postid),
                Text = model.CommentText,
                PublishDate = DateTime.Now
            };

            _dataContext.ForumPostComments.Add(newComment);
            _dataContext.SaveChanges();

            return RedirectToAction(postid);
        }



        /*
         * Shit Broke
         */

        [Route("{postid:guid}-ReturnToHome")]
        public IActionResult ReturnToHome(string postid)
        {
            ForumPost forumPost = _dataContext.ForumPosts.Where(p => p.Id.ToString() == postid).First();

            string category = _dataContext.ForumCategories.Where(p => p.Id == forumPost.CategoryId).First().Name;
            return RedirectToAction("Category", "Home", new { category = _uriOperations.EncodeUri(category) });
        }
    }
}
