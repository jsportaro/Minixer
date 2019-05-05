using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Domain.PullRequests
{
    public class Container
    {
        public string Action { get; set; }

        [JsonProperty("pull_request")]
        public PullRequest PullRequest { get; set; }
    }
}
