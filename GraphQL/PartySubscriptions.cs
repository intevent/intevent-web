using System;
using System.Collections.Generic;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web.GraphQL
{
    public class PartySubscriptions : ObjectGraphType
    {
        private IPartyService PartyService { get; }

        public PartySubscriptions(IPartyService partyService)
        {
            PartyService = partyService;

            /*
            AddField(new EventStreamFieldType
            {
                Name = "songsUpdated",
                Type = typeof(ListGraphType<SongGraphType>),
                Resolver = new FuncFieldResolver<Message>(ResolveMessage),
                Subscriber = new EventStreamResolver<Message>(Subscribe)
            });
            */

            AddField(new EventStreamFieldType
            {
                Name = "votesUpdated",
                Type = typeof(VoteTotalGraphType),
                Resolver = new FuncFieldResolver<IEnumerable<VoteTotal>>(ResolveVotes),
                Subscriber = new EventStreamResolver<IEnumerable<VoteTotal>>(SubscribeToVotes)
            });
        }

        private IEnumerable<VoteTotal> ResolveVotes(ResolveFieldContext context)
        {
            var votes = context.Source as IEnumerable<VoteTotal>;
            return votes;
        }

        private IObservable<IEnumerable<VoteTotal>> SubscribeToVotes(ResolveEventStreamContext context)
        {
            return PartyService.ObservableSongVotes;
        }
    }
}
