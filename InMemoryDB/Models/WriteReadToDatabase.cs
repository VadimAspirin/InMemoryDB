using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDB.Models
{
    public static class WriteReadToDatabase
    {
        public static async Task Write(string path, ConcurrentDictionary<int, string> data)
        {
            var writer = new StreamWriter(path);

            foreach (var str in data)
            {
                await writer.WriteLineAsync(str.Key.ToString() + ':' + str.Value);
            }

            writer.Close();
        }

        public static ConcurrentDictionary<int, string> Read(string path)
        {
            Dictionary<int, string> buf = new Dictionary<int, string>();
            var reader = new StreamReader(path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] arr = line.Split(':');

                if (Int32.TryParse(arr[0], out int key))
                    buf.Add(key, arr[1]);
            }
            reader.Close();
            return new ConcurrentDictionary<int, string>(buf);
        }
    }
}
