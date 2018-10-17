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
    public interface ISpotifyVotingService
    {
    }

    public class SpotifyVotingService : ISpotifyVotingService
    {
        private const int SearchLimit = 1;

        private ILogger Logger { get; }

        public SpotifyVotingService(ILogger<SpotifyVotingService> logger)
        {
            Logger = logger;
        }
    }
}