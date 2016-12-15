namespace Org.Feeder.Models
{
    public struct PostSummary
    {
        public PostSummary(int postId, string title)
        {
            PostId = postId;
            Title = title;
        }

        public int PostId { get; private set; }
        public string Title { get; private set; }
    }
}