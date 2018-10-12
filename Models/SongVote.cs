using System;
using GraphQL.Types;
using intevent_web.Models;

namespace intevent_web.Models
{
    public class SongVote
    {
        public string VoterId { get; set; }

        public string SongId { get; set; }
    }

        public class SongVoteGraphType : ObjectGraphType<SongVote>
    {
        public SongVoteGraphType()
        {
            Name = "SongVote";
            Description = "";
            Field(_ => _.VoterId).Description("Voter Id");
            Field(_ => _.SongId).Description("Song Id");
        }
    }

    public class SongVoteInputGraphType : InputObjectGraphType<SongVote>
    {
        public SongVoteInputGraphType()
        {
            Name = "SongVoteInput";
            Description = "";
            Field(_ => _.VoterId).Description("Voter Id");
            Field(_ => _.SongId).Description("Song Id");
        }
    }
}
