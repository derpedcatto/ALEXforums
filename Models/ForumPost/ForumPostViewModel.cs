using ALEXforums.Data.Entity;

namespace ALEXforums.Models.ForumPost
{
    public class ForumPostViewModel
    {
        public PostViewModel PostVM { get; set; } = null!;
        public PostCommentsViewModel CommentsVM { get; set; } = null!;
    }
}
