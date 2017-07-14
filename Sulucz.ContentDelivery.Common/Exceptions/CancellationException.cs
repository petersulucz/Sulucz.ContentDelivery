namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// The cancellation exception.
    /// </summary>
    public class CancellationException : GenericFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationException"/> class.
        /// </summary>
        /// <param name="longMessage">
        /// The long message.
        /// </param>
        public CancellationException(string longMessage)
            : base("OPER CANCELLED", longMessage, HttpStatusCode.InternalServerError)
        {
        }
    }
}
