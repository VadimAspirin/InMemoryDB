using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proxy.Models;

namespace Proxy.Models
{
    public static class MessagesModel
    {
        public static async Task<List<int>> SendReshardingRequest(int nodeIndex)
        {
            string url = "http://localhost:" + Nodes.Ports[nodeIndex] + "/messages/resharding";
            ReshardingDataDTO data = new ReshardingDataDTO { Index = nodeIndex, Count = (Nodes.Ports.Count + 1) };
            string postBody = JsonConvert.SerializeObject(data);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage wcfResponse = await client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                var result = wcfResponse.Content.ReadAsAsync<List<int>>().GetAwaiter().GetResult();
                return result;
            }
        }
    }
}
