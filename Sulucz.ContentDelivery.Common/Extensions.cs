namespace Sulucz.ContentDelivery.Common
{
    using System;

    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The parse enum.
        /// </summary>
        /// <param name="self">The string to parse.</param>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ParseEnum<T>(this string self)
        {
            return (T)Enum.Parse(typeof(T), self, true);
        }
    }
}
