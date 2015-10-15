using System;
using Test.NewSolution.Contracts.Repositories;
using SQLite.Net;
using Test.NewSolution.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Test.NewSolution.Data.Repositories
{
    /// <summary>
    /// Repository.
    /// </summary>
    public class Repository<TModel>: IRepository<TModel>
        where TModel  : RepositoryModel, new()          
    {
        #region Private Members

        /// <summary>
        /// The connection.
        /// </summary>
        private SQLiteConnection _connection;

        /// <summary>
        /// The repository provider.
        /// </summary>
        private readonly IConnectionProvider _connectionProvider;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Data.Repositories.Repository`1"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public Repository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            _connection = _connectionProvider.GetSQLConnection();
            _connection.CreateTable<TModel> ();
        }

        #region IRepository implementation

        /// <summary>
        /// Updated the entity
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public void Update (TModel entity) 
        {
            _connection.Update (entity);
        }

        /// <summary>
        /// Inserts the entity
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public void Insert (TModel entity) 
        {
            _connection.Insert (entity);
        }

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public void Delete (TModel entity) 
        {
            _connection.Delete (entity);
        }

        /// <summary>
        /// Deletes all entities
        /// </summary>
        public void DeleteAll () 
        {
            _connection.DeleteAll<TModel>();
        }

        /// <summary>
        /// Returns items async
        /// </summary>
        /// <returns>The items async.</returns>
        public IEnumerable<TModel> GetItems () 
        {
            var retVal = _connection.Table<TModel> ().ToList();
            return retVal.Cast<TModel> ();
        }

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <returns>The item by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public TModel GetItemById (string id) 
        {
            var retVal = _connection.Table<TModel> ().Where (mn => mn.Id == id).FirstOrDefault();
            if (retVal == null)
                return default(TModel);

            return (TModel)retVal;
        }

        /// <summary>
        /// Returns the number of items in the repository
        /// </summary>
        /// <returns>The count async.</returns>
        public int GetCount()
        {
            return _connection.Table<TModel> ().Count();
        }

        /// <summary>
        /// Filtered items
        /// </summary>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public IEnumerable<TModel> GetItems(Expression<Func<TModel, bool>> predExpr)
        {
            var retVal = _connection.Table<TModel>().Where(predExpr).ToList();
            return retVal.Cast<TModel>();
        }
        #endregion
    }
}

