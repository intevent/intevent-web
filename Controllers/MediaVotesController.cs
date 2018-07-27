using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaVotesController : ControllerBase
    {
        private IMediaVotesService MediaVotesService { get; }

        public MediaVotesController(IMediaVotesService mediaVotesService)
        {
            MediaVotesService = mediaVotesService;
        }
        
        // GET api/votes
        [HttpGet]
        public async Task<IEnumerable<MediaVoteSet>> Get()
        {
            return await MediaVotesService.GetMediaVotes();
        }

        // GET api/votes/5
        [HttpGet("{id}")]
        public async Task<MediaVoteSet> Get(int id)
        {
            return await MediaVotesService.GetMediaVotes(id);
        }

        // POST api/votes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
