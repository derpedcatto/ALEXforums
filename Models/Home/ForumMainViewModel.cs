using ALEXforums.Models.Forum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ALEXforums.Models.Home
{
    public class ForumMainViewModel
    {
        public List<SelectListItem> PostCategories { get; set; } = null!;

        public NewForumPostModel NewForumPostModel { get; set; } = null!;
    }
}
