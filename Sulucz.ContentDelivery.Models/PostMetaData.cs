namespace Sulucz.ContentDelivery.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The post meta data.
    /// </summary>
    public sealed class PostMetaData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMetaData"/> class.
        /// </summary>
        [Obsolete]
        public PostMetaData()
        {
            // Empty for asp.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostMetaData"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        public PostMetaData(SuluczPostMetaData metadata)
        {
            this.Id = metadata.Id.Value;
            this.Title = metadata.Title;
            this.Description = metadata.Description;
            this.WhenPublished = metadata.WhenPublished;
            this.Revision = metadata.Revision;
            this.Slug = metadata.Slug;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// Gets the when published.
        /// </summary>
        [Required]
        public DateTimeOffset WhenPublished { get; set; }

        /// <summary>
        /// Gets the slug.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Slug { get; set; }

        /// <summary>
        /// Gets the post revision number.
        /// </summary>
        [Required]
        public int Revision { get; set; }

        /// <summary>
        /// The to sulucz meta data.
        /// </summary>
        /// <returns>
        /// The <see cref="SuluczPostMetaData"/>.
        /// </returns>
        public SuluczPostMetaData ToSuluczMetaData()
        {
            return new SuluczPostMetaData(
                this.Id,
                this.Revision,
                this.Slug,
                this.Title,
                this.Description,
                DateTimeOffset.UtcNow,
                this.WhenPublished);
        }
    }
}
