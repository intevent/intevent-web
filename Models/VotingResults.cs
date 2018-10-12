using System;
using System.Collections.Generic;
using GraphQL.Types;

namespace intevent_web.Models
{
    public class VotingResults
    {
        public IEnumerable<VotingTotal> Votes { get; set; }

        public bool CanVote { get; set; }
    }

    public class VotingResultsGraphType : ObjectGraphType<VotingResults>
    {
        public VotingResultsGraphType()
        {
            Name = "VotingResults";
            Description = "";
            Field<ListGraphType<VotingTotalGraphType>>("votes", "Votes");
            Field(_ => _.CanVote).Description("Is Voting still allowed");
        }
    }
}
