using System;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Claims;
using GraphQL.Resolvers;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Subscription;
using GraphQL.Types;
using intevent_web.Models;

namespace intevent_web.idk
{
    public class PartySubscriptions : ObjectGraphType<object>
    {
        private readonly IParty _party;

        public PartySubscriptions(IParty party)
        {
            _party = party;
            AddField(new EventStreamFieldType
            {
                Name = "voteAdded",
                Type = typeof(SongVoteGraphType),
                Resolver = new FuncFieldResolver<SongVote>(ResolveMessage),
                Subscriber = new EventStreamResolver<SongVote>(Subscribe)
            });

            AddField(new EventStreamFieldType
            {
                Name = "voteAddedByUser",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id"}
                ),
                Type = typeof(SongVoteGraphType),
                Resolver = new FuncFieldResolver<SongVote>(ResolveMessage),
                Subscriber = new EventStreamResolver<SongVote>(SubscribeById)
            });
        }

        private IObservable<SongVote> SubscribeById(ResolveEventStreamContext context)
        {
            /*
            var messageContext = context.UserContext.As<MessageHandlingContext>();
            var user = messageContext.Get<ClaimsPrincipal>("user");

            var sub = "Anonymous";
            if (user != null)
                sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var messages = _party.SongVotes(sub);

            var id = context.GetArgument<string>("voterId");
            return messages.Where(message => message.From.Id == id);
            */

            return _party.SongVotes(null);
        }

        private SongVote ResolveMessage(ResolveFieldContext context)
        {
            var message = context.Source as SongVote;

            return message;
        }

        private IObservable<SongVote> Subscribe(ResolveEventStreamContext context)
        {
            /*
            var voteContext = context.UserContext.As<MessageHandlingContext>();
            var user = voteContext.Get<ClaimsPrincipal>("user");

            var sub = "Anonymous";
            if (user != null)
                sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            */

            return _party.SongVotes(null);
        }
    }
}
