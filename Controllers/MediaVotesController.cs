using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using intevent_web.Models;

namespace intevent_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaVotesController : ControllerBase
    {
        private IEnumerable<MediaVoteSet> VoteSets = new List<MediaVoteSet>
        {
            new MediaVoteSet
            {
                Id = 1,
                TimeVotingStarted = DateTimeOffset.Now,
                TimeVotingEnded = DateTimeOffset.Now,
                MediaVotes = new List<MediaVote>
                {
                    new MediaVote
                    {
                        Id = 1,
                        MediaId = 1,
                        MediaVoteSetId = 1,
                        TotalVotes = 1,
                        TimeUserVoted = DateTimeOffset.Now,
                    },
                    new MediaVote
                    {
                        Id = 2,
                        MediaId = 2,
                        MediaVoteSetId = 1,
                        TotalVotes = 2,
                    },
                },
            },
            new MediaVoteSet
            {
                Id = 2,
                TimeVotingStarted = DateTimeOffset.Now,
                TimeVotingEnded = DateTimeOffset.Now,
                MediaVotes = new List<MediaVote>
                {
                    new MediaVote
                    {
                        Id = 3,
                        MediaId = 1,
                        MediaVoteSetId = 2,
                        TotalVotes = 1,
                    },
                    new MediaVote
                    {
                        Id = 4,
                        MediaId = 2,
                        MediaVoteSetId = 2,
                        TotalVotes = 2,
                        TimeUserVoted = DateTimeOffset.Now,
                    },
                },
            },
        };

        // GET api/votes
        [HttpGet]
        public IEnumerable<MediaVoteSet> Get()
        {
            return VoteSets;
        }

        // GET api/votes/5
        [HttpGet("{id}")]
        public MediaVoteSet Get(int id)
        {
            return VoteSets.SingleOrDefault(s => s.Id == id);
        }

        // POST api/votes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
