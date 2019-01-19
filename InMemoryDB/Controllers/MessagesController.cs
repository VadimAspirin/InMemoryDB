using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InMemoryDB.Models;

namespace InMemoryDB.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {

        private Database _data;
        string _port;

        public MessagesController(Database databaseModel, string port)
        {
            _data = databaseModel;
            _port = port;
        }

        [Route("resharding")]
        [HttpPost]
        public List<int> Post([FromBody] ReshardingDataDTO data)
        {
            return _data.GetReshardingIndex(data.Index, data.Count);
        }
    }
}
