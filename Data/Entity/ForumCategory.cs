namespace ALEXforums.Data.Entity
{
    public class ForumCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        // Nav property
        public ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
    }
}
