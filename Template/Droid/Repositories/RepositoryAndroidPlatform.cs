using System;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Test.NewSolution.Contracts.Models;
using Test.NewSolution.Data.Repositories;

namespace Test.NewSolution.Droid.Repositories
{
	/// <summary>
	/// Repository android platform.
	/// </summary>
	public class RepositoryAndroidPlatform<TModel>: Repository<TModel>
		where TModel  : class, IRepositoryModel, new()			
	{
        private bool initialized = false;

        public override Task InitializeAsync ()
        {
            if (initialized)
                return Task.FromResult(true);

            initialized = true;

			var folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filename = Path.Combine (folder, "storage.db");
			var connection = new SQLiteConnectionWithLock (
				new SQLitePlatformAndroid(), new SQLiteConnectionString(
					filename, false, null));

			return InitializeAsync (connection);
		}
	}
}

