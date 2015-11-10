using System;
using System.Threading.Tasks;
using System.IO;
using Test.NewSolution.Contracts.Carriers;

namespace Test.NewSolution.Contracts.Clients
{
    /// <summary>
    /// I API client provider.
    /// </summary>
    public interface IApiClientProvider
    {
        /// <summary>
        /// Posts an async request and returns the json result
        /// </summary>
        /// <returns>The request async.</returns>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="request">Request.</param>
        /// <typeparam name="TRequestMessage">The 1st type parameter.</typeparam>
        Task<string> PostRequestAsync<TRequestMessage> (
            string endpoint, TRequestMessage request)
            where TRequestMessage: RequestCarrier;

        /// <summary>
        /// Posts an async request and returns the response
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Content.</param>
        Task<string> PostAsync (string requestUri, string content, string contentType);

        /// <summary>
        /// Puts an async request and returns the response
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Content.</param>
        Task<string> PutAsync (string requestUri, string content, string contentType);

        /// <summary>
        /// Sends a get request and returns the json async
        /// </summary>
        /// <returns>The request async.</returns>
        /// <param name="requestUri">Request URI.</param>
        Task<string> GetRequestAsync(string requestUri) ;

        /// <summary>
        /// Sends a get request and returns the json async
        /// </summary>
        /// <returns>The request async.</returns>
        /// <param name="requestUri">Request URI.</param>
        Task<Stream> GetRequestStreamAsync(string requestUri) ;

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        string BaseUrl {get;set;}

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        /// <value>The API version.</value>
        string ApiVersion { get; set; }
    }
}

