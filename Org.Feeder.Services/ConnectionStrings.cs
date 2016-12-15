namespace Org.Feeder.Services
{
    internal static class ConnectionStrings
    {
        /// <summary>
        /// Default connection string. This connection will take some time to return results, and will sometimes timeout.
        /// </summary>
        internal const string Default = "local";

        /// <summary>
        /// Simulates an extra unreliable connection (for trying error cases within the app).
        /// This connetion will take some time to return results, and will often timeout.
        /// </summary>
        internal const string UnreliableConnection = "vogon";
    }
}