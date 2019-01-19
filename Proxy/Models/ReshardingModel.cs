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
    public static class ReshardingModel
    {
        
        public static void Resharding(int port, List<int> reshard)
        {
            List<string> ports = new List<string>();
            ports.AddRange(Nodes.Ports);
            ports.Add(port.ToString());
            for (int i = 0; i < reshard.Count; ++i)
            {
                var resp = ProxyModel.GetValue(reshard[i]).Result;
                var del = ProxyModel.DeleteValue(reshard[i]).Result;
                var t = ProxyModel.SendReshardingValuesAsync(reshard[i], resp).Result;
            }
            Nodes.Ports.Add(port.ToString());
        }
    }

}
