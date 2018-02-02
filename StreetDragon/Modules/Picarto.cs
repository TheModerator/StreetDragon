using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    class Picarto
    {
        public async Task getOnline()
        {
            string url = Config.PICARTO_URL;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PicartoChannelInfo));
            request.UserAgent = "StreetDragon 0.666 (Discord Bot)";
            request.Timeout = 2000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            PicartoChannelInfo p2 = (PicartoChannelInfo)ser.ReadObject(stream);
        }
    }

    public class PicartoChannelInfo
    {
        public string id { get; set; }
        public BasicChannelInfo channel { get; set; } 
        public string type {get;set;}
        public string uri { get; set; }
    }

    public class BasicChannelInfo
    {
        public int user_id {get;set;}
        public string name { get; set; }
        public string avatar { get; set; }
        public Boolean online { get; set; }
    }
}
