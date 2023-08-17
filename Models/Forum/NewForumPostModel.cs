using Microsoft.AspNetCore.Mvc;

namespace ALEXforums.Models.Forum
{
    public class NewForumPostModel
    {
        [FromForm(Name = "newpost-category")]
        public string Category { get; set; } = null!;

        [FromForm(Name = "newpost-name")]
        public string Name { get; set; } = null!;

        [FromForm(Name = "newpost-bodytext")]
        public string BodyText { get; set; } = null!;

        [FromForm(Name = "newpost-attachment")]
        public IFormFile Attachment { get; set; } = null!;
    }
}
