using System;
using Test.NewSolution.Contracts.Services;
using System.Threading.Tasks;
using Test.NewSolution.Contracts.Carriers;
using System.Net.Http;
using System.Text;
using System.IO;
using ModernHttpClient;
using Test.NewSolution.Contracts.Clients;

namespace Test.NewSolution.Data.Clients
{
    /// <summary>
    /// Http API client provider.
    /// </summary>
    public class HttpApiClientProvider: IApiClientProvider
    {
        #region Private Members

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILoggingService _logger;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CF.Xamarin.Backend.Clients.ApiClient"/> class.
        /// </summary>
        public HttpApiClientProvider(ILoggingService logger)
        {
            _logger = logger;
        }

        #region Interface Members

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl {get;set;}

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        /// <value>The API version.</value>
        public string ApiVersion {get;set;}

        /// <summary>
        /// Posts the request async.
        /// </summary>
        /// <returns>The request async.</returns>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="request">Request.</param>
        /// <typeparam name="TRequestMessage">The 1st type parameter.</typeparam>
        /// <typeparam name="TResponseMessage">The 2nd type parameter.</typeparam>
        public Task<string> PostRequestAsync<TRequestMessage>(string endpoint, TRequestMessage request)
            where TRequestMessage: RequestCarrier
        {
            // Serialize request
            var jsonRequest = JsonSerializer.Serialize<TRequestMessage> (request);
            return PostAsync(endpoint, jsonRequest, "application/json");
        }

        /// <summary>
        /// Posts an async request and returns the response
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Content.</param>
        public async Task<string> PostAsync(string requestUri, string content, string contentType)
        {
            _logger.Log(LogLevel.Information, this, "ApiClient.PostRequestAsync({0})", BaseUrl + ApiVersion + requestUri);

            var messageToLog = content.Length > 1024 ? content.Substring(0, 1024) + "..." : content;
            _logger.Log(LogLevel.Verbose, this, messageToLog);

            // Create client
            var client = CreateClient();

            var httpContent = new StringContent(content, Encoding.UTF8, contentType);

            // Call api
            var responseMessage = await client.PostAsync (requestUri, httpContent);

            // Deserialize
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync ();

            _logger.Log(LogLevel.Verbose, this, "Results: {0}", jsonResponse);

            return jsonResponse;
        }

        /// <summary>
        /// Posts an async request and returns the response
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Content.</param>
        public async Task<string> PutAsync(string requestUri, string content, string contentType)
        {
            _logger.Log(LogLevel.Information, this, "ApiClient.PostRequestAsync({0})", BaseUrl + ApiVersion + requestUri);

            var messageToLog = content.Length > 1024 ? content.Substring(0, 1024) + "..." : content;
            _logger.Log(LogLevel.Verbose, this, messageToLog);

            // Create client
            var client = CreateClient();

            var httpContent = new StringContent(content, Encoding.UTF8, contentType);

            // Call api
            var responseMessage = await client.PutAsync (requestUri, httpContent);

            // Deserialize
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync ();

            _logger.Log(LogLevel.Verbose, this, "Results: {0}", jsonResponse);

            return jsonResponse;
        }

        /// <summary>
        /// Performs a get request
        /// </summary>
        /// <returns>The request async.</returns>
        public async Task<Stream> GetRequestStreamAsync(string requestUri) 
        {
            _logger.Log(LogLevel.Information, this, "ApiClient.GetRequestStreamAsync(" + BaseUrl + ApiVersion + requestUri + ")");

            // Create client
            var client = CreateClient ();

            // Read response
            var responseMessage = await client.GetAsync (requestUri);

            // Ensure success
            responseMessage.EnsureSuccessStatusCode ();

            // Deserialize response
            var stream = await responseMessage.Content.ReadAsStreamAsync();     
            _logger.Log(LogLevel.Verbose, this, "Resultlength: {0}", stream.Length);
            return stream;
        }

        /// <summary>
        /// Performs a get request
        /// </summary>
        /// <returns>The request async.</returns>
        public async Task<string> GetRequestAsync(string requestUri) 
        {
            _logger.Log(LogLevel.Information, this, "ApiClient.GetRequestAsync(" + BaseUrl + ApiVersion +requestUri + ")");

            // Create client
            var client = CreateClient ();

            // Read response
            var responseMessage = await client.GetAsync (requestUri);

            // Ensure success
            responseMessage.EnsureSuccessStatusCode ();

            // Deserialize response
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync ();
            _logger.Log(LogLevel.Verbose, this, "Results: {0}", jsonResponse);
            return jsonResponse;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Creates a new HttpClient set up with correct headers 
        /// </summary>
        /// <returns>The client.</returns>
        protected virtual HttpClient CreateClient()
        {
            var client = new HttpClient(new NativeMessageHandler()){
                BaseAddress = new Uri(BaseUrl + ApiVersion)
            };

            return client;
        }

        #endregion
    }
}

