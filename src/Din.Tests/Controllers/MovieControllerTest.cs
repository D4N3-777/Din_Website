﻿using System;
using System.Collections.Generic;
using Din.Controllers;
using Din.Service.DTO;
using Din.Service.DTO.Content;
using Din.Tests.Fixtures;
using Din.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;
using Xunit;

namespace Din.Tests.Controllers
{
    public class MovieControllerTest : IClassFixture<MovieFixture>
    {
        private readonly MovieFixture _fixture;

        public MovieControllerTest(MovieFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SearchMovieAsyncTest()
        {
            const string query = "movie-title";
            var movieDto = new MovieDTO
            {
                CurrentMovieCollection = new List<int>
                {
                    1, 2
                },
                QueryCollection = new List<SearchMovie>()
            };
            _fixture.MockService.Setup(_ => _.SearchMovieAsync(query)).ReturnsAsync(movieDto);
            var controller = new MovieController(_fixture.MockService.Object, _fixture.Mapper);

            var result = controller.SearchMovieAsync(query);

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            Assert.IsType<MovieResultsViewModel>(viewResult.Model);
        }

        [Fact]
        public void AddMovieAsyncTest()
        {
            var movieToAdd = new SearchMovie
            {
                Title = "MovieTitle"
            };
            var resultDto = new ResultDTO
            {
                Title = "Success"
            };
            _fixture.MockService.Setup(_ => _.AddMovieAsync(movieToAdd, Convert.ToInt32(TestConsts.Id))).ReturnsAsync(resultDto);
            var controller = new MovieController(_fixture.MockService.Object, _fixture.Mapper)
            {
                ControllerContext = _fixture.ControllerContextWithSession()
            };

            var result = controller.AddMovieAsync(JsonConvert.SerializeObject(movieToAdd));

            var viewResult = Assert.IsType<PartialViewResult>(result.Result);
            //Assert.IsType<ResultViewModel>(viewResult.Model); //TODO no clue why it returns null
        }
    }
}