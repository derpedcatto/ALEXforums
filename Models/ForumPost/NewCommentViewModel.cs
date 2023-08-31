using Microsoft.AspNetCore.Mvc;

namespace ALEXforums.Models.ForumPost
{
    public class NewCommentViewModel
    {
        [FromForm(Name = "newcomment-text")]
        public string CommentText { get; set; } = String.Empty;
    }
}
