namespace Sulucz.ContentDelivery.ApiTestApp
{
    using System.Net;

    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks to see if a request is local.
        /// </summary>
        /// <param name="self">The request.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsLocal(this HttpRequest self)
        {
            var connection = self.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                return connection.LocalIpAddress != null ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress) : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            // for in memory TestServer or when dealing with default connection info
            return connection.RemoteIpAddress == null && connection.LocalIpAddress == null;
        }
    }
}
