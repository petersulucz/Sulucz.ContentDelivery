namespace Sulucz.ContentDelivery.Data.Sql
{
    using System;
    using System.IO;
    using System.Reflection;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The sql datalayer factory.
    /// </summary>
    public static class SqlDatalayerFactory
    {
        /// <summary>
        /// The lazy instance of the data layer.
        /// </summary>
        private static readonly Lazy<IDataLayer> LazyInstance = new Lazy<IDataLayer>(
            () =>
                {
                    var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    var builder = new ConfigurationBuilder();
                    builder.SetBasePath(dir);
                    builder.AddJsonFile("sqlconfig.json");
                    var config = builder.Build();
                    var connstring = config["ConnectionString"];
                    return new SqlDataLayer(connstring);
                });

        /// <summary>
        /// Gets the datalayer instance.
        /// </summary>
        public static IDataLayer Instance => SqlDatalayerFactory.LazyInstance.Value;
    }
}
