namespace ALEXforums.Data.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateTime RegisterDate { get; set; }

        // Nav property
        public ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
        public ICollection<ForumPostComment> ForumPostComments { get; set; } = new List<ForumPostComment>();
    }
}
