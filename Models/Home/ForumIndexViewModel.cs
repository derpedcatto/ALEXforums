using ALEXforums.Models.Forum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ALEXforums.Models.Home
{
    public class ForumIndexViewModel
    {
        public List<SelectListItem> PostCategories { get; set; } = null!;

        public NewForumPostModel NewForumPostModel { get; set; } = null!;
    }
}
