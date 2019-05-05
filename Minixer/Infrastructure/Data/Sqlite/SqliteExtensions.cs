using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.Data.Sqlite
{
    public static class SqliteExtensions
    {
        private const string Database = @".\storage.db";

        public static void AddSqliteStorage(this IServiceCollection services)
        {
            EnsureDatabaseExists();

            services.AddScoped<IPullRequestRepository, SqlitePullRequestRepository>();
            services.AddScoped((sp) =>
            {
                var connectionStringBuilder = new SQLiteConnectionStringBuilder();

                connectionStringBuilder.DataSource = Database;
                return new SQLiteConnection(connectionStringBuilder.ToString());
            });
        }

        private static void EnsureDatabaseExists()
        {
            var bd = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(Database))
            {
                CreateDatabase();
            }
            else
            {
                UpdateSchema();
            }
        }

        private static void UpdateSchema()
        {
        }

        private static void CreateDatabase()
        {
            var connectionStringBuilder = new SQLiteConnectionStringBuilder();

            connectionStringBuilder.DataSource = Database;
            using (var connection = new SQLiteConnection(connectionStringBuilder.ToString()))
            {
                var seedData = File.ReadAllText(@".\Infrastructure\Data\Sqlite\Schema\Seed.sql");

                var result = connection.Execute(seedData);
            }
        }
    }
}
