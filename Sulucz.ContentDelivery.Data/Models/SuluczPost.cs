namespace Sulucz.ContentDelivery.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Sulucz post.
    /// </summary>
    public class SuluczPost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuluczPost"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="contents">The contents.</param>
        public SuluczPost(SuluczPostMetaData metadata, IEnumerable<SuluczPostContent> contents)
        {
            this.MetaData = metadata;
            this.Contents = contents.ToList();
        }

        /// <summary>
        /// Gets the meta data.
        /// </summary>
        public SuluczPostMetaData MetaData { get; }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        public IList<SuluczPostContent> Contents { get; }
    }
}
