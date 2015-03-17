using System;
using Test.NewSolution.Data.Repositories;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Contracts.Repositories;

namespace Test.NewSolution.iOS.Native.Repositories
{
	/// <summary>
	/// Repositoryi OS platform.
	/// </summary>
    public class RepositoryProvider: IRepositoryProvider
	{
        #region Private Members

        /// <summary>
        /// The connection.
        /// </summary>
        private SQLiteConnectionWithLock _connection;

        #endregion

        #region IRepositoryProvider implementation

        /// <summary>
        /// Gets the SQL connection.
        /// </summary>
        /// <returns>The SQL connection.</returns>
        public SQLiteConnectionWithLock GetSQLConnection()
        {
            if (_connection != null)
                return _connection;

            var folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filename = Path.Combine (folder, "storage.db");
            _connection = new SQLiteConnectionWithLock (
                new SQLitePlatformIOS(), new SQLiteConnectionString(
                    filename, false, null));

            return _connection;
        }

        #endregion

	}
}

