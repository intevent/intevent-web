using System;
using GraphQL.Types;

namespace intevent_web.Models
{
    public class Song
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public TimeSpan Duration { get; set; }

        public string AlbumArtUrl { get; set; }
    }

    public class SongGraphType : ObjectGraphType<Song>
    {
        public SongGraphType()
        {
            Name = "Song";
            Description = "";
            Field(_ => _.Id).Description("Id");
            Field(_ => _.Title).Description("Title");
            Field(_ => _.Artist).Description("Artist");
            Field(_ => _.Duration).Description("Duration");
            Field(_ => _.AlbumArtUrl).Description("Album Art URL");
        }
    }
}
