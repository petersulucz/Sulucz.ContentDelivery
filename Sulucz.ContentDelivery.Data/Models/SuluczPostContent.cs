﻿namespace Sulucz.ContentDelivery.Data.Models
{
    using System;

    /// <summary>
    /// The sulucz post content.
    /// </summary>
    public class SuluczPostContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuluczPostContent"/> class.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        /// <param name="orderId">The order ID within the post.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentType">The content type.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="uniqueIdentifier">The unique id.</param>
        public SuluczPostContent(int postId, int orderId, string content, SuluczContentType contentType, int revision, Guid uniqueIdentifier)
        {
            this.Revision = revision;
            this.PostId = postId;
            this.OrderId = orderId;
            this.UniqueIdentifier = uniqueIdentifier;
            this.Content = content;
            this.ContentType = contentType;
        }

        /// <summary>
        /// Gets the unique id.
        /// </summary>
        public Guid UniqueIdentifier { get; }

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

        /// <summary>
        /// Gets the post id.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// Gets the order id.
        /// </summary>
        public int OrderId { get; }
    }
}
