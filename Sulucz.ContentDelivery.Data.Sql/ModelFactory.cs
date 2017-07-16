namespace Sulucz.ContentDelivery.Data.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Microsoft.SqlServer.Server;

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

            return new SuluczPostContent(postid, orderid, content, contenttype.ParseEnum<SuluczContentType>(), revision, uniqueid);
        }

        /// <summary>
        /// Generates the SQL parameter for the content list.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>The parameter.</returns>
        public static SqlParameter GenerateContentList(IEnumerable<SuluczPostContent> contents)
        {
            return new SqlParameter("postcontent", SqlDbType.Structured)
                                {
                                    TypeName = "su.postcontentlist",
                                    Value = ModelFactory.GenerateContentRecords(contents)
                                };
        }

        /// <summary>
        /// The generate content records.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        private static IEnumerable<SqlDataRecord> GenerateContentRecords(IEnumerable<SuluczPostContent> contents)
        {
            var record = new SqlDataRecord(
                new SqlMetaData("orderid", SqlDbType.Int),
                new SqlMetaData("uniqueid", SqlDbType.UniqueIdentifier),
                new SqlMetaData("revision", SqlDbType.Int),
                new SqlMetaData("contenttype", SqlDbType.NVarChar, 64),
                new SqlMetaData("content", SqlDbType.NVarChar, 2048));

            foreach (var content in contents)
            {
                record.SetInt32(0, content.OrderId);
                record.SetGuid(1, content.UniqueIdentifier);
                record.SetInt32(2, content.Revision);
                record.SetString(3, content.ContentType.ToString());
                record.SetString(4, content.Content);

                yield return record;
            }
        }
    }
}
