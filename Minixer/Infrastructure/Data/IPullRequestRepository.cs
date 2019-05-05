using Minixer.Domain;
using Minixer.Domain.PullRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.Data
{
    public interface IPullRequestRepository
    {
        Task Add(Container pullRequestContainer);

       Task<IEnumerable<PullRequest>> GetAsync(int page, int size);
    }
}
