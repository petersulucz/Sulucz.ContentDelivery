namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// The bad arguments exception.
    /// </summary>
    public class BadArgumentsException : GenericFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadArgumentsException"/> class.
        /// </summary>
        /// <param name="longMessage">The long message.</param>
        public BadArgumentsException(string longMessage)
            : base("BAD REQUEST", longMessage, HttpStatusCode.BadRequest)
        {
        }
    }
}
