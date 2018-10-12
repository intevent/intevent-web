using System;
using GraphQL.Types;

namespace intevent_web.Models
{
    public class VotingTotal
    {
        public string SongId { get; set; }

        public int Votes { get; set; }
    }

    public class VotingTotalGraphType : ObjectGraphType<VotingTotal>
    {
        public VotingTotalGraphType()
        {
            Name = "VotingTotal";
            Description = "";
            Field(_ => _.SongId).Description("Song Id");
            Field(_ => _.Votes).Description("Total Number of Votes");
        }
    }
}
