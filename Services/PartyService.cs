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
        SongListing SongListing { get; }

        IObservable<SongListing> ObservableSongListing { get; }

        VotingResults VotingResults { get; }

        IObservable<VotingResults> ObservableVotingResults { get; }

        Song CurrentlyPlaying { get; }

        bool CanVote { get; }

        SongVote AddSongVote(SongVote vote);

        void Reset(IEnumerable<Song> songs, Song currentlyPlaying);

        void LockVoting();
    }

    public class PartyService : IPartyService
    {
        private ISubject<SongListing> SongListingStream { get; } = new ReplaySubject<SongListing>(1);

        private ISubject<VotingResults> VotingResultsStream { get; } = new ReplaySubject<VotingResults>(1);

        private ConcurrentBag<Song> AllSongs { get; } = new ConcurrentBag<Song>();

        private ConcurrentDictionary<string, string> AllVotes { get; } = new ConcurrentDictionary<string, string>();

        public PartyService()
        {
        }

        public SongListing SongListing
        {
            get { return GenerateSongListing(AllSongs, CurrentlyPlaying); }
        }

        public IObservable<SongListing> ObservableSongListing
        {
            get { return SongListingStream.AsObservable(); }
        }

        public VotingResults VotingResults
        {
            get { return GenerateVotingResults(AllSongs, AllVotes, CanVote); }
        }

        public IObservable<VotingResults> ObservableVotingResults
        {
            get { return VotingResultsStream.AsObservable(); }
        }

        public bool CanVote { get; private set; } = false;

        public Song CurrentlyPlaying { get; private set; }

        public SongVote AddSongVote(SongVote vote)
        {
            if (CanVote)
            {
                AllVotes[vote.VoterId] = vote.SongId;
                VotingResultsStream.OnNext(GenerateVotingResults(AllSongs, AllVotes, CanVote));
                return vote;
            }

            return new SongVote{ VoterId = vote.VoterId, SongId = AllVotes[vote.VoterId] };
        }

        public void Reset(IEnumerable<Song> songs, Song currentlyPlaying)
        {
            AllSongs.Clear();
            foreach (Song song in songs)
            {
                AllSongs.Add(song);
            }

            AllVotes.Clear();
            CanVote = true;
            CurrentlyPlaying = currentlyPlaying;

            SongListingStream.OnNext(GenerateSongListing(AllSongs, CurrentlyPlaying));
            VotingResultsStream.OnNext(GenerateVotingResults(AllSongs, AllVotes, CanVote));
        }

        public void LockVoting()
        {
            CanVote = false;
            VotingResultsStream.OnNext(GenerateVotingResults(AllSongs, AllVotes, CanVote));
        }

        private VotingResults GenerateVotingResults(IEnumerable<Song> songs, IDictionary<string, string> votes, bool canVote)
        {
            return new VotingResults
            {
                Votes = songs.Select(s => new VotingTotal
                {
                    SongId = s.Id,
                    Votes = votes.Count(v => v.Value == s.Id)
                }),
                CanVote = canVote
            };
        }

        private SongListing GenerateSongListing(IEnumerable<Song> songs, Song currentlyPlaying)
        {
            return new SongListing
            {
                CurrentlyPlaying = currentlyPlaying,
                VotableSongs = songs
            };
        }
    }
}
