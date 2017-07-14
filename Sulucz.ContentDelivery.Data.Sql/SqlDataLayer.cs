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
        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataLayer"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal SqlDataLayer(string connectionString)
        {
            this.connectionString = connectionString;
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

        public void Dispose()
        {
            this.tokenSource.Cancel();
        }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="storedproc">The storedproc.</param>
        /// <param name="parameterModifier">The parameter modifier.</param>
        /// <param name="readers">The readers.</param>
        /// <returns>The results.</returns>
        private async Task<object[]> ExecuteAsync(
            string storedproc,
            Action<SqlParameterCollection> parameterModifier,
            Func<SqlDataReader, Task<object>>[] readers)
        {
            using (var connection = await this.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedproc;

                var returnValue = new SqlParameter("ReturnValue", SqlDbType.Int)
                                      {
                                          Direction =
                                              ParameterDirection.ReturnValue
                                      };
                var errorMessage = new SqlParameter("errormessage", SqlDbType.NVarChar)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 2048
                                       };

                command.Parameters.Add(returnValue);
                command.Parameters.Add(errorMessage);

                parameterModifier(command.Parameters);

                using (var reader = await command.ExecuteReaderAsync(this.tokenSource.Token))
                {
                    var results = new object[readers.Length];
                    for (var i = 0; i < readers.Length; i++)
                    {
                        this.VerifyResults(command.Parameters);
                        results[i] = await readers[i](reader);

                        await reader.NextResultAsync(this.tokenSource.Token);
                    }

                    return results;
                }
            }
        }

        private void VerifyResults(SqlParameterCollection parameters)
        {
            var returnValue = parameters["ReturnValue"].Value as int? ?? 0;
            var errorMessage = parameters["errormessage"].Value as string;

            switch (returnValue)
            {
                case 0:
                    return;
                case 50001:
                    throw new ItemNotFoundException(errorMessage ?? "The item was not found.");
                case 50002:
                    throw new BadArgumentsException(errorMessage ?? "Bad arguments were provided");
                default:
                    throw new GenericFailureException("SQL FAIL", "Something went wrong for sure.", HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets an SQL connection.
        /// </summary>
        /// <returns>The <see cref="SqlConnection"/>.</returns>
        private async Task<SqlConnection> GetConnection()
        {
            var connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync(this.tokenSource.Token);
            if (connection.State != ConnectionState.Open)
            {
                throw this.tokenSource.IsCancellationRequested
                          ? (GenericFailureException)new CancellationException(
                              "Could not get SQL connection because the operation was cancelled by the webservice.")
                          : new ConnectionFailureException("Could not get a connecdtion to the SQl server.");
            }

            return connection;
        }
    }
}
