namespace Sulucz.ContentDelivery.Data
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
    }
}
