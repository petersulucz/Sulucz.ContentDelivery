namespace Sulucz.ContentDelivery.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Sulucz.ContentDelivery.Common;
    using Sulucz.ContentDelivery.Data;
    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The post model.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel"/> class.
        /// </summary>
        [Obsolete]
        public PostModel()
        {
            // Empty for asp.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        public PostModel(SuluczPost post)
        {
            this.MetaData = new PostMetaData(post.MetaData);

            this.Content = ContentConverter.ConvertContent(post.Contents);

            this.RawContent = post.Contents.Select(s => new PostContent(s)).ToArray();
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        [Required]
        public PostMetaData MetaData { get; set; }

        /// <summary>
        /// Gets the raw content.
        /// </summary>
        [Required]
        public PostContent[] RawContent { get; set; }

        /// <summary>
        /// Converts this model to the backend model.
        /// </summary>
        /// <returns>A sulucz post.</returns>
        public SuluczPost ToSuluczPost()
        {
            var sulContents = new SuluczPostContent[this.RawContent.Length];
            for (var i = 0; i < sulContents.Length; i++)
            {
                sulContents[i] = new SuluczPostContent(
                    this.MetaData.Id ?? 0,
                    i,
                    this.RawContent[i].Content,
                    this.RawContent[i].ContentType.ParseEnum<SuluczContentType>(),
                    0,
                    Guid.Empty);
            }

            return new SuluczPost(this.MetaData.ToSuluczMetaData(), sulContents);
        }
    }
}
