using System.Text.Json.Serialization;

namespace BlazorApp2.Response
{
    public class JwtTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? JwtToken { get; set; }
    }
}
