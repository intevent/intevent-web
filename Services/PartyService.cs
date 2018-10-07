using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using intevent_web.Models;

namespace intevent_web.Services
{
    public interface IPartyService
    {
        IEnumerable<Song> Songs { get; }

        IObservable<IEnumerable<Song>> ObservableSongs { get; }

        IEnumerable<VoteTotal> SongVotes { get; }

        IObservable<IEnumerable<VoteTotal>> ObservableSongVotes { get; }

        SongVote AddSongVote(SongVote vote);

        void Reset(IEnumerable<Song> songs);
    }
    
    public class PartyService : IPartyService
    {
        private ISubject<IEnumerable<Song>> SongStream { get; } = new ReplaySubject<IEnumerable<Song>>(1);

        private ISubject<IEnumerable<VoteTotal>> VoteTotalStream { get; } = new ReplaySubject<IEnumerable<VoteTotal>>(1);

        private ConcurrentBag<Song> AllSongs { get; } = new ConcurrentBag<Song>();

        private ConcurrentDictionary<string, string> AllVotes { get; } = new ConcurrentDictionary<string, string>();

        public PartyService()
        {
            Reset(new List<Song>
            {
                new Song { Id = "1", Artist = "Artist1", Title = "Title1", Duration = new TimeSpan(0, 1, 0) },
                new Song { Id = "2", Artist = "Artist2", Title = "Title2", Duration = new TimeSpan(0, 2, 0) },
                new Song { Id = "3", Artist = "Artist3", Title = "Title3", Duration = new TimeSpan(0, 3, 0) },
                new Song { Id = "4", Artist = "Artist4", Title = "Title4", Duration = new TimeSpan(0, 4, 0) },
                new Song { Id = "5", Artist = "Artist5", Title = "Title5", Duration = new TimeSpan(0, 5, 0) },
            });
        }

        public IEnumerable<Song> Songs
        {
            get { return AllSongs.AsEnumerable(); }
        }

        public IObservable<IEnumerable<Song>> ObservableSongs
        {
            get { return SongStream.AsObservable(); }
        }

        public IEnumerable<VoteTotal> SongVotes
        {
            get { return GenerateVoteTotals(AllSongs, AllVotes); }
        }

        public IObservable<IEnumerable<VoteTotal>> ObservableSongVotes
        {
            get { return VoteTotalStream.AsObservable(); }
        }

        public SongVote AddSongVote(SongVote vote)
        {
            AllVotes[vote.VoterId] = vote.SongId;
            VoteTotalStream.OnNext(GenerateVoteTotals(AllSongs, AllVotes));

            return vote;
        }

        public void Reset(IEnumerable<Song> songs)
        {
            AllSongs.Clear();
            foreach (Song song in songs)
            {
                AllSongs.Add(song);
            }

            AllVotes.Clear();

            SongStream.OnNext(AllSongs.AsEnumerable());
            VoteTotalStream.OnNext(GenerateVoteTotals(AllSongs, AllVotes));
        }

        private IEnumerable<VoteTotal> GenerateVoteTotals(IEnumerable<Song> songs, IDictionary<string, string> votes)
        {
            return songs.Select(s => new VoteTotal
            {
                SongId = s.Id,
                Votes = votes.Count(v => v.Value == s.Id)
            });
        }
    }
}
