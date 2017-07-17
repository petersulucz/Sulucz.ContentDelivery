namespace Sulucz.ContentDelivery.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The Validateable interface.
    /// </summary>
    internal interface IValidateable
    {
        /// <summary>
        /// Returns the list of all invalid reasons.
        /// </summary>
        /// <returns>The invalid reasoning. Empty list if everything is invalid.</returns>
        IEnumerable<(string key, string reason)> IsValid();
    }
}
