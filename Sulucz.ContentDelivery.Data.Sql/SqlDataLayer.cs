namespace Sulucz.ContentDelivery.Data.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Sulucz.ContentDelivery.Common.Exceptions;
    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The SQL data layer.
    /// </summary>
    internal class SqlDataLayer : IDataLayer
    {

        private readonly SqlClientFacade sqlClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataLayer"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal SqlDataLayer(string connectionString)
        {
            this.sqlClient = new SqlClientFacade(connectionString);
        }

        /// <summary>
        /// The get post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="slug">The slug.</param>
        /// <param name="top">The number of posts to return.</param>
        /// <param name="skip">The number to skip. Used for paging.</param>
        /// <returns>The list of posts.</returns>
        public IReadOnlyCollection<SuluczPost> GetPost(int? postId = null, string slug = null, int top = 20, int skip = 0)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.sqlClient.Dispose();
        }
    }
}
