﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DinWebsite.ExternalModels.DownloadClient
{
    public class DownloadClientRequestObject2
    {
        public DownloadClientRequestObject2()
        {
        }

        public DownloadClientRequestObject2(string method, List<List<string>> paramaters, int id)
        {
            Method = method;
            Params = paramaters;
            Id = id;
        }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<List<string>> Params { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}