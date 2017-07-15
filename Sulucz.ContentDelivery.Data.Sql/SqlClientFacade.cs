namespace Sulucz.ContentDelivery.Data.Sql
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Sulucz.ContentDelivery.Common.Exceptions;

    /// <summary>
    /// The SQL client facade.
    /// </summary>
    internal sealed class SqlClientFacade : IDisposable
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
        /// Initializes a new instance of the <see cref="SqlClientFacade"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal SqlClientFacade(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.tokenSource.Dispose();
        }

        /// <summary>
        /// Asynchronously execute a stored procedure.
        /// </summary>
        /// <param name="storedproc">The stored procedure.</param>
        /// <param name="parameterModifier">The parameter modifier.</param>
        /// <param name="reader1">The reader 1.</param>
        /// <typeparam name="T1">The type which the first reader produces.</typeparam>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task<IEnumerable<T1>> ExecuteAsync<T1>(
            string storedproc,
            Action<SqlParameterCollection> parameterModifier,
            Func<SqlDataReader, T1> reader1) where T1 : class
        {
            return
                this.ExecuteAsync(storedproc, parameterModifier, new Func<SqlDataReader, object>[] { reader1 })
                    .ContinueWith(
                        task =>
                        {
                            return SqlClientFacade.ExecuteContinuation(
                                task,
                                result => (IEnumerable<T1>)result.First());
                        });
        }

        /// <summary>
        /// Asynchronously execute a stored procedure.
        /// </summary>
        /// <param name="storedproc">The stored procedure.</param>
        /// <param name="parameterModifier">The parameter modifier.</param>
        /// <param name="reader1">The reader 1.</param>
        /// <param name="reader2">The reader 2.</param>
        /// <typeparam name="T1">The type which the first reader produces.</typeparam>
        /// <typeparam name="T2">The type which the second reader produces.</typeparam>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteAsync<T1, T2>(
            string storedproc,
            Action<SqlParameterCollection> parameterModifier,
            Func<SqlDataReader, T1> reader1,
            Func<SqlDataReader, T1> reader2) where T1 : class where T2 : class
        {
            return
                this.ExecuteAsync(storedproc, parameterModifier, new Func<SqlDataReader, object>[] { reader1, reader2 })
                    .ContinueWith(
                        task =>
                        {
                            return SqlClientFacade.ExecuteContinuation(
                                task,
                                result => ((IEnumerable<T1>)result[0], (IEnumerable<T2>)result[1]));
                        });
        }

        /// <summary>
        /// Verifies standard return codes.
        /// </summary>
        /// <param name="parameters">The parameters to the stored procedure.</param>
        /// <exception cref="ItemNotFoundException">If something was not found.</exception>
        /// <exception cref="BadArgumentsException">For bad arguments.</exception>
        /// <exception cref="GenericFailureException">Generic stuff.</exception>
        private static void VerifyResults(SqlParameterCollection parameters)
        {
            var returnValue = parameters["ReturnValue"].Value as int? ?? 0;
            var errorMessage = parameters["errormessage"].Value as string;

            switch (returnValue)
            {
                case 0:
                    return;
                case 1205:
                    throw new GenericFailureException("SQL FAIL", "SQL Deadlock. Sucks.", HttpStatusCode.InternalServerError);
                case 50001:
                    throw new ItemNotFoundException(errorMessage ?? "The item was not found.");
                case 50002:
                    throw new BadArgumentsException(errorMessage ?? "Bad arguments were provided");
                default:
                    throw new GenericFailureException("SQL FAIL", "Something went wrong for sure.", HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// The execute continuation.
        /// </summary>
        /// <param name="queryResult">The query result.</param>
        /// <param name="transform">The transform.</param>
        /// <typeparam name="T">The return type.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        /// <exception cref="Exception">Re-throws whatever the task threw.</exception>
        /// <exception cref="CancellationException">If the task was cancelled.</exception>
        private static T ExecuteContinuation<T>(Task<IEnumerable<object>[]> queryResult, Func<IEnumerable<object>[], T> transform)
        {
            if (queryResult.IsFaulted)
            {
                throw queryResult.Exception.InnerException;
            }

            if (queryResult.IsCanceled)
            {
                throw new CancellationException("Query was canelled.");
            }

            return transform(queryResult.Result);
        }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="storedproc">The stored procedure.</param>
        /// <param name="parameterModifier">The parameter modifier.</param>
        /// <param name="readers">The readers.</param>
        /// <returns>The results.</returns>
        private async Task<IEnumerable<object>[]> ExecuteAsync(
            string storedproc,
            Action<SqlParameterCollection> parameterModifier,
            Func<SqlDataReader, object>[] readers)
        {
            using (var connection = await this.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedproc;

                // Add default parameters.
                var returnValue = new SqlParameter("ReturnValue", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
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
                    var results = new List<object>[readers.Length];
                    for (var i = 0; i < readers.Length; i++)
                    {
                        VerifyResults(command.Parameters);

                        results[i] = new List<object>();

                        while (await reader.ReadAsync())
                        {
                            results[i].Add(readers[i](reader));
                        }

                        await reader.NextResultAsync(this.tokenSource.Token);
                    }

                    return results;
                }
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
