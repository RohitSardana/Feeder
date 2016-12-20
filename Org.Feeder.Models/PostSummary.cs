namespace Org.Feeder.Models
{
    /// <summary>
    /// Represents summary of a post such as id and title
    /// </summary>
    public struct PostSummary
    {
        /// <summary>
        /// Initializes PostSummary with PostId and Title
        /// </summary>
        /// <param name="postId">The post id</param>
        /// <param name="title">Title of the post</param>
        public PostSummary(int postId, string title)
        {
            PostId = postId;
            Title = title;
        }

        /// <summary>
        /// Gets the id of the post
        /// </summary>
        public int PostId { get; private set; }

        /// <summary>
        /// Gets the title of the post
        /// </summary>
        public string Title { get; private set; }
    }
}