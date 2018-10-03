using System;
using System.Security.Claims;

namespace intevent_web.GraphQL
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
