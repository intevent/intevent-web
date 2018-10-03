using System;
using GraphQL.Types;

namespace intevent_web.Models
{
    public class VoteTotal
    {
        public string SongId { get; set; }

        public int Votes { get; set; }
    }

    public class VoteTotalGraphType : ObjectGraphType<VoteTotal>
    {
        public VoteTotalGraphType()
        {
            Name = "VoteTotal";
            Description = "";
            Field(_ => _.SongId).Description("Song Id");
            Field(_ => _.Votes).Description("Total Number of Votes");
        }
    }
}
