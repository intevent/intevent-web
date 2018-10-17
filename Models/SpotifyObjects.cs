using System;
using Newtonsoft.Json;

namespace intevent_web.Models
{
    public class SpotifyToken
    {
        [JsonProperty("Access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("Token_type")]
        public string TokenType { get; set; }

        [JsonProperty("Expires_in")]
        public int ExpiresIn { get; set; }
    }
}
