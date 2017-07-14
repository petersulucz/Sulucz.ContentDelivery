namespace Sulucz.ContentDelivery.Data.Models
{
    /// <summary>
    /// The sulucz post content.
    /// </summary>
    public class SuluczPostContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuluczPostContent"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentType">The content type.</param>
        /// <param name="revision">The revision.</param>
        public SuluczPostContent(string content, SuluczContentType contentType, int revision)
        {
            this.Revision = revision;
            this.Content = content;
            this.ContentType = contentType;
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        public int Revision { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the content type.
        /// </summary>
        public SuluczContentType ContentType { get; }
    }
}
