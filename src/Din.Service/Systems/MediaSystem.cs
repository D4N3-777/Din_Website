﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Din.ExternalModels.Entities;
using Din.ExternalModels.MediaSystem;
using Din.ExternalModels.Utils;
using Din.Service.Concrete;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Service.Systems
{

    //TODO rename to client and supply HttpClientFactory
    public class MediaSystem
    {
        private readonly string _url;
        private readonly string _key;
        private readonly string _savePath;

        /* TODO These are all DEPRECATED and should be removed
        private readonly string _movieSystemUrl = MainService.PropertyFile.Get("movieSystemUrl");
        private readonly string _movieSystemKey = MainService.PropertyFile.Get("movieSystemKey");
        private readonly string _tvShowSystemUrl = MainService.PropertyFile.Get("tvShowSystemUrl");
        private readonly string _tvShowSystemKey = MainService.PropertyFile.Get("tvShowSystemKey");
        */
        public MediaSystem(string url, string key, string savePath)
        {
            _url = url;
            _key = key;
            _savePath = savePath;
        }

        public async Task<List<int>> GetCurrentMoviesAsync()
        {
            var movieIds = new List<int>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(
                    await new HttpRequestHelper(_movieSystemUrl, false).PerformGetRequestAsync());
            foreach (var m in objects)
                movieIds.Add(m.Tmdbid);
            return movieIds;
        }

        public async Task<List<string>> GetCurrentTvShowsAsync()
        {
            var tvShowIds = new List<string>();
            var objects =
                JsonConvert.DeserializeObject<List<MediaSystemTvShow>>(
                    await new HttpRequestHelper(_tvShowSystemUrl, false).PerformGetRequestAsync());
            foreach (var t in objects)
                tvShowIds.Add(t.Title.ToLower());
            return tvShowIds;
        }

        public async Task<int> AddMovieAsync(SearchMovie movie)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("poster", movie.PosterPath)};
<<<<<<< Updated upstream
            var payload = new MediaSystemMovie(movie.Title, Convert.ToDateTime(movie.ReleaseDate), movie.Id, images, MainService.PropertyFile.Get("movieSystemFileLocation"));
            var response = await new HttpRequestHelper(_movieSystemUrl, false).PerformPostRequestAsync(JsonConvert.SerializeObject(payload));
=======
            var payload = new MediaSystemMovie(movie.Title, Convert.ToDateTime(movie.ReleaseDate), movie.Id, images,
                _savePath);
            var response =
                await new HttpRequestHelper(BuildRequestUrl(Selection.MovieSystem, Endpoint.Movie), false)
                    .PerformPostRequestAsync(
                        JsonConvert.SerializeObject(payload));
>>>>>>> Stashed changes
            return response.Item1;
        }

        public async Task<int> AddTvShowAsync(SearchTv show, string tvdbId, IEnumerable<SearchTvSeason> seasons)
        {
            var images = new List<MediaSystemImage> {new MediaSystemImage("poster", show.PosterPath)};
<<<<<<< Updated upstream
            var payload = new MediaSystemTvShow(show.Name, Convert.ToDateTime(show.FirstAirDate), tvdbId, images, seasons,
                MainService.PropertyFile.Get("tvShowSystemFileLocation"));
=======
            var payload = new MediaSystemTvShow(show.Name, Convert.ToDateTime(show.FirstAirDate), tvdbId, images,
                seasons,
               _savePath);
>>>>>>> Stashed changes
            var response =
                await new HttpRequestHelper(_tvShowSystemUrl, false).PerformPostRequestAsync(
                    JsonConvert.SerializeObject(payload));
            return response.Item1;
        }

        public async Task<List<AddedContent>> CheckIfItemIsCompletedAsync(List<AddedContent> content)
        {
            var httpRequest = new HttpRequestHelper(_url, false);
            var current =
                JsonConvert.DeserializeObject<List<MediaSystemMovie>>(await httpRequest.PerformGetRequestAsync());
            foreach (var i in content)
            foreach (var m in current)
            {
                if (!i.Title.Equals(m.Title)) continue;
                if (!m.Downloaded) continue;
                i.Status = ContentStatus.Downloaded;
                break;
            }
            return content;
        }
<<<<<<< Updated upstream
=======

        private string BuildRequestUrl(Selection system, Endpoint endpoint)
        {
            return system.Equals(Selection.MovieSystem)
                ? new StringBuilder().Append(_url).Append(endpoint.ToString().ToLower())
                    .Append(_url).ToString()
                : new StringBuilder().Append(_url).Append(endpoint.ToString().ToLower())
                    .Append(_url).ToString();
        }
    }

    internal enum Selection
    {
        MovieSystem,
        TvShowSystem
    }

    internal enum Endpoint
    {
        Movie,
        Series,
        Calendar
>>>>>>> Stashed changes
    }
}
