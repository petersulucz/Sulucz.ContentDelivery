namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System;
    using System.Net;

    /// <summary>
    /// The generic failure exception.
    /// </summary>
    public class GenericFailureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFailureException"/> class.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="longMessage">The long message.</param>
        /// <param name="status">The status.</param>
        public GenericFailureException(string shortMessage, string longMessage, HttpStatusCode status) : base(longMessage)
        {
            this.ShortMessage = shortMessage;
            this.StatusCode = status;
        }

        /// <summary>
        /// Gets the short message.
        /// </summary>
        public string ShortMessage { get; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}
