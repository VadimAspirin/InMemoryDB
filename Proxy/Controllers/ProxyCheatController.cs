using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proxy.Models;

namespace Proxy.Controllers
{
    [Route("proxy")]
    [ApiController]
    public class ProxyCheatController : ControllerBase
    {
        [Route("delete/{id}")]
        [HttpGet]
        public string Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:10000/");
                var response = client.DeleteAsync("proxy/" + id.ToString()).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    return "Success";
                return "Error";
            }
        }

        [Route("put/{id}/{value}")]
        [HttpGet]
        public string Put(int id, string value)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:10000/");
                ValueDTO data = new ValueDTO { Value = value };
                string postBody = JsonConvert.SerializeObject(data);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PutAsync("proxy/" + id.ToString(), new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    return "Success";
                return "Error";
            }
        }
    }
}
