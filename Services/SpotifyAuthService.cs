using System;
using System.Collections.Generic;
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
    public interface ISpotifyAuthService
    {
        Task StartAuthAsync();

        Task RefreshAuthAsync();
    }

    public class SpotifyAuthService : ISpotifyAuthService
    {
        private ILogger Logger { get; }
        
        private IHttpClientFactory HttpClientFactory { get; }

        private string SpotifyClientId { get; }

        private string SpotifyClientSecret { get; }

        private string EncodedClientCredentials { get; }

        private SpotifyToken SpotifyToken { get; set; }

        private Timer Timer { get; set; }

        public SpotifyAuthService(IHttpClientFactory httpClientFactory, ILogger<SpotifyAuthService> logger)
        {
            HttpClientFactory = httpClientFactory;
            Logger = logger;

            SpotifyClientId = Environment.GetEnvironmentVariable("spotify_clientId");
            SpotifyClientSecret = Environment.GetEnvironmentVariable("spotify_clientSecret");
            //SpotifyClientId = "CLIENT ID HERE";
            //SpotifyClientSecret = "SECRET HERE";

            if (string.IsNullOrWhiteSpace(SpotifyClientId) || string.IsNullOrWhiteSpace(SpotifyClientSecret))
            {
                Console.WriteLine("Missing Spotify account details");
                throw new ArgumentException("Missing Spotify account details");
            }

            EncodedClientCredentials = EncodeClientConnectionDetails(SpotifyClientId, SpotifyClientSecret);
        }

        public async Task StartAuthAsync()
        {
            Logger.LogDebug("SpotifyAuthService: StartAuthAsync");
            Console.WriteLine("SpotifyAuthService: StartAuthAsync");
            
            using (HttpClient client = HttpClientFactory.CreateClient("spotifyAccountClient"))
            {
                var kvp = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                client.DefaultRequestHeaders.Add("Authorization", $"Basic {EncodedClientCredentials}");
                var response = await client.PostAsync($"{client.BaseAddress}/token", new FormUrlEncodedContent(kvp));
                var json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Logger.LogError($"Response JSON: {json}");
                    Console.WriteLine($"Response JSON: {json}");
                }

                SpotifyToken = JsonConvert.DeserializeObject<SpotifyToken>(json);
                Console.WriteLine($"Expires in: {SpotifyToken.ExpiresIn}");
            }
        }

        public async Task RefreshAuthAsync()
        {
        }

        private string EncodeClientConnectionDetails(string clientId, string clientSecret) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret)));
    }
}