using System;
using System.Collections.Generic;

namespace intevent_web.Models
{
    public class MediaVoteSet
    {
        public int Id { get; set; }
      
        public IEnumerable<MediaVote> MediaVotes { get; set; }

        public int TotalVotes { get; set; }

        public DateTimeOffset TimeVotingStarted { get; set; }

        public DateTimeOffset? TimeVotingEnded { get; set; }
    }
}
