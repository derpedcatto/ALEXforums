using ALEXforums.Data.Entity;

namespace ALEXforums.Models.Home
{
    public class HomeViewModel
    {
		public CategoriesViewModel CategoriesVM { get; set; } = null!;
		public ForumPostsViewModel ForumPostsVM { get; set; } = null!;
	}
}
