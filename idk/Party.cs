using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using intevent_web.Models;

namespace intevent_web.idk
{
    public interface IParty
    {
        ConcurrentStack<SongVote> AllVotes { get; }

        IObservable<SongVote> SongVotes(string voterId);

        SongVote AddSongVote(SongVote vote);
    }

    public class Party: IParty
    {
        private readonly ISubject<SongVote> _voteStream = new ReplaySubject<SongVote>(1);

        public Party()
        {
            AllVotes = new ConcurrentStack<SongVote>();
            Users = new ConcurrentDictionary<string, string>
            {
                ["1"] = "developer",
                ["2"] = "tester"
            };
        }

        public ConcurrentDictionary<string, string> Users { get; set; }

        public ConcurrentStack<SongVote> AllVotes { get; }

        public SongVote AddSongVote(SongVote vote)
        {
            AllVotes.Push(vote);
            _voteStream.OnNext(vote);
            return vote;
        }

        public IObservable<SongVote> SongVotes(string voterId)
        {
            return _voteStream
                /*
                .Select(vote =>
                {
                    vote.Sub = user;
                    return vote;
                })
                */
                .AsObservable();
        }

        public void AddError(Exception exception)
        {
            _voteStream.OnError(exception);
        }
    }
}