using System;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;
using Test.NewSolution.Contracts.Repositories;

namespace Test.NewSolution.Droid.Platform.Repositories
{
    /// <summary>
    /// Repositoryi OS platform.
    /// </summary>
    public class DroidConnectionProvider: IConnectionProvider
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
                new SQLitePlatformAndroid(), new SQLiteConnectionString(
                    filename, false, null));

            return _connection;
        }

        #endregion

    }	
}

