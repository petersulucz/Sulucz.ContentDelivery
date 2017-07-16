namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// The invalid model exception.
    /// </summary>
    public class InvalidModelException : GenericFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidModelException"/> class.
        /// </summary>
        /// <param name="longMessage">The long message.</param>
        public InvalidModelException(string longMessage)
            : base("BAD MODEL", longMessage, HttpStatusCode.BadRequest)
        {
        }
    }
}
