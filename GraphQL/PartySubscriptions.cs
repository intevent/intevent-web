using System;
using GraphQL.Types;
using intevent_web.Services;

namespace intevent_web.GraphQL
{
    public class PartySubscriptions : ObjectGraphType
    {
        private IPartyService PartyService { get; }

        public PartySubscriptions(IPartyService partyService)
        {
            PartyService = partyService;
        }
    }
}
