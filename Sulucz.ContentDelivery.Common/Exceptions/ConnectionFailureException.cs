namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// The connection failure exception.
    /// </summary>
    public class ConnectionFailureException : GenericFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFailureException"/> class.
        /// </summary>
        /// <param name="longMessage">The long message.</param>
        public ConnectionFailureException(string longMessage)
            : base("CONN FAIL", longMessage, HttpStatusCode.InternalServerError)
        {
        }
    }
}
