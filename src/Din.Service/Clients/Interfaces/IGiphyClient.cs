﻿using System.Threading.Tasks;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.ResponseObjects;

namespace Din.Service.Clients.Interfaces
{
    public interface IGiphyClient
    {
        Task<GiphyResponse> GetRandomGifAsync(GiphyTag tag);
    }
}