using System;
using GraphQL.Types;
using intevent_web.Models;

namespace intevent_web.Models
{
    public class SongVote
    {
        public string VoterId { get; set; }

        public int SongId { get; set; }

        public int TotalVotes { get; set; }

        public DateTimeOffset? TimeUserVoted { get; set; }
    }

    public class SongVoteGraphType : ObjectGraphType<SongVote>
    {
        public SongVoteGraphType()
        {
            Field(_ => _.VoterId).Description("Voter Id");
            Field(_ => _.SongId).Description("Song Id");
            Field(_ => _.TotalVotes).Description("Total Votes");
            Field(_ => _.TimeUserVoted).Description("Time User Voted");
        }
    }
}
