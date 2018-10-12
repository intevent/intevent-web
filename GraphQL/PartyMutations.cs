using System;
using GraphQL.Types;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web.GraphQL
{
    public class PartyMutations : ObjectGraphType
    {
        public PartyMutations(IPartyService partyService)
        {
            Name = "Mutation";

            Field<SongVoteGraphType>("addVote",
                arguments: new QueryArguments(
                    new QueryArgument<SongVoteInputGraphType> {Name = "vote"}
                ),
                resolve: context =>
                {
                    SongVote receivedVote = context.GetArgument<SongVote>("vote");
                    SongVote savedVote = partyService.AddSongVote(receivedVote);
                    return savedVote;
                }
            );
        }
    }
}
