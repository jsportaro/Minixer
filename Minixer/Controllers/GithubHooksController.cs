using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Minixer.Hubs;
using Newtonsoft.Json.Linq;

namespace Minixer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubHooksController : ControllerBase
    {
        private readonly IHubContext<RepositoryHub> hubContext;


        public GithubHooksController(IHubContext<RepositoryHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        // GET: api/GithubHooks
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GithubHooks/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GithubHooks
        [HttpPost]
        public async Task Post([FromBody] JToken body)
        {


            switch (Request.Headers["X-GitHub-Event"])
            {
                case "ping":
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "ping");
                    break;
                case "push":
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "push");
                    break;
            }
        }

        // PUT: api/GithubHooks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
