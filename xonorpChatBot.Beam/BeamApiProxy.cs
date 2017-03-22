using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.Beam
{
    public class BeamApiProxy
    {
        private HttpClient _httpClient;
        private string _baseUrl = "https://beam.pro/api/v1";

        public BeamApiProxy(/*string baseUrl*/)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Getting channels from the api
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetChannelsFromBeamAsync()
        {
            var channelUrl = $"/channels";

            HttpResponseMessage response = await _httpClient.GetAsync(channelUrl);

            if (response.IsSuccessStatusCode) // OR response.StatusCode == HttpStatusCode.OK
            {
                var channels = await response.Content.ReadAsStringAsync();
                return channels;
            }
            return string.Empty;
        }
    }
}
