namespace Sulucz.ContentDelivery.Data.Models
{
    using System;

    /// <summary>
    /// The sulucz post content.
    /// </summary>
    public class SuluczPostMetaData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuluczPostMetaData"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="slug">The slug.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="whenCreated">The when created.</param>
        /// <param name="whenPublished">The when published.</param>
        public SuluczPostMetaData(
            int? id,
            int revision,
            string slug,
            string title,
            string description,
            DateTimeOffset whenCreated,
            DateTimeOffset whenPublished)
        {
            this.Id = id;
            this.Revision = revision;
            this.Slug = slug;
            this.Title = title;
            this.Description = description;
            this.WhenCreated = whenCreated;
            this.WhenPublished = whenPublished;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int? Id { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        public int Revision { get; }

        /// <summary>
        /// Gets the slug.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the when created date.
        /// </summary>
        public DateTimeOffset WhenCreated { get; }

        /// <summary>
        /// Gets the when published.
        /// </summary>
        public DateTimeOffset WhenPublished { get; }
    }
}
