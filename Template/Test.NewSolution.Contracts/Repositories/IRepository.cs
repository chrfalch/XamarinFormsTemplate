using System;
using Test.NewSolution.Contracts.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.NewSolution.Contracts.Repositories
{
    /// <summary>
    /// I repository.
    /// </summary>
    public interface IRepository<TModel> where TModel: RepositoryModel, new()
    {

        /// <summary>
        /// Updated the entity
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        void Update(TModel entity) ;

        /// <summary>
        /// Inserts the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        void Insert (TModel entity);

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        void Delete (TModel entity);

        /// <summary>
        /// Deletes all async.
        /// </summary>
        /// <returns>The all async.</returns>
        void DeleteAll();

        /// <summary>
        /// Returns items async
        /// </summary>
        /// <returns>The items async.</returns>
        IEnumerable<TModel> GetItems();

        /// <summary>
        /// Gets the item by identifier.
        /// </summary>
        /// <returns>The item by identifier.</returns>
        /// <param name="id">Identifier.</param>
        TModel GetItemById (string id);

        /// <summary>
        /// Returns items with a filter
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        IEnumerable<TModel> GetItems(Expression<Func<TModel, bool>> predExpr);

        /// <summary>
        /// Gets the count async.
        /// </summary>
        /// <returns>The count async.</returns>
        int GetCount();

    }
}

