namespace Sulucz.ContentDelivery.Common.Exceptions
{
    using System.Net;

    /// <summary>
    /// The item not found exception.
    /// </summary>
    public class ItemNotFoundException : GenericFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        /// <param name="longMessage">The long message.</param>
        public ItemNotFoundException(string longMessage)
            : base("NOT FOUND", longMessage, HttpStatusCode.NotFound)
        {
        }
    }
}
