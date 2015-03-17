using System;
using System.Threading.Tasks;

namespace Test.NewSolution.Contracts.Services
{
    /// <summary>
    /// Base service interface
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        Task InitializeAsync();
    }
}

