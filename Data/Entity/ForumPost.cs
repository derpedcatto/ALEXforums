namespace ALEXforums.Data.Entity
{
    public class ForumPost
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? BodyText { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? PictureAttachment { get; set; } = null!;

        // Nav property
        public User User { get; set; } = null!;
        public ForumCategory Category { get; set; } = null!;
        public ICollection<ForumPostComment> Comments { get; set; } = new List<ForumPostComment>();
    }
}
