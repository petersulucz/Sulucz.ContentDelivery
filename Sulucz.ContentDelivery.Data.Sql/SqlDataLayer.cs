namespace Sulucz.ContentDelivery.Data.Sql
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading.Tasks;

    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The SQL data layer.
    /// </summary>
    internal class SqlDataLayer : IDataLayer
    {
        /// <summary>
        /// The SQL client.
        /// </summary>
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
        public async Task<IReadOnlyCollection<SuluczPost>> GetPost(int? postId = null, string slug = null, int top = 20, int skip = 0)
        {
            var results =
                await this.sqlClient.ExecuteAsync<SuluczPostContent, SuluczPostMetaData>(
                    "su.getpost",
                    parameters =>
                                  {
                                      parameters.AddWithValue("postid", postId);
                                      parameters.AddWithValue("slug", slug);
                                      parameters.AddWithValue("top", top);
                                      parameters.AddWithValue("skip", skip);
                                  },
                              ModelFactory.GetContent,
                              ModelFactory.GetMetaData);

            var content = results.Item1;
            var metadata = results.Item2;

            var contentLookup = content.ToLookup(c => c.PostId);

            return
                // ReSharper disable once PossibleInvalidOperationException because its not possible
                metadata.Select(m => new SuluczPost(m, contentLookup[m.Id.Value].OrderBy(c => c.OrderId)))
                    .ToImmutableList();
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
