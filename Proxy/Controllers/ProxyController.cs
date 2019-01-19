using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proxy.Models;

namespace Proxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            return await ProxyModel.GetValue(id);
        }

        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] ValueDTO data)
        {
            return await ProxyModel.PutValue(id, data.Value);
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await ProxyModel.DeleteValue(id);
        }
    }
}
