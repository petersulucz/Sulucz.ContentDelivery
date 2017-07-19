namespace Sulucz.ContentDelivery.Data.Sql.Internal
{
    /// <summary>
    /// The post tag.
    /// </summary>
    internal sealed class PostTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTag"/> class.
        /// </summary>
        /// <param name="postid">The post id.</param>
        /// <param name="tag">The tag.</param>
        public PostTag(int postid, string tag)
        {
            this.PostId = postid;
            this.Tag = tag;
        }

        /// <summary>
        /// Gets the post id.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; }
    }
}
