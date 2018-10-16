using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using intevent_web.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace intevent_web.Services
{
    public interface ISpotifyService
    {
    }

    public class SpotifyService : ISpotifyService, IHostedService
    {
        private ILogger Logger { get; }

        private ISpotifyAuthService AuthService { get; }

        private IPartyService PartyService { get; }

        public SpotifyService(ILogger<SpotifyService> logger,
            ISpotifyAuthService authService,
            IPartyService partyService)
        {
            Logger = logger;
            AuthService = authService;
            PartyService = partyService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug("SpotifyService: StartAsync");
            Console.WriteLine("SpotifyService: StartAsync");

            await AuthService.StartAuthAsync();
            InitialiseMusic();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug("SpotifyService: StopAsync");
            Console.WriteLine("SpotifyService: StopAsync");

            return Task.CompletedTask;
        }

        private void InitialiseMusic()
        {
            var songs = new List<Song>
            {
                new Song { Id = "1", Artist = "Artist1", Title = "Title1", Duration = new TimeSpan(0, 1, 0) },
                new Song { Id = "2", Artist = "Artist2", Title = "Title2", Duration = new TimeSpan(0, 2, 0) },
                new Song { Id = "3", Artist = "Artist3", Title = "Title3", Duration = new TimeSpan(0, 3, 0) },
                new Song { Id = "4", Artist = "Artist4", Title = "Title4", Duration = new TimeSpan(0, 4, 0) },
                new Song { Id = "5", Artist = "Artist5", Title = "Title5", Duration = new TimeSpan(0, 5, 0) },
            };

            PartyService.Reset(songs, new Song { Id = "0", Artist = "Toto", Title = "Africa", Duration = new TimeSpan(0, 4, 34) });
        }
    }
}