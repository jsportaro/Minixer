using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Minixer.Domain.PullRequests;
using Minixer.Infrastructure.Data;

namespace Minixer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPullRequestRepository pullRequestRepository;

        public IndexModel(IPullRequestRepository pullRequestRepository)
        {
            this.pullRequestRepository = pullRequestRepository;
        }

        public IEnumerable<PullRequest> PullRequests { get; private set; }

        public async Task OnGetAsync()
        {
            PullRequests = await pullRequestRepository.GetAsync(1, 1);
        }
    }
}