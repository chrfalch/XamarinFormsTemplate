using System;
using Test.NewSolution.Data.Repositories;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Test.NewSolution.Contracts.Models;

namespace Test.NewSolution.iOS.Repositories
{
	/// <summary>
	/// Repositoryi OS platform.
	/// </summary>
	public class RepositoryiOSPlatform<TModel>: Repository<TModel>
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
				new SQLitePlatformIOS(), new SQLiteConnectionString(
					filename, false, null));

			return InitializeAsync (connection);
		}
	}
}

