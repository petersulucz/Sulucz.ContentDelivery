namespace Sulucz.ContentDelivery.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Sulucz post.
    /// </summary>
    public class SuluczPost : IValidateable
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

        /// <summary>
        /// Returns the list of all invalid reasons.
        /// </summary>
        /// <returns>The invalid reasoning. Empty list if everything is invalid.</returns>
        public IEnumerable<(string key, string reason)> IsValid()
        {
            if (null == this.MetaData)
            {
                yield return ("Metadata", "This cannot be null");
                yield break;
            }

            if (null == this.Contents || false == this.Contents.Any() || this.Contents.Count > 20)
            {
                yield return ("Contents", "Contents must be between 1 and 20 in length");
                yield break;
            }

            foreach (var result in this.MetaData.IsValid())
            {
                yield return result;
            }

            foreach (var reason in this.Contents.SelectMany(c => c.IsValid()))
            {
                yield return reason;
            }
        }
    }
}
