namespace Sulucz.ContentDelivery.Data.Sql
{
    using System;
    using System.Data.SqlClient;

    using Sulucz.ContentDelivery.Common;
    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The model factory.
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// Gets a metadata object from the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The <see cref="SuluczPostMetaData"/>.</returns>
        public static SuluczPostMetaData GetMetaData(SqlDataReader reader)
        {
            var id = (int)reader["id"];
            var slug = (string)reader["slug"];
            var title = (string)reader["title"];
            var description = (string)reader["description"];
            var whencreated = (DateTimeOffset)reader["whencreated"];
            var whenpublished = (DateTimeOffset)reader["whenpublished"];
            var revision = (int)reader["revision"];
            return new SuluczPostMetaData(id, revision, slug, title, description, whencreated, whenpublished);
        }

        /// <summary>
        /// Gets a post content object from the reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The post content.</returns>
        public static SuluczPostContent GetContent(SqlDataReader reader)
        {
            var postid = (int)reader["postid"];
            var orderid = (int)reader["orderid"];
            var revision = (int)reader["revision"];
            var uniqueid = (Guid)reader["uniqueid"];
            var content = (string)reader["content"];
            var contenttype = (string)reader["contenttype"];

            return new SuluczPostContent(postid, orderid, content, contenttype.ParseEnum<SuluczContentType>(), revision);
        }
    }
}
