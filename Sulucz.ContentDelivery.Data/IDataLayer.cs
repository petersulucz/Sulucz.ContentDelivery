﻿namespace Sulucz.ContentDelivery.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The DataLayer interface.
    /// </summary>
    public interface IDataLayer : IDisposable
    {
        /// <summary>
        /// The get post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="slug">The slug.</param>
        /// <param name="top">The number of posts to return.</param>
        /// <param name="skip">The number to skip. Used for paging.</param>
        /// <returns>The list of posts.</returns>
        Task<IReadOnlyCollection<SuluczPost>> GetPost(int? postId = null, string slug = null, int top = 20, int skip = 0);

        /// <summary>
        /// Creates a post, or updates an old one.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>An async task.</returns>
        Task SetPost(SuluczPost post);

        /// <summary>
        /// Deletes a post. Returns success if the post is already deleted.
        /// </summary>
        /// <param name="postid">The post id.</param>
        /// <returns>An async task</returns>
        Task DeletePost(int postid);
    }
}
