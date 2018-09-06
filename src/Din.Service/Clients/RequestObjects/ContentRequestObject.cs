﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.Service.Clients.RequestObjects
{
    public abstract class ContentRequestObject
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("qualityProfileId")] public int QualityProfileId { get; set; }
        [JsonProperty("profileId")] public string ProfileId { get; set; }
        [JsonProperty("titleslug")] public string TitleSlug { get; set; }
        [JsonProperty("images")] public List<ContentRequestObjectImage> Images { get; set; }
        [JsonProperty("rootFolderPath")] public string RootFolderPath { get; set; }
        [JsonProperty("monitored")] public bool Monitored { get; set; }
    }

    public class ContentRequestObjectImage
    {
        [JsonProperty("covertype")] public string CoverType { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }
}