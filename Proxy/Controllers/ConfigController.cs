using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proxy.Models;

namespace Proxy.Controllers
{
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [Route("startNodes/{port}")]
        [HttpGet]
        public async Task<string> StartNodeAsync(int port)
        {
            string command = "cd " + Directory.GetCurrentDirectory() + @"\..\InMemoryDB\ & script.bat " + port.ToString();
            Process.Start("cmd.exe", "/C " + command);

            List<int> reshard = new List<int>();
            for (int i = 0; i < Nodes.Ports.Count(); ++i)
            {
                var result = await MessagesModel.SendReshardingRequest(i);
                if (result.Count() != 0)
                    reshard.AddRange(result);
            }
            if (reshard.Count() != 0)
                ReshardingModel.Resharding(port, reshard);

            Nodes.Ports.Add(port.ToString());

            return "localhost:" + port.ToString();
        }

        [Route("getNodes")]
        [HttpGet]
        public List<string> GetNodes()
        {
            return Nodes.Ports;
        }
    }
}
