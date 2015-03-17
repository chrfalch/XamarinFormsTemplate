using System;
using System.Threading.Tasks;

namespace Test.NewSolution.Contracts.Services
{
    /// <summary>
    /// I preference service.
    /// </summary>
    public interface IPreferenceService: IService
    {
        /// <summary>
        /// Persists the data to disk
        /// </summary>
        /// <returns>The async.</returns>
        Task PersistAsync();
    }
}

