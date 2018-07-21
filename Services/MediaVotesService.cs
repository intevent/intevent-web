using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using intevent_web.Models;

namespace intevent_web.Services
{
    public interface IMediaVotesService
    {
        Task<IEnumerable<MediaVoteSet>> GetMediaVotes();
        Task<MediaVoteSet> GetMediaVotes(int id);
    }

    public class MediaVotesService : IMediaVotesService
    {
        private HttpClient Client { get; }

        public MediaVotesService(HttpClient client)
        {
            Client = client;
        }

        public async Task<IEnumerable<MediaVoteSet>> GetMediaVotes()
        {
            HttpResponseMessage response = await Client.GetAsync("api/MediaVotes");
            return await response.Content.ReadAsAsync<IEnumerable<MediaVoteSet>>();
        }

        public async Task<MediaVoteSet> GetMediaVotes(int id)
        {
            HttpResponseMessage response = await Client.GetAsync($"api/MediaVotes/{id}");
            return await response.Content.ReadAsAsync<MediaVoteSet>();
        }
    }
}
