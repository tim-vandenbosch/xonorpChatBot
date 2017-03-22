using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.Beam
{
    public abstract class BeamApiProxy
    {
        protected HttpClient _httpClient;
        private string _baseUrl = "https://beam.pro/api/v1";

        public BeamApiProxy(/*string baseUrl*/)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        
    }
}
