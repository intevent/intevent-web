using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using intevent_web.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace intevent_web.Services
{
    public class SpotifyService : IHostedService, IDisposable
    {
        private const int SecondsBetweenVotingRounds = 30;
        private const int SongOptionLimit = 5;

        private ILogger Logger { get; }

        private ISpotifyAuthService AuthService { get; }

        private ISpotifySongService SongService { get; }

        private IPartyService PartyService { get; }

        private Timer Timer { get; set; }

        public SpotifyService(ILogger<SpotifyService> logger,
            ISpotifyAuthService authService,
            ISpotifySongService songService,
            IPartyService partyService)
        {
            Logger = logger;
            AuthService = authService;
            SongService = songService;
            PartyService = partyService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug("SpotifyService: StartAsync");
            Console.WriteLine("SpotifyService: StartAsync");

            await AuthService.StartAuthAsync();
            await InitialiseMusicAsync();

            Timer = new Timer(ProcessRound, null, TimeSpan.Zero, TimeSpan.FromSeconds(SecondsBetweenVotingRounds));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug("SpotifyService: StopAsync");
            Console.WriteLine("SpotifyService: StopAsync");

            Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async Task InitialiseMusicAsync()
        {
            var songs = await SongService.GetSongsAsync(AuthService.AuthToken, SongOptionLimit);
            PartyService.Reset(songs, SongService.StartingSong);
        }

        private void ProcessRound(object state)
        {
            Logger.LogDebug("SpotifyService: ProcessRound");
            Console.WriteLine("SpotifyService: ProcessRound");

            Task.Run(() => ProcessRoundAsync()).Wait();
        }

        private async Task ProcessRoundAsync()
        {
            var winningSongIds = PartyService.VotingResults.Votes
                .GroupBy(
                    v => v.Votes,
                    v => v.SongId,
                    (key, idk) => new { Votes = key, SongIds = idk.ToList() }
                )
                .OrderByDescending(v => v.Votes)
                .First();

            var winningSongIdsCount = winningSongIds.SongIds.Count();
            Random random = new Random();
            var winningSongId = winningSongIds.SongIds[random.Next(winningSongIdsCount)];
            var winningSong = PartyService.SongListing.VotableSongs.First(s => s.Id == winningSongId);
            
            Logger.LogDebug($"WINNER: '{winningSong.Title}', with {winningSongIds.Votes} votes");
            Console.WriteLine($"WINNER: '{winningSong.Title}', with {winningSongIds.Votes} votes");
            var songs = await SongService.GetSongsAsync(AuthService.AuthToken, SongOptionLimit);
            PartyService.Reset(songs, winningSong);
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}