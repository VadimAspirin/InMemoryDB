using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Models
{
    public static class ProxyModel
    {
        public static int GetShardNumber(int id)
        {
            return id % Nodes.Ports.Count();
        }

        public static async Task<string> GetValue(int id)
        {
            string nodePort = Nodes.Ports[ProxyModel.GetShardNumber(id)];

            string url = "http://localhost:" + nodePort + "/database/" + id.ToString();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject(content).ToString();
                return responseContent;
            }
        }

        public static async Task<string> PutValue(int id, string value)
        {
            string nodePort = Nodes.Ports[ProxyModel.GetShardNumber(id)];

            string url = "http://localhost:" + nodePort + "/database/" + id.ToString();
            string responseContent;
            ValueDTO data = new ValueDTO { Value = value };
            string postBody = JsonConvert.SerializeObject(data);
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    responseContent = JsonConvert.DeserializeObject(content).ToString();
                else
                    responseContent = value;
                return responseContent;
            }
        }

        public static async Task<string> DeleteValue(int id)
        {
            string nodePort = Nodes.Ports[ProxyModel.GetShardNumber(id)];

            string url = "http://localhost:" + nodePort + "/database/" + id.ToString();
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject(content).ToString();
                return responseContent;
            }
        }

        public static async Task<string> SendReshardingValuesAsync(int key, string value)
        {
            string url = "http://localhost:10000/proxy/" + key.ToString();
            string responseContent;
            ValueDTO data = new ValueDTO { Value = value };
            string postBody = JsonConvert.SerializeObject(data);
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                    responseContent = JsonConvert.DeserializeObject(content).ToString();
                else
                    responseContent = value;
                return responseContent;
            }
        }
    }
}
