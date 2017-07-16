namespace Sulucz.ContentDelivery.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The post content.
    /// </summary>
    public class PostContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostContent"/> class.
        /// </summary>
        [Obsolete]
        public PostContent()
        {
            // Empty for stupid ASP
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostContent"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public PostContent(SuluczPostContent content)
        {
            this.Content = content.Content;
            this.ContentType = content.ContentType.ToString();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Required]
        [MaxLength(2048)]
        [MinLength(5)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string ContentType { get; set; }
    }
}
