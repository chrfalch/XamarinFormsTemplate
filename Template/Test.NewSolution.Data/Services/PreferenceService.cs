using System;
using Test.NewSolution.Contracts.Services;
using Test.NewSolution.Repositories;
using Test.NewSolution.Contracts.Models;
using System.Threading.Tasks;
using Test.NewSolution.Helpers;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Test.NewSolution.Data.Services
{
    /// <summary>
    /// Preference service.
    /// </summary>
    public class PreferenceService: IPreferenceService
    {
        #region Private Members

        /// <summary>
        /// The in persist.
        /// </summary>
        private bool _inPersist;

        /// <summary>
        /// The update count.
        /// </summary>
        private int _updateCount = 0;

        /// <summary>
        /// The initialization task
        /// </summary>
        private Task Initialization;

        /// <summary>
        /// The logging service.
        /// </summary>
        private ILoggingService _loggingService;

        /// <summary>
        /// The key value repository.
        /// </summary>
        private IRepository<PreferenceModel> _preferenceModelRepository;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly Dictionary<string, PreferenceModel> _cache = new Dictionary<string, PreferenceModel>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Data.Services.PreferenceService"/> class.
        /// </summary>
        /// <param name="preferenceModelRepository">Preference model repository.</param>
        public PreferenceService(IRepository<PreferenceModel> preferenceModelRepository, ILoggingService loggingService)
        {
            _preferenceModelRepository = preferenceModelRepository;
            _loggingService = loggingService;
            Initialization = InitializeAsync();
        }

        #region IPreferenceService implementation

        /// <summary>
        /// Persists the data to disk
        /// </summary>
        /// <returns>The async.</returns>
        /// <summary>
        /// Persists the data to disk
        /// </summary>
        /// <returns>The async.</returns>
        public async Task PersistAsync()
        {       
            if (_inPersist)
                return;

            _inPersist = true;

            await Initialization;

            try
            {
                var cacheCopy = new Dictionary<string, PreferenceModel>();

                lock (_cache)
                {
                    foreach (var element in _cache)
                        cacheCopy.Add(element.Key, new PreferenceModel { Id = element.Value.Id, ValueAsJSON = element.Value.ValueAsJSON});
                }

                foreach (var keyValue in cacheCopy)
                    await _preferenceModelRepository.UpdateAsync(keyValue.Value);                
            }
            finally
            {
                _inPersist = false;
            }
        }

        #endregion

        #region IService implementation

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        public async Task InitializeAsync()
        {
            // Fill Cache
            foreach (var model in await _preferenceModelRepository.GetItemsAsync())
                _cache.Add(model.Id, model);              
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Sets the object for key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private void SetObjectForKey<T>(Expression<Func<object>> property, T value)
        {
            Task.Run(async () => await Initialization).Wait();

            var key = PropertyNameHelper.GetPropertyName<PreferenceService>(property);

            var json = JsonConvert.SerializeObject(value);

            if (!_cache.ContainsKey(key))
            {
                // create new model
                var item = new PreferenceModel{ Id = key, ValueAsJSON = json };

                // Add to cache 
                _cache.Add(key, item);

                // Add to the repo                
                Task.Run(async () => await _preferenceModelRepository.InsertAsync(item)).Wait();

            }
            else
            {
                // Update in cache
                var item = _cache[key];
                item.ValueAsJSON = json;
            }

            // Persist after a number of updates
            if(_updateCount > 20)
            {
                Task.Run(async () => await PersistAsync()).Wait();
                _updateCount = 0;
            }
            else
            {
                _updateCount++;
            }

            _loggingService.Log(LogLevel.Verbose, this, "SetObject(key={0}, value={1})", key, value);
        }

        /// <summary>
        /// Gets the object for key.
        /// </summary>
        /// <returns>The object for key.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private T GetObjectForKey<T>(Expression<Func<object>> property, T defaultValue)
        {
            Task.Run(async () => await Initialization).Wait();

            var key = PropertyNameHelper.GetPropertyName<PreferenceService>(property);

            // DO NOT LOG HERE!! Logging will cause the log system to never stop since each log causes
            // a call to a preference method which in turn will log..
            //Logger.Log(LogLevel.Verbose, this, "GetObject(key={0})", key);
            if(!_cache.ContainsKey(key))
            {                    
                SetObjectForKey<T>(property, defaultValue);
                return defaultValue;
            }

            return JsonConvert.DeserializeObject<T>(_cache[key].ValueAsJSON);

        }
        #endregion

    }
}

