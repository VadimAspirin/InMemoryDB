using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDB.Models
{
    public class Database
    {
        public ConcurrentDictionary<int, string> dataBase;

        public Database()
        {
            dataBase = new ConcurrentDictionary<int, string>();
        }

        public Database(ConcurrentDictionary<int, string> db)
        {
            dataBase = db;
        }

        public string GetItem(int key)
        {
            if(dataBase.TryGetValue(key, out string value))
                return value;
            return "404";
        }

        public string AddOrUpdate(int key, string value)
        {
            dataBase.AddOrUpdate
                (key, value,
                (index, oldValue) => value);

            dataBase.TryGetValue(key, out string data);

            return data;
        }

        public string Delete(int key)
        {
            dataBase.TryRemove(key, out string data);
            return data;
        }

        public List<int> GetReshardingIndex(int index, int count)
        {
            return new List<int>(dataBase.Keys.Where(x => x % count != index));
        }
    }
}
