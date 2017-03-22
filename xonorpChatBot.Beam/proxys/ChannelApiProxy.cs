using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xonorpChatBot.Beam.proxys
{
    public class ChannelApiProxy : BeamApiProxy
    {
        /// <summary>
        /// Getting channels from the api
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetChannelsFromBeamAsync()
        {
            var channelUrl = $"/channels";
            var response = await _httpClient.GetAsync(channelUrl);

            if (response.IsSuccessStatusCode) // OR response.StatusCode == HttpStatusCode.OK
                return await response.Content.ReadAsStringAsync();
            return string.Empty;
        }

        ///// <summary>
        ///// Getting a channel by id
        ///// </summary>
        ///// <param name="channelId"></param>
        ///// <returns></returns>
        //public async Task<string> GetChannelFromBeamAsyncById(int channelId)
        //{
        //    var channelByIdUrl = $"/channels/{channelId}";
        //    var response = await _httpClient.GetAsync(channelByIdUrl);

        //    if (response.IsSuccessStatusCode)
        //        return await response.Content.ReadAsStringAsync();
        //    return string.Empty;
        //}
    }
}
