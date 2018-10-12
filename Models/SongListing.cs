using System;
using System.Collections.Generic;
using GraphQL.Types;

namespace intevent_web.Models
{
    public class SongListing
    {
        public Song CurrentlyPlaying { get; set; }

        public IEnumerable<Song> VotableSongs { get; set; }
    }

    public class SongListingGraphType : ObjectGraphType<SongListing>
    {
        public SongListingGraphType()
        {
            Name = "SongListing";
            Description = "";
            Field<SongGraphType>("currentlyPlaying", "Currently Playing Song");
            Field<ListGraphType<SongGraphType>>("votableSongs", "Votable Songs");
        }
    }
}
