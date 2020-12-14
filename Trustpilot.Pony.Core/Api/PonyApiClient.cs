using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Trustpilot.Pony.Core.Model;

namespace Trustpilot.Pony.Core.Api
{
    public class PonyApiClient
    {
        private readonly HttpClient _httpClient;

        public PonyApiClient(string baseUrl) : this(new HttpClient { BaseAddress = new Uri(baseUrl) })
        {
        }

        public PonyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Uri BaseUri => _httpClient.BaseAddress;

        public async Task<MazeIdentifier> CreateNewMaze(MazeParams mazeParams)
        {
            if (mazeParams == null)
                throw new ArgumentNullException(nameof(mazeParams));

            mazeParams.ValidateParams();

            var url = "pony-challenge/maze";
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(mazeParams), Encoding.UTF8, "application/json")
            };
            return await GetResponse<MazeIdentifier>(request);
        }

        public async Task<Maze> GetMaze(MazeIdentifier mazeIdentifier)
        {
            if (mazeIdentifier == null)
                throw new ArgumentNullException(nameof(mazeIdentifier));

            var url = $"pony-challenge/maze/{WebUtility.UrlEncode(mazeIdentifier.MazeId)}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await GetResponse<Maze>(request);
        }

        public async Task<GameState> MakeMove(MazeIdentifier mazeIdentifier, MazeMove direction)
        {
            if (mazeIdentifier == null)
                throw new ArgumentNullException(nameof(mazeIdentifier));
            if (direction == null)
                throw new ArgumentNullException(nameof(direction));

            var url = $"pony-challenge/maze/{WebUtility.UrlEncode(mazeIdentifier.MazeId)}";
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(direction), Encoding.UTF8, "application/json")
            };
            return await GetResponse<GameState>(request);
        }

        public async Task<string> GetMazePrintString(MazeIdentifier mazeIdentifier)
        {
            if (mazeIdentifier == null)
                throw new ArgumentNullException(nameof(mazeIdentifier));

            var url = $"pony-challenge/maze/{WebUtility.UrlEncode(mazeIdentifier.MazeId)}/print";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await GetResponse<string>(request);
        }

        private async Task<T> GetResponse<T>(HttpRequestMessage request) where T : class
        {
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(content);
            }

            if (typeof(T) == typeof(string))
            {
                return content as T;
            }

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
