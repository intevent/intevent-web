using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using intevent_web.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace intevent_web.Services
{
    public interface ISpotifySongService
    {
      Song StartingSong { get; }
      Task<IEnumerable<Song>> GetSongsAsync(string authToken, int songCount);
    }

    public class SpotifySongService : ISpotifySongService
    {
        private const int SearchLimit = 1;

        private ILogger Logger { get; }
        
        private IHttpClientFactory HttpClientFactory { get; }

        public SpotifySongService(IHttpClientFactory httpClientFactory, ILogger<SpotifySongService> logger)
        {
            HttpClientFactory = httpClientFactory;
            Logger = logger;
        }

        public Song StartingSong => new Song
        {
            Id = "62U7xIHcID94o20Of5ea4D",
            Title = "Africa",
            Artist = "Toto",
            Duration = TimeSpan.FromMilliseconds(295893),
            AlbumArtUrl = "https://i.scdn.co/image/f934fc64507ef928a246607f5a505f1bac9ea746",
        };

        public async Task<IEnumerable<Song>> GetSongsAsync(string authToken, int songCount)
        {
            Logger.LogDebug("SpotifySongService: GetSongsAsync");
            Console.WriteLine("SpotifySongService: GetSongsAsync");
            
            using (HttpClient client = HttpClientFactory.CreateClient("spotifyApiClient"))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

                IEnumerable<Song> songs = new List<Song>(songCount);
                for (int i = 0; i < songCount; i++)
                {
                    songs = songs.Append(await GetSongAsync(client));
                }

                return songs;
            }
        }

        private async Task<Song> GetSongAsync(HttpClient client)
        {
            Song song = null;
            bool found = false;
            Random random = new Random();
            while (!found)
            {
                string uri = GenerateUri(client.BaseAddress, GenerateSearchTerm());
                //Logger.LogDebug($"SpotifySongService: {uri}");
                //Console.WriteLine($"SpotifySongService: {uri}");
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<dynamic>(json);
                int totalResults = searchResult.tracks.total;
                if (totalResults == 0) continue;

                var track = searchResult.tracks.items[random.Next(Math.Min(SearchLimit, totalResults))];

                song = new Song
                {
                    Id = track.id,
                    Title = track.name,
                    Artist = string.Join(", ", ((IEnumerable<dynamic>)track.artists).Select(a => (string)a.name)),
                    Duration = TimeSpan.FromMilliseconds((double)track.duration_ms),
                    AlbumArtUrl = track.album.images[1].url,
                };

                found = true;
            }

            return song;
        }

        private string GenerateUri(Uri baseAddress, string searchTerm) =>
            $"{baseAddress}/search?q={searchTerm}&market=ZA&type=track&limit={SearchLimit}";

        private string GenerateSearchTerm()
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            int numberOfCharacters = 3;
            char[] searchTerm = new char[numberOfCharacters];
            Random random = new Random();

            for (int i = 0; i < numberOfCharacters; i++)
            {
              searchTerm[i] = alphabet[random.Next(alphabet.Length)];
            }

            return new string(searchTerm);
        }
    }
}