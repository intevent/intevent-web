using System;
using GraphQL;
using GraphQL.Types;

namespace intevent_web.GraphQL
{
    public class PartySchema : Schema
    {
        public PartySchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<PartyQueries>();
            Mutation = resolver.Resolve<PartyMutations>();
            // Subscription = resolver.Resolve<PartySubscriptions>();
        }
    }
}
