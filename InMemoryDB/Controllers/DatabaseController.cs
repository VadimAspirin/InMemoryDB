using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InMemoryDB.Models;
using System.IO;
using System.Collections.Concurrent;

namespace InMemoryDB.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class DatabaseController : Controller
    {

        private Database _data;
        private string _port;

        public DatabaseController(Database databaseModel, string port)
        {
            _data = databaseModel;
            _port = port;
        }

        [HttpGet]
        public ConcurrentDictionary<int, string> Get()
        {
            return _data.dataBase;
        }

        [Route("count")]
        [HttpGet]
        public string GetCount()
        {
            return _data.dataBase.Count().ToString();
        }

        [HttpGet("{id}")]
        public string GetItem(int id)
        {
            return _data.GetItem(id);
        }
        
        [HttpPut("{id}")]
        public async Task<string> Create([FromBody] ValueDTO data, int id)
        {
            var writedData = _data.AddOrUpdate(id, data.Value);

            string dataFileName = "node" + _port + @"\data.db";
            await WriteReadToDatabase.Write(dataFileName, _data.dataBase);

            return writedData;
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteAsync(int id)
        {
            var deletedData = _data.Delete(id);

            string dataFileName = "node" + _port + @"\data.db";
            await WriteReadToDatabase.Write(dataFileName, _data.dataBase);

            return deletedData;
        }
    }
}
