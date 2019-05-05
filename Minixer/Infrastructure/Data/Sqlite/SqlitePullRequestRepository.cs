using Dapper;
using Dapper.Contrib.Extensions;    
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Minixer.Domain.PullRequests;
using System.Data.SQLite;
using System;


//CREATE TABLE pull_request
//(
//    id INTEGER PRIMARY KEY,
//    title TEXT,
//    createdAt TEXT
//);

namespace Minixer.Infrastructure.Data.Sqlite
{
    public class SqlitePullRequestRepository : IPullRequestRepository
    {
        private readonly IDbConnection connection;

        public SqlitePullRequestRepository(SQLiteConnection connection)
        {
            this.connection = connection;
        }
        public async Task Add(Container pullRequestContainer)
        {
            try
            {
                if (pullRequestContainer.Action == "opened")
                {
                    await connection.ExecuteAsync("INSERT INTO pull_request (id, title, createdAt) values (@id, @title, @createdAt)",
                        new { id = pullRequestContainer.PullRequest.Number, title = pullRequestContainer.PullRequest.Title, pullRequestContainer.PullRequest.CreatedAt });
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async Task<IEnumerable<PullRequest>> GetAsync(int page, int size)
        {
            var list = await connection.QueryAsync<PullRequest>("Select * From pull_request");

            return list;
        }
    }
}
