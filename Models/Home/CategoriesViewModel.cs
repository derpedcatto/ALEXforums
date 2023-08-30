using ALEXforums.Data.Entity;

namespace ALEXforums.Models.Home
{
	public class CategoriesViewModel
	{
		public List<ForumCategory> Categories { get; set; } = null!;
		public ForumCategory ChosenCategory { get; set; } = null!;
	}
}
