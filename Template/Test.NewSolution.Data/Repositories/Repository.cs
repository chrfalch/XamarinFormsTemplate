using System;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Test.NewSolution.Repositories;
using Test.NewSolution.Contracts.Models;

namespace Test.NewSolution.Data.Repositories
{
	/// <summary>
	/// Implements the base Repository class for building SQLite database repositories 
	/// </summary>
	public abstract class Repository<TModel>: IRepository<TModel>
        where TModel  : class, IRepositoryModel, new()			
	{
		#region Private Members

		/// <summary>
		/// The connection.
		/// </summary>
		private SQLiteAsyncConnection _connection;

        /// <summary>
        /// The initialized flag.
        /// </summary>
        private bool _initializedFlag = false;

		#endregion

		#region IRepository implementation

		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		public abstract Task InitializeAsync ();

		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		public Task InitializeAsync(SQLiteConnectionWithLock connection)
		{
            _initializedFlag = true;
			_connection = new SQLiteAsyncConnection (() => connection);
			return _connection.CreateTableAsync<TModel> ();
		}

		/// <summary>
		/// Updated the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task UpdateAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.UpdateAsync (entity);
		}

		/// <summary>
		/// Inserts the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task InsertAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.InsertAsync (entity);
		}

		/// <summary>
		/// Deletes the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task DeleteAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.DeleteAsync (entity);
		}

        /// <summary>
        /// Deletes all entities
        /// </summary>
        public Task DeleteAllAsync () 
        {
            EnsureInitialized();

            return _connection.DeleteAllAsync<TModel>(default(CancellationToken));
        }

		/// <summary>
		/// Returns items async
		/// </summary>
		/// <returns>The items async.</returns>
		public async Task<IEnumerable<TModel>> GetItemsAsync () 
		{
            EnsureInitialized();

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
            EnsureInitialized();

			var retVal = await _connection.Table<TModel> ().Where (mn => mn.Id == id).FirstOrDefaultAsync ();
			if (retVal == null)
				return default(TModel);

			return (TModel)retVal;
		}

		/// <summary>
		/// Returns the number of items in the repository
		/// </summary>
		/// <returns>The count async.</returns>
		public Task<int> GetCountAsync()
		{
            EnsureInitialized();

			return _connection.Table<TModel> ().CountAsync ();
		}

        /// <summary>
        /// Filtered items
        /// </summary>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predExpr)
        {
            EnsureInitialized();

            var retVal = await _connection.Table<TModel>().Where(predExpr).ToListAsync();
            return retVal.Cast<TModel>();
        }
		#endregion

        #region Private Members

        /// <summary>
        /// Ensures that the class has been initialized and raises an exception if not
        /// </summary>
        private void EnsureInitialized()
        {
            if (_initializedFlag)
                return;

            throw new InvalidOperationException("Repository needs to be initialized before it is used.");
        }
        #endregion
    }
}

