namespace ALEXforums.Data.Entity
{
    public class ForumPostComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime PublishDate { get; set; }

        // Nav property
        public User User { get; set; } = null!;
        public ForumPost Post { get; set; } = null!;
    }
}
