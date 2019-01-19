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
using InMemoryDB.Models;

namespace InMemoryDB.Controllers
{
    [Produces("application/json")]
    [Route("database")]
    public class DatabaseСheatController : Controller
    {

        string _port;

        public DatabaseСheatController(string port)
        {
            _port = port;
        }

        [Route("delete/{id}")]
        [HttpGet]
        public string Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:" + _port);
                var response = client.DeleteAsync("database/"+id.ToString()).Result;
                if (response.IsSuccessStatusCode)
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
                client.BaseAddress = new Uri("http://localhost:" + _port);
                ValueDTO data = new ValueDTO { Value = value };
                string postBody = JsonConvert.SerializeObject(data);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PutAsync("database/" + id.ToString(), new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                    return "Success";
                return "Error";
            }
        }
    }
}
