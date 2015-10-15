using System;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Contracts.Repositories;

namespace Test.NewSolution.Data.Repositories
{
	/// <summary>
	/// Implements the base Repository class for building SQLite database repositories 
	/// </summary>
	public class RepositoryAsync<TModel>: IRepositoryAsync<TModel>
        where TModel  : RepositoryModel, new()			
	{
		#region Private Members

		/// <summary>
		/// The connection.
		/// </summary>
		private SQLiteAsyncConnection _connection;

        /// <summary>
        /// The initialization task
        /// </summary>
        private Task Initialization;

        /// <summary>
        /// The repository provider.
        /// </summary>
        private readonly IConnectionProvider _connectionProvider;

		#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Data.Repositories.Repository`1"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public RepositoryAsync(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            Initialization = InitializeAsync();
        }

		#region IRepository implementation

		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		public Task InitializeAsync()
		{
            var connection = _connectionProvider.GetSQLConnection();
			_connection = new SQLiteAsyncConnection (() => connection);
			return _connection.CreateTableAsync<TModel> ();
		}

		/// <summary>
		/// Updated the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public async Task UpdateAsync (TModel entity) 
		{
            await Initialization;
			await _connection.UpdateAsync (entity);
		}

		/// <summary>
		/// Inserts the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public async Task InsertAsync (TModel entity) 
		{
            await Initialization;
			await _connection.InsertAsync (entity);
		}

		/// <summary>
		/// Deletes the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public async Task DeleteAsync (TModel entity) 
		{
            await Initialization;
			await _connection.DeleteAsync (entity);
		}

        /// <summary>
        /// Deletes all entities
        /// </summary>
        public async Task DeleteAllAsync () 
        {
            await Initialization;
            await _connection.DeleteAllAsync<TModel>(default(CancellationToken));
        }

		/// <summary>
		/// Returns items async
		/// </summary>
		/// <returns>The items async.</returns>
		public async Task<IEnumerable<TModel>> GetItemsAsync () 
		{
            await Initialization;

			var retVal = await _connection.Table<TModel> ().ToListAsync();
			return retVal.Cast<TModel> ();
		}

		/// <summary>
		/// Gets the item by identifier.
		/// </summary>
		/// <returns>The item by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<TModel> GetItemByIdAsync (string id) 
		{
            await Initialization;

			var retVal = await _connection.Table<TModel> ().Where (mn => mn.Id == id).FirstOrDefaultAsync ();
			if (retVal == null)
				return default(TModel);

			return (TModel)retVal;
		}

		/// <summary>
		/// Returns the number of items in the repository
		/// </summary>
		/// <returns>The count async.</returns>
		public async Task<int> GetCountAsync()
		{
            await Initialization;

			return await _connection.Table<TModel> ().CountAsync ();
		}

        /// <summary>
        /// Filtered items
        /// </summary>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predExpr)
        {
            await Initialization;

            var retVal = await _connection.Table<TModel>().Where(predExpr).ToListAsync();
            return retVal.Cast<TModel>();
        }
		#endregion
    }
}

