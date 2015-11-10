using System;

namespace Test.NewSolution.Contracts.Clients
{
    /// <summary>
    /// Base client.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        bool IsAuthenticated { get; }
    }
}

