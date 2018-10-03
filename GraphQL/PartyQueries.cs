using System;
using GraphQL.Types;
using intevent_web.Models;
using intevent_web.Services;

namespace intevent_web.GraphQL
{
    public class PartyQueries : ObjectGraphType
    {
        public PartyQueries(IPartyService partyService)
        {
            Name = "Query";

            Field<ListGraphType<SongGraphType>>(
                name: "songs",
                description: "songs",
                resolve: (context) => partyService.Songs
            );
        }
    }
}
