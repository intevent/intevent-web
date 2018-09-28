using GraphQL.Types;
using intevent_web.Models;

namespace intevent_web.idk
{
    public class PartyMutation : ObjectGraphType<object>
    {
        public PartyMutation(IParty party)
        {
            Field<SongVoteGraphType>("addVote",
                arguments: new QueryArguments(
                    new QueryArgument<SongVoteInputType> {Name = "message"}
                ),
                resolve: context =>
                {
                    var receivedVote = context.GetArgument<SongVote>("vote");
                    var message = party.AddSongVote(receivedVote);
                    return message;
                });
        }
    }

    public class SongVoteInputType : InputObjectGraphType
    {
        public SongVoteInputType()
        {
            Field<StringGraphType>("voterId");
            Field<StringGraphType>("songId");
            Field<IntGraphType>("totalVotes");
            Field<DateTimeOffsetGraphType>("TimeUserVoted");
        }
    }
}