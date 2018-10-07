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

            AddField(new EventStreamFieldType
            {
                Name = "songsUpdated",
                Type = typeof(SongListingGraphType),
                Resolver = new FuncFieldResolver<SongListing>(ResolveSongListing),
                Subscriber = new EventStreamResolver<SongListing>(SubscribeToSongListing)
            });

            AddField(new EventStreamFieldType
            {
                Name = "votingResultsUpdated",
                Type = typeof(VotingResultsGraphType),
                Resolver = new FuncFieldResolver<VotingResults>(ResolveVotingResults),
                Subscriber = new EventStreamResolver<VotingResults>(SubscribeToVotingResults)
            });
        }

        private VotingResults ResolveVotingResults(ResolveFieldContext context) => context.Source as VotingResults;

        private IObservable<VotingResults> SubscribeToVotingResults(ResolveEventStreamContext context) => PartyService.ObservableVotingResults;

        private SongListing ResolveSongListing(ResolveFieldContext context) => context.Source as SongListing;

        private IObservable<SongListing> SubscribeToSongListing(ResolveEventStreamContext context) => PartyService.ObservableSongListing;
    }
}
