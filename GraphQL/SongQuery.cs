using System;
using GraphQL.Types;
using intevent_web.Models;

namespace intevent_web.GraphQL
{
    public class SongQuery : ObjectGraphType
    {
        public SongQuery()
        {
          Field<SongGraphType>(
            "song",
            resolve: (context) => new Song
            {
              Id = "lol",
              Title = "Africa",
              Artist = "Toto",
              Duration = new TimeSpan(0, 4, 34),
            }
          );
        }
    }
}
