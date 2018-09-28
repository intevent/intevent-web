using GraphQL.Types;

namespace intevent_web.idk
{
    public class PartySchema : Schema
    {
        public PartySchema(IParty party) 
        {
            //Query = new ChatQuery(chat);
            //Mutation = new ChatMutation(chat);
            Subscription = new PartySubscriptions(party);
        }
    }
}
