namespace Sulucz.ContentDelivery.Data
{
    using System.Linq;

    using Sulucz.ContentDelivery.Common.Exceptions;

    /// <summary>
    /// The validator.
    /// </summary>
    internal static class Validator
    {
        /// <summary>
        /// Validates if the object is ok.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="BadArgumentsException">If the object is bad.</exception>
        public static void Validate(IValidateable target)
        {
            var reasons = target.IsValid().ToList();
            if (false == reasons.Any())
            {
                return;
            }

            var result = string.Join(",", reasons.Select(r => $"{r.key} : {r.reason}"));
            throw new BadArgumentsException(result);
        }
    }
}
